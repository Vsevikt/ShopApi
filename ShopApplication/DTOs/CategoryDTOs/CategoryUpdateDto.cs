using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.DTOs.CategoryDTOs
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
    }
}
