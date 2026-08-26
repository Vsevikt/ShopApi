using Microsoft.Extensions.Options;
using ShopApplication.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

using ShopInfrastructure.Configuration;

namespace ShopInfrastructure.Services
{
    public class EmailService(IOptions<EmailSettings> emailOptions) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailOptions.Value;
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Неможливо відправити лист: адреса отримувача (email) відсутня або дорівнює null.", nameof(email));
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetToken)
        {
            var resetLink = $"https://localhost:7061/reset-password?token={resetToken}";
            var subject = "Відновлення паролю";
            var body = $"<p>Для відновлення паролю перейдіть за <a href='{resetLink}'>цим посиланням</a>.</p>";

            await SendEmailAsync(email, subject, body);
        }
    }
}
