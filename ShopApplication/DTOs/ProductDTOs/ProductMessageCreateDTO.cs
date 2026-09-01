using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.ProductDTOs
{
    public class ProductMessageCreateDto
    {   
        public string Email { get; set; } = string.Empty;
        public ProductMessageType Type { get; set; }
        public string Text { get; set; } = string.Empty;
        public int? Rating { get; set; }
    }
}
