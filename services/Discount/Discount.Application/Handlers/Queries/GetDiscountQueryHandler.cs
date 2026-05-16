using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers.Queries
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, DiscountModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<GetDiscountQueryHandler> _logger;
        public GetDiscountQueryHandler(IDiscountRepository discountRepository, ILogger<GetDiscountQueryHandler> logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }
        public async Task<DiscountModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.GetDiscount(request.ProductName);
            if (discount == null)
            {
                _logger.LogWarning("No discount found for product: {ProductName}", request.ProductName);
                return new DiscountModel
                {
                    ProductName = request.ProductName,
                    Amount = 0,
                    Id = 0,
                    Description = "No discount available"
                };
            }

            _logger.LogInformation("Discount retrieved for product: {ProductName}", request.ProductName);
            return new DiscountModel
            {
                ProductName = discount.ProductName,
                Amount = discount.Amount,
                Id = discount.Id,
                Description = discount.Description

            };

        }
    }
}
