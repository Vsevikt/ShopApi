using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IQueueService
    {
        Task PublishAsync<T>(string queue, T message);
    }
}
