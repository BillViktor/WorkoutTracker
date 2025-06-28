using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Auth.Email;
using WorkoutTracker.Shared.Models.Auth.Password;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.AuthViewModel
{
    public interface IAuthViewModel : IBaseViewModel
    {
        WorkoutTrackerUserDto UserInfo { get; }

        Task<bool> ChangeEmail(ChangeEmailRequestDto request);
        Task<bool> ChangePassword(ChangePasswordRequestDto request);
        Task<bool> ConfirmChangeEmail(ConfirmChangeEmailRequestDto request);
        Task<bool> ForgotPassword(ForgotPasswordRequestDto request);
        Task<bool> Login(LoginRequestDto aLoginRequestModel);
        Task<bool> Logout();
        Task<bool> Register(RegisterRequestDto request);
        Task ResendConfirmationEmail();
        Task<bool> ResetPassword(ResetPasswordRequestDto request);
        Task<bool> VerifyEmail(VerifyEmailRequestDto request);
    }
}