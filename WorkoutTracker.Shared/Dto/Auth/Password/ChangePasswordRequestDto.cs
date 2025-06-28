using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Auth.Password
{
    public class ChangePasswordRequestDto
    {
        [Required(ErrorMessage = "You must confirm your current password!")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "You must provide a new password!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must contain: one uppercase letter, one lowercase letter, a digit, a special sign and be at least eight characters long!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "You must confirm your new password!")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match!")]
        public string ConfirmNewPassword { get; set; }
    }
}
