using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShopApplication.Interfaces.Services;
using ShopInfrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Services
{
    public class MongoDbService(IOptions<MongoDbSettings> settings) : IMongoDbService
    {
        private readonly IMongoDatabase _database =
            new MongoClient(settings.Value.ConnectionString)
                .GetDatabase(settings.Value.DatabaseName);

        public IMongoCollection<Product> GetCollection()
        {
            return _database.GetCollection<Product>(
                settings.Value.ProductsCollectionName);
        }
    }
}
