using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<int?> AddCategoryAsync(Category category);
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int id);
        Task<bool> EditCategoryAsync(Category category);
        Task<int?> RemoveCategoryAsync(int id);
    }
}
