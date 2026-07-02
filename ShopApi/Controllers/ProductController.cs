using Microsoft.AspNetCore.Mvc;
using ShopApi.Filters;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.DTOs.Product;
using ShopApplication.Interfaces.Services;
using ShopApplication.Services;
using ShopDomain.Models;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            int? id = await _productService.CreateProductAsync(dto);
            return Ok($"Product created {id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductById(int id, [FromBody] ProductUpdateDTO dto)
        {
            var product = await _productService.UpdateProductAsync(dto);

            if (product == null)
                return NotFound("Product not found");

            return Ok("Product updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var product = await _productService.DeleteProductAsync(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok($"Product deleted {product}");
        }
    }
}
