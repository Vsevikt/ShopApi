using AutoMapper;
using ShopApplication.DTOs.ProductDTOs;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class ProductMessageService(IProductMessageRepository _repository, IMapper _mapper) : IProductMessageService
    {
        public async Task<bool> CreateMessageAsync(int id, ProductMessageCreateDto dto)
        {
            if (dto.Type == ProductMessageType.Review && dto.Rating == null)
                return false;

            if (dto.Type == ProductMessageType.Question && dto.Rating != null)
                return false;

            var message = _mapper.Map<ProductMessage>(dto);
            message.ProductId = id;
            await _repository.AddMessageAsync(message);
            return true;
        }
    }
}
