using ShopDomain.Models;

namespace ShopApplication.Interfaces.Repositories;

public interface IDeliveryAddressRepository
{
    Task<int> AddDeliveryAddressAsync(DeliveryAddress address, CancellationToken cancellationToken);
    Task<DeliveryAddress?> GetByIdAsync(int id, Guid userId, CancellationToken cancellationToken);
}