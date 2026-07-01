using Microsoft.AspNetCore.Mvc;
using ShopApi.Filters;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.Product;
using ShopApi.Interfaces;
using ShopDomain.Models;
//using ShopApp.Services;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [LogActionFilter]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public List<ProductReadDTO> GetProducts()
        {
            return _productService.GetAllProducts();
        }

        /// <summary>
        /// Получить товар по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <returns>Объект товара.</returns>
        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
                return NotFound("Product not found");

            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Product null");

            //if (product.IsShow)
            //{
            //    return BadRequest("Already viewed");
            //}

            //product.IsShow = true;
            //_productService.UpdateProduct(id, product);

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddNewProduct([FromBody] ProductCreateDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto?.Name))
                return BadRequest("Product null");

            //var products = _productService.GetAllProducts();
            //int nextId = products.Max(p => p.Id) + 1;
            //product.Id = nextId;

            _productService.AddProduct(dto);
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductById(int id, [FromBody] ProductUpdateDTO dto) 
        {
            var products = _productService.GetAllProducts();
            var findProduct = products.FirstOrDefault(p => p.Id == id);

            if (findProduct == null)
                return NotFound("Product not found");

            if (string.IsNullOrWhiteSpace(findProduct.Name))
                return BadRequest("Product null");

            findProduct.Name = dto.Name;
            findProduct.Price = dto.Price;

            _productService.UpdateProduct(id, dto);
            return Ok(findProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductById([FromRoute] int id)
        {
            var products = _productService.GetAllProducts();
            var findProduct = products.FirstOrDefault(p => p.Id == id);

            if (findProduct == null)
                return NotFound("Product not found");

            if (string.IsNullOrWhiteSpace(findProduct.Name))
                return BadRequest("Product null");

            _productService.DeleteProduct(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchItem([FromQuery] string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title is required");

            var products = _productService.GetAllProducts();

            var product = products.FirstOrDefault(p => p.Name != null && p.Name.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }
    }
}
