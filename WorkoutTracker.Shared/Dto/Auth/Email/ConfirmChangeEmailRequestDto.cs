using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Shared.Dto.Auth.Email
{
    /// <summary>
    /// Represents a request to confirm a change of email address.
    /// </summary>
    public class ConfirmChangeEmailRequestDto
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}
