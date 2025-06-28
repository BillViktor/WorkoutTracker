using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Models.Auth.Email
{
    /// <summary>
    /// Represents a request to change the user's email address.
    /// </summary>
    public class ChangeEmailRequestDto
    {
        [Required(ErrorMessage = "Email is mandatory!")]
        [EmailAddress(ErrorMessage = "Email is invalid!")]
        public string NewEmail { get; set; }
    }
}
