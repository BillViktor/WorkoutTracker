using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Result;

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
        public async Task<ResultModel> Login(LoginRequestModel loginRequest)
        {
            return await HttpRequestHelper.PostAsJsonAsync<ResultModel, LoginRequestModel>(httpClient, "login", loginRequest);
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
    }
}
