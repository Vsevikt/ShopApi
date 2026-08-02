using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Repository
{
    public interface ICartRepository
    {
        Task<int?> AddCartAsync(Cart cart);
        Task<ICollection<Cart>> GetCartsAsync();
        Task<Cart?> GetCartAsync(int id);
        Task<bool> EditCartAsync(int id, int quantity);
        Task<int?> RemoveCartAsync(int id);
    }
}
