using Catalog.Application.Commands;
using Catalog.Core.Repository;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.DeleteProductAsync(request.Id);
            return result;
        }

    }
}
