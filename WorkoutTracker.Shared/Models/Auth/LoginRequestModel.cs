using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Models.Auth
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "You must enter your username!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter your password!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
