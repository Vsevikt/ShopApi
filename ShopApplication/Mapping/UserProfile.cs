using AutoMapper;
using ShopApplication.DTOs.UserDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserReadDTO>();
            CreateMap<User, UserLoginDTO>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}
