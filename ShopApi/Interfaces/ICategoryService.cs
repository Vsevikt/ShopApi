using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopDomain.Models;

namespace ShopApi.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryReadDTO> GetAllCategories();
        CategoryReadDTO? GetCategory(int id);
        CategoryReadDTO AddCategory(CategoryCreateDTO dto);
        CategoryReadDTO? UpdateCategory(int id, CategoryUpdateDTO dto);
        void DeleteCategory(int id);
        Category? SearchCategory(string? title);
        Task<int?> CreateCategoryAsync(CategoryCreateDTO dto);
    }
}
