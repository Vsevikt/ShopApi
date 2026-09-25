using Microsoft.EntityFrameworkCore;
using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;
using ShopInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Repositories
{
    public class DeliveryAddressRepository(ShopDbContext _context, MongoDbContext _mongoDbContext) : IDeliveryAddressRepository
    {
        public async Task<int> AddDeliveryAddressAsync(DeliveryAddress address, CancellationToken cancellationToken)
        {
            await _context.DeliveryAddresses.AddAsync(address, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return address.Id;
        }

        public async Task<DeliveryAddress?> GetByIdAsync(int id, Guid userId, CancellationToken cancellationToken)
        {
            return await _context.DeliveryAddresses
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }
    }
}
