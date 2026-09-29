using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Repositories
{
    public interface IUserProviderRepository
    {
        Task AddAsync(UserProvider userProvider);
    }
}
