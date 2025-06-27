using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.Auth;
using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.API.Controllers
{
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
        public async Task<ActionResult<ResultModel>> Register(RegisterRequestModel registerRequest)
        {
            var result = await authService.Register(registerRequest);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                logger.LogWarning("User registration failed for {Email}. Errors: {Errors}", registerRequest.Email, errors);
                return BadRequest(new ResultModel(errors));
            }

            logger.LogInformation("User {Email} registered successfully.", registerRequest.Email);
            return Ok(new ResultModel { Message = "User registered successfully." });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<ResultModel>> Login(LoginRequestModel loginRequest)
        {
            var result = await authService.Login(loginRequest);

            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    logger.LogWarning("User {UserName} is not allowed to login. Email not confirmed.", loginRequest.UserName);
                    return BadRequest(new ResultModel("Email not confirmed."));
                }

                var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
                logger.LogWarning("Login failed for user {UserName} from IP {IP}.", loginRequest.UserName, ip);
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
    }
}
