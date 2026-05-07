using AutoMapper;
using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;


namespace Basket.Application.Handlers.Commands
{
    public class DeleteBasketByUserNameCommmandHandler : IRequestHandler<DeleteBasketByUserNameCommmand, Unit>
    {
        #region  Fields
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        #endregion
        #region Constructors
        public DeleteBasketByUserNameCommmandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(DeleteBasketByUserNameCommmand request, CancellationToken cancellationToken)
        {
            await _basketRepository.DeleteBasketAsync(request.UserName);
            return Unit.Value;
        }
        #endregion
    }
}
