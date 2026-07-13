using ShopApplication.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserReadDTO?> RegisterAsync(UserCreateDTO dto);
    }
}
