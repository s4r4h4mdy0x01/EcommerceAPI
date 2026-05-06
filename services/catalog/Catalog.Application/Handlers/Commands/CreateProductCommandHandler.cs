using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<Product>(request);
            var createdProduct = await _productRepository.CreateProductAsync(products);
            return _mapper.Map<ProductResponseDto>(createdProduct);
        }
    }
}
