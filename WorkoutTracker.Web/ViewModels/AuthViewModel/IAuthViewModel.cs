using WorkoutTracker.Shared.Models.Auth;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.AuthViewModel
{
    public interface IAuthViewModel : IBaseViewModel
    {
        Task<bool> Login(LoginRequestModel aLoginRequestModel);
        Task<bool> Logout();
    }
}