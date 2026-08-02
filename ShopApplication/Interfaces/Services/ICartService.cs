using ShopApplication.DTOs.CartDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface ICartService
    {
        Task<int?> CreateCartAsync(CartCreateDTO dto);
        Task<ICollection<CartReadDTO>> GetAllCartsAsync();
        Task<CartReadDTO?> GetCartByIdAsync(int id);
        Task<bool> UpdateCartAsync(int id, int quantity);
        Task<int?> DeleteCartAsync(int id);
    }
}
