using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs.UserDTOs;
using ShopApplication.Interfaces.Services;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO dto)

        {

            var user = await _authService.RegisterAsync(dto);
            if (user.User == null || user.Token == null)
                return BadRequest("Користувач за таким email вже існує");

            //Response.Cookies.Append("accessToken", user.Token, new CookieOptions//{//    HttpOnly = true,//    Secure = true,//    SameSite = SameSiteMode.Strict,//    Expires = DateTimeOffset.UtcNow.AddMinutes(30)//});
            return Ok(new { user = user.User, token = user.Token });

        }
    }
}
