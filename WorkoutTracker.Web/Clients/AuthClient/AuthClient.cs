using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WorkoutTracker.Shared.Dto.Auth;
using WorkoutTracker.Shared.Dto.Auth.Email;
using WorkoutTracker.Shared.Dto.Auth.Password;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.AuthClient
{
    /// <summary>
    /// Client for the Auth API Controller.
    /// </summary>
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient httpClient;

        public AuthClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Login a user.
        /// </summary>
        public async Task<ResultModel> Login(LoginRequestDto loginRequest)
        {
            return await HttpRequestHelper.PostAsJsonAsync<ResultModel, LoginRequestDto>(httpClient, "login", loginRequest);
        }

        /// <summary>
        /// Get user information.
        /// </summary>
        public async Task<ResultModel<WorkoutTrackerUserDto>> Info()
        {
            return await HttpRequestHelper.GetAsync<WorkoutTrackerUserDto>(httpClient, "info");
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task<ResultModel> Logout()
        {
            return await HttpRequestHelper.PostAsync(httpClient, "logout");
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        public async Task<ResultModel> Register(RegisterRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "register", request);
        }

        /// <summary>
        /// Deletes the current user account.
        /// </summary>
        public async Task<ResultModel> Delete()
        {
            return await HttpRequestHelper.DeleteAsync(httpClient, "");
        }


        #region Email
        /// <summary>
        /// Verifies the email of a user.
        /// </summary>
        public async Task<ResultModel> VerifyEmail(VerifyEmailRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "verify-email", request);
        }

        /// <summary>
        /// Resends the email confirmation email.
        /// </summary>
        public async Task<ResultModel> ResendConfirmationEmail()
        {
            return await HttpRequestHelper.PostAsync(httpClient, "resend-confirmation-email");
        }

        /// <summary>
        /// Sends a link to the users new email address to confirm the change of email.
        /// </summary>
        [Authorize]
        public async Task<ResultModel> ChangeEmail(ChangeEmailRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "change-email", request);
        }

        /// <summary>
        /// Confirms the change of a users email.
        /// </summary>
        public async Task<ResultModel> ConfirmChangeEmail(ConfirmChangeEmailRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "confirm-change-email", request);
        }
        #endregion


        #region Password
        /// <summary>
        /// Sends an email with a password reset link to the specified email.
        /// </summary>
        public async Task<ResultModel> ForgotPassword(ForgotPasswordRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "forgot-password", request);
        }

        /// <summary>
        /// Resets the password of the user.
        /// </summary>
        public async Task<ResultModel> ResetPassword(ResetPasswordRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "reset-password", request);
        }

        /// <summary>
        /// Changes the password of the current user.
        /// </summary>
        public async Task<ResultModel> ChangePassword(ChangePasswordRequestDto request)
        {
            return await HttpRequestHelper.PostAsJsonAsync(httpClient, "change-password", request);
        }
        #endregion
    }
}
