using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Auth
{
    /// <summary>
    /// Data Transfer Object for user login requests.
    /// </summary>
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "You must enter your username!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter your password!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
