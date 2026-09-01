using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Configuration
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ProductsCollectionName { get; set; } = string.Empty;
    }
}
