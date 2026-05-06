using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductByBrandNameHandlers : IRequestHandler<GetProductByBrandNameQuery, ProductResponseDto>

    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByBrandNameHandlers(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> Handle(GetProductByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAllProductByBrandAsync(request.BrandName);
            var productResponse = _mapper.Map<ProductResponseDto>(product);
            return productResponse;
        }
    }
}
