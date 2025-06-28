using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using WorkoutTracker.API.Models.Exceptions;
using WorkoutTracker.Business.Services.Email;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Auth.Email;
using WorkoutTracker.Shared.Models.Auth.Password;

namespace WorkoutTracker.Business.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<WorkoutTrackerUserModel> userManager;
        private readonly SignInManager<WorkoutTrackerUserModel> signInManager;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        private readonly IMemoryCache memoryCache;

        public AuthService(UserManager<WorkoutTrackerUserModel> userManager, SignInManager<WorkoutTrackerUserModel> signInManager, IConfiguration configuration, IEmailService emailService, IMemoryCache memoryCache)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.emailService = emailService;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        public async Task<IdentityResult> Register(RegisterRequestDto registerRequest)
        {
            WorkoutTrackerUserModel newUser = new WorkoutTrackerUserModel
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email,
                LockoutEnabled = true
            };

            var createUserResult = await userManager.CreateAsync(newUser, registerRequest.Password);

            //Send a verification email
            if (createUserResult.Succeeded)
            {
                await SendVerificationEmail(newUser);
            }

            return createUserResult;
        }

        /// <summary>
        /// Logins a user
        /// </summary>
        public async Task<SignInResult> Login(LoginRequestDto loginRequest)
        {
            var user = await userManager.FindByNameAsync(loginRequest.UserName);

            return await signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, loginRequest.RememberMe, true);
        }

        /// <summary>
        /// Returns information about the currently logged-in user
        /// </summary>
        public async Task<WorkoutTrackerUserDto> Info(ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var identity = (ClaimsIdentity)claimsPrincipal.Identity;

            var roles = identity.FindAll(identity.RoleClaimType).Select(c => new RoleClaimDto
            {
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            }).ToList();

            return new WorkoutTrackerUserDto
            {
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                RoleClaims = roles,
                UserName = user.UserName,
            };
        }

        /// <summary>
        /// Logs out a user
        /// </summary>
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        /// <summary>
        /// Deletes the current user
        /// </summary>
        public async Task Delete(ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var result  = await userManager.DeleteAsync(user);

            if(!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await emailService.SendEmail(user.Email, "Workout Tracker - Account Deleted", EmailHelper.GetAccountDeletedConfirmation());

            await Logout();
        }

        #region Email
        /// <summary>
        /// Sends a verification email to the user
        /// </summary>
        private async Task SendVerificationEmail(WorkoutTrackerUserModel user)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            string frontEndUrl = configuration["WorkoutTrackerWebUrl"] ?? "";
            string confirmationLink = $"{frontEndUrl}confirm-email?userId={user.Id}&token={encodedToken}";
            await emailService.SendEmail(user.Email, "Workout Tracker - Confirm your email", EmailHelper.GetConfirmationEmail(confirmationLink));
        }

        /// <summary>
        /// Resends a verification email to the user
        /// </summary>
        public async Task ResendVerificationEmail(ClaimsPrincipal aClaimsPrincipal)
        {
            var user = await userManager.GetUserAsync(aClaimsPrincipal);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.EmailConfirmed)
            {
                throw new EmailAlreadyConfirmedException();
            }

            await SendVerificationEmail(user);
        }

        /// <summary>
        /// Verifies the user's email address using the provided token
        /// </summary>
        public async Task<IdentityResult> VerifyEmail(VerifyEmailRequestDto request)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.EmailConfirmed)
            {
                throw new EmailAlreadyConfirmedException();
            }

            return await userManager.ConfirmEmailAsync(user, request.Token);
        }

        /// <summary>
        /// Changes the user's email address and sends a confirmation email to the new address.
        /// </summary>
        public async Task ChangeEmail(ChangeEmailRequestDto request, ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var token = await userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
            var encodedToken = WebUtility.UrlEncode(token);

            string link = configuration["WorkoutTrackerWebUrl"] + $"change-email?userId={user.Id}&token={encodedToken}";

            //Save the users new email in the cache for later confirmation, it's valid as long as the token
            memoryCache.Set(user.Id, request.NewEmail, TimeSpan.FromHours(1));

            await emailService.SendEmail(request.NewEmail, "WorkoutTracker - Confirm New Email", EmailHelper.GetChangeEmailEmail(link));
        }

        /// <summary>
        /// Confirms the change of a user's email address using the provided token.
        /// </summary>
        public async Task<IdentityResult> ConfirmChangeEmail(ConfirmChangeEmailRequestDto request)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if(user == null)
            {
                throw new UserNotFoundException();
            }

            //Get the email from the cache
            if (!memoryCache.TryGetValue(user.Id, out string newEmail))
            {
                throw new InvalidOperationException("The email change request has expired or is invalid.");
            }

            var result = await userManager.ChangeEmailAsync(user, newEmail, request.Token);

            //If the email change was successful, update the user's email confirmed status
            if (result.Succeeded && !user.EmailConfirmed)
            {
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            }

            return result;
        }
        #endregion


        #region Password
        /// <summary>
        /// Sends a password reset link to the user's email address.
        /// </summary>
        public async Task ForgotPassword(string email)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            string encodedToken = WebUtility.UrlEncode(token);
            string link = configuration["WorkoutTrackerWebUrl"] + $"reset-password?userId={user.Id}&token={encodedToken}";

            await emailService.SendEmail(user.Email, "WorkoutTracker - Reset Password", EmailHelper.GetResetPasswordEmail(link));
        }

        /// <summary>
        /// Resets the password of the user using the provided token and new password.
        /// </summary>
        public async Task<IdentityResult> ResetPassword(ResetPasswordRequestDto request)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return await userManager.ResetPasswordAsync(user, request.Token, request.Password);
        }

        /// <summary>
        /// Changes the password of the current user using the provided current and new passwords.
        /// </summary>
        public async Task<IdentityResult> ChangePassword(ChangePasswordRequestDto request, ClaimsPrincipal aClaimsPrincipal)
        {
            var user = await userManager.GetUserAsync(aClaimsPrincipal);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        }
        #endregion
    }
}
