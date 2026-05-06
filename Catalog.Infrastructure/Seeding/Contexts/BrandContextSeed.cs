using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Seeding.Contexts
{
    public static class BrandContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductBrand> collection)
        {
            var exist = await collection.Find(x => true).AnyAsync();
            if (exist)
                return;
            var filePath = Path.Combine("Seeding", "Data",
                "brands.json");
            if (!File.Exists(filePath)) return;
            var json = File.ReadAllText(filePath);
            var types = JsonSerializer.Deserialize<List<ProductBrand>>(json);
            if (types == null || types.Count == 0) return;
            await collection.InsertManyAsync(types);

        }
    }
}
