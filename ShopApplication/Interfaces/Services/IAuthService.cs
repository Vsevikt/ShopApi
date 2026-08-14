using ShopApplication.DTOs.UserDTOs;
using ShopDomain.Enums;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IAuthService
    {
        Task<(UserReadDTO? User, string? Token, string? RefreshToken)> RegisterAsync(UserCreateDTO dto);
        Task<(UserReadDTO? User, string? Token, string? RefreshToken)> LoginAsync(string email, string password);
        Task<(UserReadDTO? User, string? Token, string? RefreshToken)> RefreshTokenAsync(string refreshToken);
        Task<(UserReadDTO? User, string? Token, string? RefreshToken)> UpdateUserAsync(UserUpdateDTO dto);
        Task<bool> ChangeRoleAsync(Guid userId, UserRole role);
        Task<bool> CheckEmailAsync(string email);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string token, string newPassword);
    }
}
