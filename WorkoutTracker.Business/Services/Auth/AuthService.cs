using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Claims;
using WorkoutTracker.Business.Services.Email;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Business.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<WorkoutTrackerUserModel> userManager;
        private readonly SignInManager<WorkoutTrackerUserModel> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<WorkoutTrackerUserModel> userManager, SignInManager<WorkoutTrackerUserModel> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.emailService = emailService;
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        public async Task<IdentityResult> Register(RegisterRequestModel registerRequest)
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
                string emailToken = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                string frontEndUrl = configuration["WorkoutTrackerWebUrl"] ?? "";
                var encodedToken = WebUtility.UrlEncode(emailToken);
                string confirmationLink = $"{frontEndUrl}confirm-email?userId={newUser.Id}&token={encodedToken}";

                await emailService.SendEmail(registerRequest.Email, "Workout Tracker - Confirm your email", EmailHelper.GetConfirmationEmail(confirmationLink));
            }

            return createUserResult;
        }

        /// <summary>
        /// Logins a user
        /// </summary>
        public async Task<SignInResult> Login(LoginRequestModel loginRequest)
        {
            var user = await userManager.FindByNameAsync(loginRequest.UserName);

            if (user != null && !await userManager.IsEmailConfirmedAsync(user))
            {
                return SignInResult.NotAllowed;
            }

            return await signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, loginRequest.RememberMe, true);
        }

        public async Task<WorkoutTrackerUserDto> Info(ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.GetUserAsync(claimsPrincipal);
            var identity = (ClaimsIdentity)claimsPrincipal.Identity;

            var roles = identity.FindAll(identity.RoleClaimType).Select(c => new RoleClaimModel
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
                ProfilePictureUrl = $"{configuration["WorkoutTrackerApiBaseUrl"]}images/user/{user.ProfilePicturePath ?? "default.jpg"}",
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
    }
}
