using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByBrandNameQuery : IRequest<ProductResponseDto>
    {
        public string BrandName { get; set; }
        public GetProductByBrandNameQuery(string brandName)
        {
            BrandName = brandName;
        }
    }

}
