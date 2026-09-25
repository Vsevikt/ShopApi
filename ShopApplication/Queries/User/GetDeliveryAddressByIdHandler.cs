using MediatR;
using ShopApplication.DTOs.DeliveryAddress;
using ShopApplication.DTOs.UserDTOs;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Queries.DeliveryAddress;

namespace ShopApplication.Handlers.DeliveryAddress;

public class GetDeliveryAddressByIdHandler(IDeliveryAddressRepository _repository)  : IRequestHandler<GetDeliveryAddressByIdQuery, DeliveryAddressReadDTO?>
{
    public async Task<DeliveryAddressReadDTO?> Handle(GetDeliveryAddressByIdQuery query, CancellationToken cancellationToken)
    {
        var address = await _repository.GetByIdAsync(query.Id, query.UserId, cancellationToken);

        if (address == null)
            return null;

        return new DeliveryAddressReadDTO
        {
            Id = address.Id,
            City = address.City,
            Street = address.Street,
            House = address.House,
            Apartment = address.Apartment,
            PostalCode = address.PostalCode
        };
    }
}