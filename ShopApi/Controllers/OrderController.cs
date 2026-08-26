namespace ShopApi.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ShopApplication.DTOs.OrderDTOs;
    using ShopApplication.Interfaces.Repositories;
    using ShopApplication.Interfaces.Services;
    using ShopApplication.Services;
    using ShopDomain.Models;
    using ShopInfrastructure.Data;
    using ShopInfrastructure.Repositories;
    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService _orderService) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO request)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("У вашому профілі не вказана електронна пошта.");
            }

            try
            {
                var result = await _orderService.CreateOrderAsync(userEmail, request);
                return Ok(new { message = "Замовлення успішно прийнято в обробку!", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

