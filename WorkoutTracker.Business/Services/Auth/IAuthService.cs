using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Auth.Email;
using WorkoutTracker.Shared.Models.Auth.Password;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Business.Services.Auth
{
    public interface IAuthService
    {
        Task ChangeEmail(ChangeEmailRequestDto request, ClaimsPrincipal claimsPrincipal);
        Task<IdentityResult> ChangePassword(ChangePasswordRequestDto request, ClaimsPrincipal aClaimsPrincipal);
        Task<IdentityResult> ConfirmChangeEmail(ConfirmChangeEmailRequestDto confirmChangeEmailRequestDto);
        Task Delete(ClaimsPrincipal claimsPrincipal);
        Task ForgotPassword(string email);
        Task<WorkoutTrackerUserDto> Info(ClaimsPrincipal claimsPrincipal);
        Task<SignInResult> Login(LoginRequestDto loginRequest);
        Task Logout();
        Task<IdentityResult> Register(RegisterRequestDto registerRequest);
        Task ResendVerificationEmail(ClaimsPrincipal aClaimsPrincipal);
        Task<IdentityResult> ResetPassword(ResetPasswordRequestDto request);
        Task<IdentityResult> VerifyEmail(VerifyEmailRequestDto request);
    }
}