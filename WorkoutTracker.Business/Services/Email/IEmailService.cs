namespace WorkoutTracker.Business.Services.Email
{
    /// <summary>
    /// Interface for email services.
    /// </summary>
    public interface IEmailService
    {
        Task SendEmail(string recipient, string header, string body);
    }
}