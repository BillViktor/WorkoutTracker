using WorkoutTracker.Shared.Models.Auth;

namespace WorkoutTracker.Web.ViewModels.AuthViewModel
{
    public interface IAuthViewModel : IBaseViewModel
    {
        Task<bool> Login(LoginRequestModel aLoginRequestModel);
        Task<bool> Logout();
    }
}