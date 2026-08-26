namespace ShopApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ShopApplication.Interfaces.Services;
    using System.Threading.Tasks;

    namespace ShopApi.Controllers // Ваш namespace
    {
        [ApiController]
        [Route("api/[controller]")]
        public class TestEmailController : ControllerBase
        {
            private readonly IEmailService _emailService;

            public TestEmailController(IEmailService emailService)
            {
                _emailService = emailService;
            }

            [HttpPost("send-test")]
            public async Task<IActionResult> SendTestEmail([FromQuery] string toEmail)
            {
                try
                {
                    var subject = "Тестовий лист зі Swagger";
                    var body = "<h1>Привіт!</h1><p>Якщо ти бачиш це, значить EmailService налаштований правильно.</p>";

                    // Викликаємо наш сервіс
                    await _emailService.SendEmailAsync(toEmail, subject, body);

                    return Ok(new { Message = "Лист успішно відправлено! Перевірте пошту." });
                }
                catch (System.Exception ex)
                {
                    // Якщо щось піде не так (наприклад, невірний пароль SMTP), ми одразу побачимо помилку в Swagger
                    return BadRequest(new { Message = "Помилка відправки", Error = ex.Message });
                }
            }
        }
    }
}
