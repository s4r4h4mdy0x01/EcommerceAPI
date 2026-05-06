using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductByNameHandlers : IRequestHandler<GetProductByNameQuery, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByNameHandlers(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAllProductByNameAsync(request.Name);
            //if (product == null)
            //{
            //    return null;
            //}

            var productResponse = _mapper.Map<ProductResponseDto>(product);
            return productResponse;
        }
    }
}
