using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductHandlers : IRequestHandler<GetAllProductQuery, Pagination<ProductResponseDto>>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductHandlers(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Pagination<ProductResponseDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsAsync(request.Params);

            var productDtos = _mapper.Map<Pagination<ProductResponseDto>>(products);
            return productDtos;
        }
    }
}
