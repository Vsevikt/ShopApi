using ShopApplication.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.CartDTOs
{
    public class CartReadDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        public int Quantity { get; set; }
    }
}
