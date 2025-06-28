using WorkoutTracker.Shared.Dto.Auth;
using WorkoutTracker.Shared.Dto.Auth.Email;
using WorkoutTracker.Shared.Dto.Auth.Password;
using WorkoutTracker.Web.Clients.AuthClient;
using WorkoutTracker.Web.Identity;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.AuthViewModel
{
    /// <summary>
    /// ViewModel for handling authentication operations such as login and logout.
    /// </summary>
    public class AuthViewModel : BaseViewModel, IAuthViewModel
    {
        private readonly IAuthClient authClient;
        private readonly CookieAuthenticationStateProvider cookieAuthenticationStateProvider;

        public WorkoutTrackerUserDto UserInfo => cookieAuthenticationStateProvider.UserInfo;

        public AuthViewModel(IAuthClient authClient, CookieAuthenticationStateProvider cookieAuthenticationStateProvider)
        {
            this.authClient = authClient;
            this.cookieAuthenticationStateProvider = cookieAuthenticationStateProvider;
        }

        /// <summary>
        /// Logs in a user with the provided login request model.
        /// </summary>
        public async Task<bool> Login(LoginRequestDto aLoginRequestModel)
        {
            bool success = true;
            IsBusy = true;

            var result = await authClient.Login(aLoginRequestModel);

            if (result.Success)
            {
                cookieAuthenticationStateProvider.NotifyAuthenticationStateChanged();
            }
            else
            {
                success = false;
                AppendErrorList(result.Errors);
            }

            IsBusy = false;
            return success;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task<bool> Logout()
        {
            bool success = true;
            IsBusy = true;

            var result = await authClient.Logout();

            if (result.Success)
            {
                cookieAuthenticationStateProvider.NotifyAuthenticationStateChanged();
            }
            else
            {
                success = false;
                AppendErrorList(result.Errors);
            }

            IsBusy = false;
            return success;
        }

        /// <summary>
        /// Registers a new user with the provided registration request model.
        /// </summary>
        public async Task<bool> Register(RegisterRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.Register(request),
                AppendErrorList,
                null,
                null,
                busy => IsBusy = busy);
        }


        #region Password
        /// <summary>
        /// Initiates a password reset process by sending a forgot password request.
        /// </summary>
        public async Task<bool> ForgotPassword(ForgotPasswordRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.ForgotPassword(request),
                AppendErrorList,
                SuccessMessages.Add,
                "Password reset email sent successfully if account with specified email exists.",
                busy => IsBusy = busy);
        }

        /// <summary>
        /// Resets the user's password using the provided reset password request model.
        /// </summary>
        public async Task<bool> ResetPassword(ResetPasswordRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.ResetPassword(request),
                AppendErrorList,
                SuccessMessages.Add,
                "Password successfully updated.",
                busy => IsBusy = busy);
        }

        /// <summary>
        /// Changes the user's password using the provided change password request model.
        /// </summary>
        public async Task<bool> ChangePassword(ChangePasswordRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.ChangePassword(request),
                AppendErrorList,
                SuccessMessages.Add,
                "Password successfully updated.",
                busy => IsBusy = busy);
        }
        #endregion


        #region Email
        /// <summary>
        /// Resends the confirmation email to the user.
        /// </summary>
        public async Task ResendConfirmationEmail()
        {
            await ResultHandler.HandleAsync(
                authClient.ResendConfirmationEmail(),
                AppendErrorList,
                SuccessMessages.Add,
                "Verification email sent successfully.",
                busy => IsBusy = busy);
        }

        /// <summary>
        /// Verifies the user's email address.
        /// </summary>
        public async Task<bool> VerifyEmail(VerifyEmailRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.VerifyEmail(request),
                AppendErrorList,
                SuccessMessages.Add,
                "Email successfully verified.",
                busy => IsBusy = busy);
        }

        /// <summary>
        /// Changes the user's email using the provided change email request model.
        /// </summary>
        public async Task<bool> ChangeEmail(ChangeEmailRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.ChangeEmail(request),
                AppendErrorList,
                SuccessMessages.Add,
                $"A confirmation email has been sent to {request.NewEmail}.",
                busy => IsBusy = busy);
        }

        /// <summary>
        /// Confirms the change of the user's email address using the provided confirmation request model.
        /// </summary>
        public async Task<bool> ConfirmChangeEmail(ConfirmChangeEmailRequestDto request)
        {
            return await ResultHandler.HandleAsync(
                authClient.ConfirmChangeEmail(request),
                AppendErrorList,
                SuccessMessages.Add,
                "Email successfully updated.",
                busy => IsBusy = busy);
        }
        #endregion
    }
}
