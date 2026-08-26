using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.DTOs.UserDTOs;
using ShopApplication.Interfaces.Services;
using System.Text.Json;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class AuthController(IAuthService _authService, IQueueService _queueService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserCreateDTO dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user.User == null || user.Token == null || user.RefreshToken == null)
                return BadRequest("Користувач за таким email вже існує");

            Response.Cookies.Append(
                "refreshToken",
                user.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    //Expires = result.RefreshTokenExpires
                });

            await _queueService.PublishAsync("Users", user.User);

            return Ok(new { user = user.User }); // token = user.Token // refresh = user.RefreshToken
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO dto)
        {
            var user = await _authService.LoginAsync(dto.Email, dto.Password);
            if (user.User == null || user.Token == null)
                return Unauthorized("Невірний email або пароль");

            Response.Cookies.Append(
                "refreshToken",
                user.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    //Expires = result.RefreshTokenExpires
                });

            return Ok(new { user = user.User, token = user.Token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Відсутній refresh token");

            var user = await _authService.RefreshTokenAsync(refreshToken);
            if (user.User == null || user.Token == null)
                return Unauthorized("Невірний refresh token");

            Response.Cookies.Append(
                "refreshToken",
                user.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    //Expires = result.RefreshTokenExpires
                });

            return Ok(new { user = user.User }); // token = user.Token // refresh = user.RefreshToken
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO dto)
        {
            var user = await _authService.UpdateUserAsync(dto);
            if (user.User == null || user.Token == null)
                return BadRequest("Не вдалося оновити користувача");

            Response.Cookies.Append(
                "refreshToken",
                user.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    //Expires = result.RefreshTokenExpires
                });

            return Ok(new { user = user.User }); // token = user.Token // refresh = user.RefreshToken
        }

        [Authorize(Roles = "Admin,Moderator")]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest("Електронна пошта обов'язкова.");

            var result =
                await _authService.ForgotPasswordAsync(
                    dto.Email);

            if (!result)
                return BadRequest("Користувача не знайдено.");

            return Ok("Лист для скидання пароля надіслано.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> ChangeRole(Guid id, [FromBody] ChangeRoleDTO dto)
        {
            var result = await _authService.ChangeRoleAsync(id, dto.Role);

            if (!result)
                return NotFound("Користувача не знайдено.");

            return Ok("Роль успішно змінено.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] UserPasswordDTO dto)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest("Потрібен токен.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Потрібен пароль.");

            if (dto.Password != dto.ConfirmPassword)
                return BadRequest("Паролі не збігаються.");

            var result = await _authService.ResetPasswordAsync(token, dto.Password);

            if (!result)
                return BadRequest("Недійсний, термін дії минув або вже використаний токен.");

            return Ok("Пароль успішно змінено.");
        }
    }
}