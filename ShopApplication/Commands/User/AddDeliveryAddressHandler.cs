using MediatR;
using ShopApplication.Commands.DeliveryAddress;
using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;

namespace ShopApplication.DeliveryAddresses.Handlers.AddDeliveryAddress;

public class AddDeliveryAddressHandler(IDeliveryAddressRepository _repository) : IRequestHandler<AddDeliveryAddressCommand, int>
{
    public async Task<int> Handle(AddDeliveryAddressCommand command, CancellationToken cancellationToken)
    {
        var address = new DeliveryAddress
        {
            UserId = command.UserId,
            City = command.DTO.City,
            Street = command.DTO.Street,
            House = command.DTO.House,
            Apartment = command.DTO.Apartment,
            PostalCode = command.DTO.PostalCode
        };

        return await _repository.AddDeliveryAddressAsync(address, cancellationToken);
    }
}