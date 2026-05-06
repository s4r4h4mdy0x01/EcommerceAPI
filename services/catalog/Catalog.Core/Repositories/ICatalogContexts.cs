using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Core.Repository
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<ProductBrand> ProductBrands { get; }
        IMongoCollection<ProductType> ProductTypes { get; }
    }
}
