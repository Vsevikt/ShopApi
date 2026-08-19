using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Configuration
{
    sealed public class RabbitMqSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
    }
}
