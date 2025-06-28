namespace WorkoutTracker.Shared.Models.Auth.Email
{
    /// <summary>
    /// Represents a request to verify a user's email address.
    /// </summary>
    public class VerifyEmailRequestDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
