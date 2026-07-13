using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs.UserDTOs;
using ShopApplication.Interfaces.Services;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
