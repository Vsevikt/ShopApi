//using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.Interfaces;
using ShopDomain.Models;
using ShopInfrastructure.Data;

namespace ShopApplication.Services
{
    public class CategoryService : ICategoryService
    {
        private List<Category> _categories = new();

        private readonly ShopDbContext _context;

        public CategoryService()
        {
            //_categories.Add(new Category()
            //{
            //    Id = 1,
            //    Name = "Electronics",
            //});
            //_categories.Add(new Category()
            //{
            //    Id = 2,
            //    Name = "Fashion",
            //});
            //_categories.Add(new Category()
            //{
            //    Id = 3,
            //    Name = "Home",
            //});
        }

        public CategoryService(ShopDbContext context)
        {
            _context = context;
        }

        public CategoryReadDto? GetCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Name
            };
        }

        public CategoryReadDto AddCategory(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Slug = dto.Slug
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
        }

        public List<CategoryReadDto> GetAllCategories()
        {
            var categories = _context.Categories
                .Select(c => new CategoryReadDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Slug = c.Slug
                })
                .ToList();

            return categories;
        }

        public CategoryReadDto? UpdateCategory(int id, CategoryUpdateDto dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            category.Name = dto.Name;
            category.Slug = dto.Slug;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return new CategoryReadDto
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            };
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public Category? SearchCategory(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return null;

            return _categories.FirstOrDefault(c => c.Name != null && c.Name.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
