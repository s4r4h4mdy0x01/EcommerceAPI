using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Commands
{
    public class ShoppingCartCommandHandler : IRequestHandler<ShoppingCartCommand, ShoppingCartResponseDto>
    {
        #region  Fields
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcServices _discountGrpcServices;
        #endregion
        #region Constructors
        public ShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper, DiscountGrpcServices discountGrpcServices)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _discountGrpcServices = discountGrpcServices;
        }
        #endregion
        public async Task<ShoppingCartResponseDto> Handle(ShoppingCartCommand request, CancellationToken cancellationToken)
        {

            foreach (var item in request.Items)
            {
                var coupon = await _discountGrpcServices.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var result = await _basketRepository.UpdateBasketAsync(new Core.Entities.ShoppingCart() { UserName = request.UserName, Items = request.Items });
            return _mapper.Map<ShoppingCartResponseDto>(result);
        }
    }
}
