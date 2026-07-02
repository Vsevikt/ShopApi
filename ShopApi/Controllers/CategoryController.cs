using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.Interfaces;
using ShopApplication.Services;
using ShopDomain.Models;
using ShopApplication.Interfaces.Services;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController(ICategoryService _categoryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            int? id = await _categoryService.CreateCategoryAsync(dto);
            return Ok($"Category created {id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById(int id, [FromBody] CategoryUpdateDTO dto)
        {
            var category = await _categoryService.UpdateCategoryAsync(dto);

            if (category == null)
                return NotFound("Category not found");

            return Ok("Category updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            var category = await _categoryService.DeleteCategoryAsync(id);

            if (category == null)
                return NotFound("Category not found");

            return Ok($"Category deleted {category}");
        }
    }
}
