using ShopApplication.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IOrderService
    {
        //Task ProcessOrderAsync(OrderMessageDTO orderMessage);
        Task<object> CreateOrderAsync(string userEmail, OrderCreateDTO dto);
    }
}
