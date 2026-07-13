using AutoMapper;
using ShopApplication.DTOs.UserDTOs;
using ShopApplication.Interfaces.Helpers;
using ShopApplication.Interfaces.Repository;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class AuthService(IMapper _mapper, IAuthRepository _repository, IHashHelper _hashHelper) : IAuthService
    {
        public async Task<UserReadDTO?> RegisterAsync(UserCreateDTO dto)
        {

            var isExist = await _repository.IsExistEmailAsync(dto.Email);
            if (!isExist)
            {
                var hash = _hashHelper.Hash(dto.Password);
                var user = _mapper.Map<User>(dto);
                return _mapper.Map<UserReadDTO>(await _repository.RegisterUserAsync(user, hash));
            }
            return null;
        }
    }
}
