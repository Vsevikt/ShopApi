using MongoDB.Driver;
using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;
using ShopInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace ShopInfrastructure.Repositories
{
    public class ProductMessageRepository(MongoDbContext _mongoDbContext) : IProductMessageRepository
    {
        public async Task AddMessageAsync(ProductMessage message)
        {
            await _mongoDbContext.ProductMessages.InsertOneAsync(message);
        }
    }
}
