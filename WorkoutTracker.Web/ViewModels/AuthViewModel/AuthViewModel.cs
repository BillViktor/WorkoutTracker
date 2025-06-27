using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Web.Clients.AuthClient;
using WorkoutTracker.Web.Identity;

namespace WorkoutTracker.Web.ViewModels.AuthViewModel
{
    /// <summary>
    /// ViewModel for handling authentication operations such as login and logout.
    /// </summary>
    public class AuthViewModel : BaseViewModel, IAuthViewModel
    {
        private readonly IAuthClient authClient;
        private readonly CookieAuthenticationStateProvider cookieAuthenticationStateProvider;

        public AuthViewModel(IAuthClient authClient, CookieAuthenticationStateProvider cookieAuthenticationStateProvider)
        {
            this.authClient = authClient;
            this.cookieAuthenticationStateProvider = cookieAuthenticationStateProvider;
        }

        /// <summary>
        /// Logs in a user with the provided login request model.
        /// </summary>
        public async Task<bool> Login(LoginRequestModel aLoginRequestModel)
        {
            bool sSuccess = true;
            IsBusy = true;

            var sResult = await authClient.Login(aLoginRequestModel);

            if (sResult.Success)
            {
                cookieAuthenticationStateProvider.NotifyAuthenticationStateChanged();
            }
            else
            {
                sSuccess = false;
                AppendErrorList(sResult.Errors);
            }

            IsBusy = false;
            return sSuccess;
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public async Task<bool> Logout()
        {
            bool sSuccess = true;
            IsBusy = true;

            var sResult = await authClient.Logout();

            if (sResult.Success)
            {
                cookieAuthenticationStateProvider.NotifyAuthenticationStateChanged();
            }
            else
            {
                sSuccess = false;
                AppendErrorList(sResult.Errors);
            }

            IsBusy = false;
            return sSuccess;
        }
    }
}
