using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Business.Services.Auth
{
    public interface IAuthService
    {
        Task<WorkoutTrackerUserDto> Info(ClaimsPrincipal claimsPrincipal);
        Task<SignInResult> Login(LoginRequestModel loginRequest);
        Task Logout();
        Task<IdentityResult> Register(RegisterRequestModel registerRequest);
    }
}