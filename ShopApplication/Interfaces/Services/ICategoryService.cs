using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<int?> CreateCategoryAsync(CategoryCreate dto);
        Task<ICollection<CategoryRead>> GetAllCategoriesAsync();
        Task<CategoryRead?> GetCategoryByIdAsync(int id);
        Task<bool> UpdateCategoryAsync(CategoryUpdate dto);
        Task<int?> DeleteCategoryAsync(int id);
        Task<ICollection<CategoryRead>> GetCategoriesByParentAsync();
        Task<ICollection<CategoryRead>> GetCategoriesByChildAsync();
        Task<ICollection<CategoryRead>> GetCategoriesByTreeAsync();
    }
}
