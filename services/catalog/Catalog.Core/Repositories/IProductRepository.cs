using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Core.Repository
{
    public interface IProductRepository
    {
        public Task<Pagination<Product>> GetAllProductsAsync(CatalogSpecsParams param);
        public Task<Product> GetProductByIdAsync(string id);
        public Task<IEnumerable<Product>> GetAllProductByNameAsync(string name);
        public Task<IEnumerable<Product>> GetAllProductByBrandAsync(string brand);
        public Task<Product> CreateProductAsync(Product product);
        public Task<bool> UpdateProductAsync(Product newProduct);
        public Task<bool> DeleteProductAsync(string id);
    }
}
