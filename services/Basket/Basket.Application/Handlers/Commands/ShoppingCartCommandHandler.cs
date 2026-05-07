using AutoMapper;
using Basket.Application.Commands;
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
        #endregion
        #region Constructors
        public ShoppingCartCommandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        #endregion
        public async Task<ShoppingCartResponseDto> Handle(ShoppingCartCommand request, CancellationToken cancellationToken)
        {

            var result = await _basketRepository.UpdateBasketAsync(new Core.Entities.ShoppingCart() { UserName = request.UserName, Items = request.Items });
            return _mapper.Map<ShoppingCartResponseDto>(result);
        }
    }
}
