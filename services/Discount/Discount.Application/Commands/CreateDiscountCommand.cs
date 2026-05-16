using Discount.Grpc.Protos;
using MediatR;


namespace Discount.Application.Commands
{
    public class CreateDiscountCommand : IRequest<DiscountModel>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

    }
}
