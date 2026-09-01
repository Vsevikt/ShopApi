using ShopApplication.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IProductMessageService
    {
        Task<bool> CreateMessageAsync(int id, ProductMessageCreateDto dto);
    }
}
