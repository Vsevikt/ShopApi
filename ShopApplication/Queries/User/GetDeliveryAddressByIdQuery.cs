using MediatR;
using ShopApplication.DTOs.DeliveryAddress;
using ShopApplication.DTOs.UserDTOs;

namespace ShopApplication.Queries.DeliveryAddress;

public record GetDeliveryAddressByIdQuery(int Id, Guid UserId) : IRequest<DeliveryAddressReadDTO?>;