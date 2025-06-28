using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace WorkoutTracker.Business.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> logger;
        private readonly IConfiguration configuration;
        public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        /// <summary>
        /// Sends an email to the specified recipient with the given header and body.
        /// </summary>
        public async Task SendEmail(string recipient, string header, string body)
        {
            SmtpClient smtpClient = new SmtpClient
            {
                Port = Convert.ToInt32(configuration["SmtpSettings:Port"]),
                Host = configuration["SmtpSettings:Host"],
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(configuration["SmtpSettings:Username"], configuration["SmtpSettings:Password"])
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(configuration["SmtpSettings:Username"], "WorkoutTracker"),
                Subject = header,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(recipient);

            await smtpClient.SendMailAsync(mail);
        }
    }
}
