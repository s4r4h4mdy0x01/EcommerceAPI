using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductByIdHandlers : IRequestHandler<GetProductByIdQuery, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdHandlers(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);
            var productDto = _mapper.Map<ProductResponseDto>(product);
            return productDto;
        }
    }
}
