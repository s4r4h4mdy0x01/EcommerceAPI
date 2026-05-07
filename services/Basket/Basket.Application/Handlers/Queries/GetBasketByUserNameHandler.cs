using AutoMapper;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Queries
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponseDto>
    {
        #region  Fields
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        #endregion
        #region Constructors
        public GetBasketByUserNameHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        #endregion
        #region Methods
        public async Task<ShoppingCartResponseDto> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var basket = await _basketRepository.GetBasketByUserNameAsync(request.UserName);
            var result = _mapper.Map<ShoppingCartResponseDto>(basket);
            return result;
        }
        #endregion
    }
}
