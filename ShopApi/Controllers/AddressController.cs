using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.Commands.DeliveryAddress;
using ShopApplication.DTOs.DeliveryAddress;
using ShopApplication.Interfaces.Services;
using ShopApplication.Queries.DeliveryAddress;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AddressController(IAuthService _authService, IQueueService _queueService, IMediator _mediator, IJWTService _jwtService) : ControllerBase
    {
        [Authorize]
        [HttpPost("address")]
        public async Task<IActionResult> AddAddress([FromBody] DeliveryAddressCreateDTO dto, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized("Заявку на ідентифікатор користувача не знайдено.");

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized("Недійсний UserId.");

            var command = new AddDeliveryAddressCommand(
                userId,
                dto);

            var addressId = await _mediator.Send(
                command,
                cancellationToken);

            return Ok($"Адрес доставки успішно додався: {addressId}");
        }

        [Authorize]
        [HttpGet("address/{id:int}")]
        public async Task<IActionResult> GetAddressById(int id, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
                return Unauthorized();

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();

            var address = await _mediator.Send(new GetDeliveryAddressByIdQuery(id, userId), cancellationToken);

            if (address == null)
                return NotFound();

            return Ok(address);
        }
    }
}
