using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs.CartDTOs;
using ShopApplication.Interfaces.Services;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController(ICartService _cartService, IConfiguration _configuration) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartCreateDTO dto)
        {
            var id = await _cartService.CreateCartAsync(dto);
            return Ok($"Cart created {id}");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById([FromRoute] int id)
        {
            var cart = await _cartService.GetCartByIdAsync(id);
            if (cart == null)
                return NotFound($"Cart with id {id} not found.");
            return Ok(cart);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts = await _cartService.GetAllCartsAsync();
            return Ok(carts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart([FromRoute] int id, [FromBody] CartUpdateDTO dto)
        {
            var updated = await _cartService.UpdateCartAsync(id, dto.Quantity);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] int id)
        {
            var deleted = await _cartService.DeleteCartAsync(id);
            return Ok($"Cart deleted {id}");
        }
    }
}
