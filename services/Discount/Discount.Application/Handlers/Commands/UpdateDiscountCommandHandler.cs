using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers.Commands
{
    public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, DiscountModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<DiscountModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = _mapper.Map<Coupon>(request);

            var updatedDiscount = await _discountRepository.UpdateDiscount(discount);
            return _mapper.Map<DiscountModel>(updatedDiscount);
        }
    }
}
