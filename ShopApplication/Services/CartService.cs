using AutoMapper;
using ShopApplication.DTOs.CartDTOs;
using ShopApplication.Interfaces;
using ShopApplication.Interfaces.Repository;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class CartService(ICartRepository _repository, IImageService _imageService, IMapper _mapper) : ICartService
    {
        public async Task<int?> CreateCartAsync(CartCreateDTO dto)
        {
            var cart = _mapper.Map<Cart>(dto);
            return await _repository.AddCartAsync(cart);
        }

        public async Task<ICollection<CartReadDTO>> GetAllCartsAsync()
        {
            var carts = await _repository.GetCartsAsync();
            List<CartReadDTO> dtos = null;
            if (carts != null && carts.Count > 0)
                dtos = _mapper.Map<List<CartReadDTO>>(carts);
            return dtos;
        }

        public async Task<CartReadDTO?> GetCartByIdAsync(int id)
        {
            CartReadDTO? dto = null;
            var cart = await _repository.GetCartAsync(id);
            if (cart != null)
                dto = _mapper.Map<CartReadDTO>(cart);
            return dto;
        }

        public async Task<bool> UpdateCartAsync(int id, int quantity)
        {
            var cart = await _repository.GetCartAsync(id);
            if (cart == null)
                return false;
            cart.Quantity = quantity;
            return await _repository.EditCartAsync(id, quantity);
        }

        public async Task<int?> DeleteCartAsync(int id)
        {
            return await _repository.RemoveCartAsync(id);
        }
    }
}
