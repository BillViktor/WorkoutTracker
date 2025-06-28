using WorkoutTracker.Shared.Dto.Auth;
using WorkoutTracker.Shared.Dto.Auth.Email;
using WorkoutTracker.Shared.Dto.Auth.Password;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.AuthClient
{
    /// <summary>
    /// Interface for authentication-related operations in the Workout Tracker application.
    /// </summary>
    public interface IAuthClient
    {
        Task<ResultModel> ChangeEmail(ChangeEmailRequestDto request);
        Task<ResultModel> ChangePassword(ChangePasswordRequestDto request);
        Task<ResultModel> ConfirmChangeEmail(ConfirmChangeEmailRequestDto request);
        Task<ResultModel> Delete();
        Task<ResultModel> ForgotPassword(ForgotPasswordRequestDto request);
        Task<ResultModel<WorkoutTrackerUserDto>> Info();
        Task<ResultModel> Login(LoginRequestDto loginRequest);
        Task<ResultModel> Logout();
        Task<ResultModel> Register(RegisterRequestDto request);
        Task<ResultModel> ResendConfirmationEmail();
        Task<ResultModel> ResetPassword(ResetPasswordRequestDto request);
        Task<ResultModel> VerifyEmail(VerifyEmailRequestDto request);
    }
}