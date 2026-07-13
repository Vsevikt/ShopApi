using AutoMapper;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryCreate, Category>();

            CreateMap<Category, CategoryRead>()
                .ForMember(
                dest => dest.Products, 
                opt => opt.MapFrom(src => src.Products.Select(p => p.Id).ToList()));

            CreateMap<CategoryUpdate, Category>();
        }
    }
}
