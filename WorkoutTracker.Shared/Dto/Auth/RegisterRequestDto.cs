using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username is mandatory!")]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-mail ís mandatory!")]
        [EmailAddress(ErrorMessage = "E-mail is invalid!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must contain: one uppercase letter, one lowercase letter, a digit, a special sign and be at least eight characters long!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password!")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
