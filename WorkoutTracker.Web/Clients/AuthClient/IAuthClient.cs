using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.AuthClient
{
    public interface IAuthClient
    {
        Task<ResultModel<WorkoutTrackerUserDto>> Info();
        Task<ResultModel> Login(LoginRequestModel loginRequest);
        Task<ResultModel> Logout();
    }
}