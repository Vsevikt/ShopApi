using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShopDomain.Models;
using ShopInfrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Product> Products => 
            _database.GetCollection<Product>("Products");

        public IMongoCollection<ProductMessage> ProductMessages =>
            _database.GetCollection<ProductMessage>("ProductMessages");
    }
}
