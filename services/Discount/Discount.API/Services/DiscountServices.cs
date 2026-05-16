using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountServices : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;
        public DiscountServices(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<DiscountModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var result = await _mediator.Send(query);
            return result;
        }
        public override async Task<DiscountModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = new CreateDiscountCommand
            {
                ProductName = request.Discount.ProductName,
                Description = request.Discount.Description,
                Amount = request.Discount.Amount
            };
            var result = await _mediator.Send(command);
            return result;


        }
        public override async Task<DiscountModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand
            {
                Id = request.Discount.Id,
                ProductName = request.Discount.ProductName,
                Description = request.Discount.Description,
                Amount = request.Discount.Amount
            };
            var result = await _mediator.Send(command);
            return result;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var query = new DeleteDiscountCommand(request.ProductName);
            var result = await _mediator.Send(query);
            var response = new DeleteDiscountResponse
            {
                Success = result
            };
            return response;
        }
    }
}
