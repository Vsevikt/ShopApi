using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

/// <summary>
/// DTO for authentication
/// </summary>
namespace ShopApplication.DTOs.UserDTOs
{
    public class UserLoginDTO 
    { 
        [Required]
        [EmailAddress] 
        public string Email { get; set; } = null!; 

        [Required] 
        public string Password { get; set; } = null!; }
}
