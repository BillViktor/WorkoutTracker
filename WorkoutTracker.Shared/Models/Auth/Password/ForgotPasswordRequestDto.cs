using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Models.Auth.Password
{
    public class ForgotPasswordRequestDto
    {
        [Required(ErrorMessage = "Email is mandatory!")]
        [EmailAddress(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }
    }
}
