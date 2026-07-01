using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopDomain.Models;

namespace ShopApplication.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();
        CategoryReadDto? GetCategory(int id);
        CategoryReadDto AddCategory(CategoryCreateDto dto);
        CategoryReadDto? UpdateCategory(int id, CategoryUpdateDto dto);
        void DeleteCategory(int id);
        Category? SearchCategory(string? title);
        
    }
}
