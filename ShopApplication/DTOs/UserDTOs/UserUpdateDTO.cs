using ShopDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopApplication.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(5)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        //[Required]
        [MinLength(5)]
        public string Password { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;

        [RegularExpression(@"^\+?[0-9\s\-()]{10,20}$")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
