using MediatR;
using ShopApplication.DTOs.DeliveryAddress;

namespace ShopApplication.Commands.DeliveryAddress;

public record AddDeliveryAddressCommand(Guid UserId, DeliveryAddressCreateDTO DTO) : IRequest<int>;