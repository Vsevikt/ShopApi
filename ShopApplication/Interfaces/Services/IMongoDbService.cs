using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IMongoDbService
    {
        IMongoCollection<Product> GetCollection();
    }
}
