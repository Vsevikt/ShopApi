using Microsoft.AspNetCore.Mvc;
using ShopApi.Filters;
using ShopDomain.Models;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    [UserCheckFilter]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            return Ok(user);
        }
    }
}




