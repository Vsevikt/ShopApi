using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.Category
{
    public class CategoryReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}
