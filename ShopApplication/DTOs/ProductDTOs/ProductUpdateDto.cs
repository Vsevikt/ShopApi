using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.Product
{
    public class ProductUpdateDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
