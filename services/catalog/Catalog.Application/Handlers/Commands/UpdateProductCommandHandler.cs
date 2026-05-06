using Catalog.Application.Commands;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.UpdateProductAsync(new Core.Entities.Product
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Summary = request.Summary,
                Price = request.Price,
                ImageFile = request.ImageFile,
                Brand = request.Brand,
                Type = request.Type
            });
            return product;
        }
    }
}
