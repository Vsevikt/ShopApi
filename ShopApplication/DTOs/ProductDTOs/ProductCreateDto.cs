using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopApplication.DTOs.Product
{
    public class ProductCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        //[DefaultValue(null)]
        public int? CategoryId { get; set; }
    }
}
