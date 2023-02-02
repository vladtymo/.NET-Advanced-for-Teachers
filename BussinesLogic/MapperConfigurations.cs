using Core.DTOs;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryName, opts => opts.MapFrom(x => GetCategoryName(x)));

            CreateMap<ProductDto, Product>();
             
            CreateMap<Category, CategoryDto>().ReverseMap();
        }

        private string GetCategoryName(Product product)
        {
            return product.Category?.Name ?? "not loaded";
        }
    }
}
