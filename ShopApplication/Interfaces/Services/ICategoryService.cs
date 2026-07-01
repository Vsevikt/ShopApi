using ShopApplication.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<int>? CreateCategoryAsync(CategoryCreateDTO dto);
    }
}
