using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.UserDTOs
{
    public class UserPasswordDTO
    {
        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
