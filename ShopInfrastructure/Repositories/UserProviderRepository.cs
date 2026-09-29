using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;
using ShopInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopInfrastructure.Repositories
{
    public class UserProviderRepository : IUserProviderRepository
    {
        private readonly ShopDbContext _context;

        public UserProviderRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserProvider userProvider)
        {
            await _context.UserProviders.AddAsync(userProvider);
            await _context.SaveChangesAsync();
        }
    }
}
