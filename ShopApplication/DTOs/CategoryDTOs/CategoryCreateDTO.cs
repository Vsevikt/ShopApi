using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopApplication.DTOs.Category
{
    public class CategoryCreateDTO
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }
}
