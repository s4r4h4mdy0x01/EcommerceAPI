using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                 .ReverseMap();
            CreateMap<Pagination<Product>, Pagination<ProductResponseDto>>()
               .ReverseMap();
            CreateMap<ProductType, TypeResponseDto>()
                .ReverseMap();
            CreateMap<ProductBrand, BrandResponseDto>()
                .ReverseMap();
        }
    }
}
