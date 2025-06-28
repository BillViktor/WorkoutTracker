using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.Auth;
using WorkoutTracker.Shared.Dto.Auth;
using WorkoutTracker.Shared.Dto.Auth.Email;
using WorkoutTracker.Shared.Dto.Auth.Password;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and management operations.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IAuthService authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            this.logger = logger;
            this.authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<ResultModel>> Register(RegisterRequestDto request)
        {
            var result = await authService.Register(request);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                logger.LogWarning("User registration failed for {Email}. Errors: {Errors}", request.Email, errors);
                return BadRequest(new ResultModel(errors));
            }

            logger.LogInformation("User {Email} registered successfully.", request.Email);
            return Ok(new ResultModel { Message = "User registered successfully." });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ResultModel>> Login(LoginRequestDto request)
        {
            var result = await authService.Login(request);

            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    logger.LogWarning("User {UserName} is not allowed to login. Email not confirmed.", request.UserName);
                    return BadRequest(new ResultModel("Email not confirmed."));
                }

                var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
                logger.LogWarning("Login failed for user {UserName} from IP {IP}.", request.UserName, ip);
                return BadRequest(new ResultModel("Invalid credentials."));
            }

            return Ok(new ResultModel { Message = "Login successful." });
        }

        /// <summary>
        /// Retrieves information about the current user.
        /// </summary>
        [Authorize]
        [HttpGet("info")]
        public async Task<ActionResult<ResultModel>> Info()
        {
            var userInfo = await authService.Info(User);
            return Ok(new ResultModel<WorkoutTrackerUserDto> { ResultObject = userInfo });
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult<ResultModel>> Logout()
        {
            logger.LogInformation("User {UserName} is logging out.", User.Identity?.Name ?? "Unknown");
            await authService.Logout();
            return Ok(new ResultModel { Message = "Logout successful." });
        }

        /// <summary>
        /// Delete the users account
        /// </summary>
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ResultModel>> Delete()
        {
            logger.LogInformation("User {UserName} is deleting their account.", User.Identity?.Name ?? "Unknown");

            await authService.Delete(User);

            return Ok(new ResultModel { Message = "Account successfully deleted." });
        }


        #region Email
        /// <summary>
        /// Verfies the email of a user.
        /// </summary>
        [HttpPost("verify-email")]
        public async Task<ActionResult<ResultModel>> VerifyEmail(VerifyEmailRequestDto request)
        {
            logger.LogInformation("User {UserId} is verifying their email.", request.UserId ?? "Unknown");

            var result = await authService.VerifyEmail(request);

            if(!result.Succeeded)
            {
                return BadRequest(new ResultModel(result.Errors.Select(x => x.Description).ToList()));
            }

            return Ok(new ResultModel { Message = "Email verified successfully." });
        }

        /// <summary>
        /// Resends the email confirmation email.
        /// </summary>
        [Authorize]
        [HttpPost("resend-confirmation-email")]
        public async Task<ActionResult<ResultModel>> ResendConfirmationEmail()
        {
            logger.LogInformation("User {UserName} is requesting resending of a confirmation email.", User.Identity?.Name ?? "Unknown");
            await authService.ResendVerificationEmail(User);
            return Ok(new ResultModel { Message = "Verification email resent successfully." });
        }

        /// <summary>
        /// Sends a link to the users new email address to confirm the change of email.
        /// </summary>
        [Authorize]
        [HttpPost("change-email")]
        public async Task<ActionResult<ResultModel>> ChangeEmail(ChangeEmailRequestDto request)
        {
            await authService.ChangeEmail(request, User);
            return Ok(new ResultModel { Message = "Confirmation email successfully sent to the new email address." });
        }

        /// <summary>
        /// Confirms the change of a users email.
        /// </summary>
        [HttpPost("confirm-change-email")]
        public async Task<ActionResult<ResultModel>> ConfirmChangeEmail(ConfirmChangeEmailRequestDto request)
        {
            logger.LogInformation("User {UserId} is verifying their email.", request.UserId ?? "Unknown");

            var result = await authService.ConfirmChangeEmail(request);

            if (!result.Succeeded)
            {
                return BadRequest(new ResultModel(result.Errors.Select(x => x.Description).ToList()));
            }

            return Ok(new ResultModel { Message = "New email verified successfully." });
        }
        #endregion


        #region Password
        /// <summary>
        /// Sends an email with a password reset link to the specified email.
        /// </summary>
        [HttpPost("forgot-password")]
        public async Task<ActionResult<ResultModel>> ForgotPassword(ForgotPasswordRequestDto request)
        {
            logger.LogInformation("User with Email {Email} is requesting a password reset link.", request.Email);

            await authService.ForgotPassword(request.Email);

            return Ok(new ResultModel { Message = "Password reset link successfully sent." });
        }

        /// <summary>
        /// Resets the password of the user.
        /// </summary>
        [HttpPost("reset-password")]
        public async Task<ActionResult<ResultModel>> ResetPassword(ResetPasswordRequestDto request)
        {
            logger.LogInformation("User with Id {UserId} is resetting their password.", request.UserId);

            var result = await authService.ResetPassword(request);

            if (!result.Succeeded)
            {
                return BadRequest(new ResultModel(result.Errors.Select(x => x.Description).ToList()));
            }

            return Ok(new ResultModel { Message = "Password successfully updated." });
        }

        /// <summary>
        /// Changes the password of the current user.
        /// </summary>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult<ResultModel>> ChangePassword(ChangePasswordRequestDto request)
        {
            logger.LogInformation("User {UserName} is changing their password.", User.Identity?.Name ?? "Unknown");

            var result = await authService.ChangePassword(request, User);

            if (!result.Succeeded)
            {
                return BadRequest(new ResultModel(result.Errors.Select(x => x.Description).ToList()));
            }

            return Ok(new ResultModel { Message = "Password successfully updated." });
        }
        #endregion
    }
}
