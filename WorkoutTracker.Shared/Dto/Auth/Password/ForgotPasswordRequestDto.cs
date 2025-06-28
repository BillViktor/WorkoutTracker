using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Auth.Password
{
    /// <summary>
    /// Represents a request to initiate a password reset process.
    /// </summary>
    public class ForgotPasswordRequestDto
    {
        [Required(ErrorMessage = "Email is mandatory!")]
        [EmailAddress(ErrorMessage = "Email is invalid!")]
        public string Email { get; set; }
    }
}
