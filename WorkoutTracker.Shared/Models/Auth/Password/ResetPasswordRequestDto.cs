using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Models.Auth.Password
{
    /// <summary>
    /// Represents a request to reset a user's password.
    /// </summary>
    public class ResetPasswordRequestDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Password is mandatory!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must contain: one uppercase letter, one lowercase letter, a digit, a special sign and be at least eight characters long!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password!")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
