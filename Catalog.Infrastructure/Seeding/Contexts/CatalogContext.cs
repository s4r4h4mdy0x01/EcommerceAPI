using Catalog.Core.Entities;
using Catalog.Core.Repository;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Seeding.Contexts
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public IMongoCollection<ProductBrand> ProductBrands { get; }
        public IMongoCollection<ProductType> ProductTypes { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            Products = database.GetCollection<Product>(configuration["DatabaseSettings:ProductsCollectionName"]);
            ProductBrands = database.GetCollection<ProductBrand>(configuration["DatabaseSettings:ProductBrandsCollectionName"]);
            ProductTypes = database.GetCollection<ProductType>(configuration["DatabaseSettings:ProductTypesCollectionName"]);
            _ = CatalogContextSeed.SeedDataAsync(Products);
            _ = BrandContextSeed.SeedDataAsync(ProductBrands);
            _ = TypeContextSeed.SeedDataAsync(ProductTypes);
        }
    }
}