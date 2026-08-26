using ShopApplication.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IJWTService
    {
        string GenerateAccessToken(UserLoginDTO userLoginDto, string role);
        (string, int) GenerateRefreshToken();
    }
}
