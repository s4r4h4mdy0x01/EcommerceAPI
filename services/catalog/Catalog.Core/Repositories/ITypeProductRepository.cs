using Catalog.Core.Entities;

namespace Catalog.Core.Repository
{
    public interface ITypeProductRepository
    {
        public Task<IEnumerable<ProductType>> GetAllTypeProductsAsync();
    }
}
