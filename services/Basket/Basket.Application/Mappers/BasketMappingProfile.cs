using AutoMapper;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponseDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemResponseDto>().ReverseMap();
        }
    }
}
