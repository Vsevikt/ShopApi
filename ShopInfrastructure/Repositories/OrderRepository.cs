using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;
using ShopInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Repositories
{
    public class OrderRepository(ShopDbContext _context) : IOrderRepository
    {
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
    }
}
