using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
