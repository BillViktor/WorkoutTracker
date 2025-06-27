
namespace WorkoutTracker.Business.Services.Email
{
    public interface IEmailService
    {
        Task SendEmail(string recipient, string header, string body);
    }
}