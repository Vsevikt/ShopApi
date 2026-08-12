//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.AspNetCore.Identity;

//namespace ShopInfrastructure.Data
//{
//    public static class AdminSeeder
//    {
//        public static async Task SeedAsync(
//            UserManager<ApplicationUser> userManager,
//            RoleManager<IdentityRole> roleManager)
//        {
//            const string adminRole = "Admin";

//            // Створюємо роль Admin, якщо її ще немає
//            if (!await roleManager.RoleExistsAsync(adminRole))
//            {
//                await roleManager.CreateAsync(
//                    new IdentityRole(adminRole));
//            }

//            // Перевіряємо, чи існує хоча б один Admin
//            var admins = await userManager.GetUsersInRoleAsync(adminRole);

//            if (admins.Count > 0)
//            {
//                return;
//            }

//            // Створюємо першого Admin
//            var admin = new ApplicationUser
//            {
//                UserName = "admin",
//                Email = "admin@example.com",
//                EmailConfirmed = true
//            };

//            var result = await userManager.CreateAsync(
//                admin,
//                "AdminPassword123!");

//            if (!result.Succeeded)
//            {
//                throw new Exception(
//                    "Не вдалося створити першого адміністратора.");
//            }

//            await userManager.AddToRoleAsync(
//                admin,
//                adminRole);
//        }
//    }
//}
