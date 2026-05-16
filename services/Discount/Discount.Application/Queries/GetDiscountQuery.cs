using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetDiscountQuery : IRequest<DiscountModel>
    {
        public string ProductName { get; set; }

        public GetDiscountQuery(string productName)
        {
            ProductName = productName;
        }
    }
}
