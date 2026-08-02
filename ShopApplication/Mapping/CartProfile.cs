using AutoMapper;
using ShopApplication.DTOs.CartDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Mapping
{
    public class CartProfile : Profile
    {
        public CartProfile() 
        {
            CreateMap<CartCreateDTO, Cart>();
            CreateMap<CartUpdateDTO, Cart>();
            CreateMap<Cart, CartReadDTO>()
             .ForMember(
                 dest => dest.Name,
                 opt => opt.MapFrom(src => src.Product.Name)
             )
             .ForMember(
                 dest => dest.Price,
                 opt => opt.MapFrom(src => src.Product.Price)
             )
             .ForMember(
                dest => dest.ImageUrls,
                opt => opt.MapFrom(src =>
                    src.Product.Images.Select(x => x.Url).ToList()
                )
            );
        }
    }
}
