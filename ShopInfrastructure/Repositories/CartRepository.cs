using Microsoft.EntityFrameworkCore;
using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;
using ShopInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Repositories
{
    public class CartRepository(ShopDbContext _context) : ICartRepository
    {
        public async Task<int?> AddCartAsync(Cart cart)
        {
            var existingCart = await _context.Carts
            .FirstOrDefaultAsync(c =>
            c.ProductId == cart.ProductId);


            if (existingCart != null)
            {
                existingCart.Quantity += cart.Quantity;
            }
            else
            {
                await _context.Carts.AddAsync(cart);
            }

            await _context.SaveChangesAsync();

            return existingCart?.Id ?? cart.Id;
        }

        public async Task<ICollection<Cart>> GetCartsAsync()
        {
            var carts = await _context.Carts
                .Include(c => c.Product)
                    .ThenInclude(p => p.Images)
                .ToListAsync();

            return carts;
        }

        public async Task<Cart?> GetCartAsync(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public async Task<bool> EditCartAsync(int id, int quantity)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
                return false;

            cart.Quantity = quantity;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> RemoveCartAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
                return null;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
