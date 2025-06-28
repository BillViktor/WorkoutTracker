namespace WorkoutTracker.Business.Services.Email
{
    /// <summary>
    /// Helper class for generating email templates.
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Generates a confirmation email template with the provided confirmation link.
        /// </summary>
        public static string GetConfirmationEmail(string confirmationLink)
        {
            string body = $@"
                        <h1>Welcome to WorkoutTracker!</h1>
                        <p>
                            Verify your email address to complete your registration.
                        </p>
                        <a href=""{confirmationLink}"" class=""button"">Verify email</a>
                        <p>
                            The link is valid for an hour.
                        </p>
                        <p class=""footer"">
                            Best Regards,<br>
                            <strong>WorkoutTracker</strong>
                        </p>
                        ";

            return GetEmailTemplate(body);
        }

        /// <summary>
        /// Generates an email template for account deletion confirmation.
        /// </summary>
        public static string GetAccountDeletedConfirmation()
        {
            string body = $@"
                        <h1>Workout Tracker - Account Deleted</h1>
                        <p>
                            This is a confirmation that your account is deleted. We hope to see you again!
                        </p>
                        <p class=""footer"">
                            Best Regards,<br>
                            <strong>WorkoutTracker</strong>
                        </p>
                        ";

            return GetEmailTemplate(body);
        }

        /// <summary>
        /// Generates an email template for changing the email address.
        /// </summary>
        public static string GetChangeEmailEmail(string confirmationLink)
        {
            string body = $@"
                        <h1>Change Email</h1>
                        <p>
                           Confirm your new email by clicking the link below.
                        </p>
                        <a href=""{confirmationLink}"" class=""button"">Change Email</a>
                        <p>
                            The link is valid for an hour.
                        </p>
                        <p class=""footer"">
                            Best Regards,<br>
                            <strong>WorkoutTracker</strong>
                        </p>
                        ";

            return GetEmailTemplate(body);
        }

        /// <summary>
        /// Generates an email template for resetting the password with the provided change password link.
        /// </summary>
        public static string GetResetPasswordEmail(string changePasswordLink)
        {
            string body = $@"
                        <h1>WorkoutTracker - Reset Password</h1>
                        <p>
                            Reset your password by clicking the link below.
                        </p>
                        <a href=""{changePasswordLink}"" class=""button"">Reset Password</a>
                        <p>
                            The link is valid for an hour.
                        </p>
                        <p class=""footer"">
                            Best Regards,<br>
                            <strong>WorkoutTracker</strong>
                        </p>
                        ";

            return GetEmailTemplate(body);
        }

        /// <summary>
        /// Private helper method that returns the email template with the provided body.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        private static string GetEmailTemplate(string body)
        {
            return $@"<!DOCTYPE html>
                <html lang=""sv"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f9f9f9;
                            margin: 0;
                            padding: 0;
                        }}
                        .email-container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background: #ffffff;
                            padding: 20px;
                            border: 1px solid #e0e0e0;
                            border-radius: 8px;
                            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                            text-align: center;
                        }}
                        h1 {{
                            color: #333333;
                        }}
                        p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        a.button {{
                            display: inline-block;
                            margin-top: 10px;
                            padding: 10px 20px;
                            color: #ffffff;
                            background-color: #007BFF;
                            text-decoration: none;
                            border-radius: 5px;
                            font-size: 16px;
                        }}
                        a.button:hover {{
                            background-color: #0056b3;
                        }}
                        .footer {{
                            margin-top: 20px;
                            font-size: 14px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""email-container"">
                        {body}
                    </div>
                </body>
                </html>";
        }
    }
}
