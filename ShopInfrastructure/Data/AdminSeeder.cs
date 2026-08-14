using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopApplication.Interfaces.Helpers;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Data
{
    public static class AdminSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ShopDbContext>();
            var hashHelper = serviceProvider.GetRequiredService<IHashHelper>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            bool adminExists = await context.Users.AnyAsync(u => u.Role == UserRole.Admin);

            if (!adminExists)
            {
                string adminEmail = config["SuperAdmin:Email"] ?? "admin@system.com";
                string adminPassword = config["SuperAdmin:Password"] ?? "Admin123!";

                var adminUser = new User
                {
                    Email = adminEmail,
                    PasswordHash = hashHelper.Hash(adminPassword),
                    Role = UserRole.Admin
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
