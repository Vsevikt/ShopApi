using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApi.Interfaces;
using ShopApi.Services;
using ShopDomain.Models;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController(ICategoryService _categoryService) : ControllerBase
    {
        [HttpGet]
        public List<CategoryReadDTO> GetCategories()
        {
            return _categoryService.GetAllCategories();
        }

        /// <summary>
        /// Получить товар по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <returns>Объект товара.</returns>
        [HttpGet("{id}")]
        public IActionResult GetCategoryById([FromRoute] int id)
        {
            var category = _categoryService.GetCategory(id);

            if (category == null)
                return NotFound("Category not found");

            if (string.IsNullOrWhiteSpace(category.Name))
                return BadRequest("Category null");

            //if (category.IsShow)
            //{
            //    return BadRequest("Already viewed");
            //}

            //category.IsShow = true;
            //_categoryService.UpdateCategory(id, category);

            return Ok(category);
        }

        //[HttpPost]
        //public IActionResult AddNewCategory([FromBody] CategoryCreateDTO dto)
        //{
        //    if (string.IsNullOrWhiteSpace(dto?.Name))
        //        return BadRequest("Product null");

        //    //var categories = _categoryService.GetAllCategories();
        //    //int nextId = categories.Max(p => p.Id) + 1;
        //    //dto.Id = nextId;

        //    _categoryService.AddCategory(dto);
        //    return Created();
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            //TODO: Created statusint?
            int? id = await _categoryService.CreateCategoryAsync(dto);
            return Ok($"Category created {id}"); //200 status}
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateCategoryById(int id, [FromBody] CategoryUpdateDTO dto)
        //{
        //    var categories = _categoryService.GetAllCategories();
        //    var findCategory = categories.FirstOrDefault(c => c.Id == id);

        //    if (findCategory == null)
        //        return NotFound("Category not found");

        //    if (string.IsNullOrWhiteSpace(findCategory.Name))
        //        return BadRequest("Category null");

        //    findCategory.Name = dto.Name;

        //    _categoryService.UpdateCategory(id, dto);
        //    return Ok(findCategory);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteCategoryById([FromRoute] int id)
        //{
        //    var categories = _categoryService.GetAllCategories();
        //    var findCategory = categories.FirstOrDefault(c => c.Id == id);

        //    if (findCategory == null)
        //        return NotFound("Category not found");

        //    if (string.IsNullOrWhiteSpace(findCategory.Name))
        //        return BadRequest("Category null");

        //    _categoryService.DeleteCategory(id);
        //    return NoContent();
        //}

        //[HttpGet("search")]
        //public IActionResult SearchItem([FromQuery] string? title)
        //{
        //    if (string.IsNullOrWhiteSpace(title))
        //        return BadRequest("Title is required");

        //    var categories = _categoryService.GetAllCategories();

        //    var category = categories.FirstOrDefault(c => c.Name != null && c.Name.Equals(title, StringComparison.OrdinalIgnoreCase));

        //    if (category == null)
        //        return NotFound("Category not found");

        //    return Ok(category);
        //}
    }
}
