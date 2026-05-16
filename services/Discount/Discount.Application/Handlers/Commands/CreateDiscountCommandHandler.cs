using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers.Commands
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<DiscountModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = _mapper.Map<Coupon>(request);

            var createdDiscount = await _discountRepository.CreateDiscount(discount);
            return _mapper.Map<DiscountModel>(createdDiscount);
        }
    }
}
