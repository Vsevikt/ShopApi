using ShopApplication.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendPasswordResetEmailAsync(string email, string resetToken)
        {
            var resetLink = $"https://localhost:7061/reset-password?token={resetToken}";
            await Task.CompletedTask;
        }
    }
}
