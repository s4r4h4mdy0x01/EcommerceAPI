using Catalog.Core.Entities;
using Catalog.Core.Repository;
using Catalog.Core.Specs;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypeProductRepository
    {
        public readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetAllProductsAsync(CatalogSpecsParams param)

        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(param.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(param.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(param.BrandId))
            {
                filter = filter & builder.Eq(p => p.Brand.Id, param.BrandId);
            }
            if (!string.IsNullOrEmpty(param.TypeId))
            {
                filter = filter & builder.Eq(p => p.Type.Id, param.TypeId);
            }
            var totalItems = await _context.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(param, filter);

            return new Pagination<Product>(
                param.PageIndex,
                param.PageSize,
         (int)totalItems,
                data
                );
        }

        public async Task<IEnumerable<Product>> GetAllProductByBrandAsync(string brand)
        {
            return await _context.Products.Find(p => p.Brand.Name == brand).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductByNameAsync(string name)
        {
            return await _context.Products.Find(p => p.Name == name).ToListAsync();
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        public async Task<bool> UpdateProductAsync(Product newProduct)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(p => p.Id == newProduct.Id, newProduct);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
        {
            return await _context.ProductBrands.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypeProductsAsync()
        {
            return await _context.ProductTypes.Find(p => true).ToListAsync();
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecsParams param, FilterDefinition<Product> filterDefinition)
        {
            var sortDefinition = Builders<Product>.Sort.Ascending(p => p.Name);
            var pageIndex = param.PageIndex < 1 ? 1 : param.PageIndex;
            var pageSize = param.PageSize < 1 ? 5 : param.PageSize;
            if (!string.IsNullOrEmpty(param.Sort))
            {
                switch (param.Sort.ToLower())
                {
                    case "priceasc":
                        sortDefinition = Builders<Product>.Sort.Ascending(p => p.Price);
                        break;
                    case "pricedesc":
                        sortDefinition = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDefinition = Builders<Product>.Sort.Ascending(p => p.Name);
                        break;
                }
            }
            return await _context.Products
                .Find(filterDefinition)
                .Sort(sortDefinition)
           .Skip(pageSize * (pageIndex - 1))
                .Limit(pageSize)
                .ToListAsync();
        }


    }
}
