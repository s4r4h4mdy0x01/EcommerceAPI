using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Seeding.Contexts
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> collection)
        {
            var exist = await collection.Find(x => true).AnyAsync();
            if (exist)
                return;
            var filePath = Path.Combine("Seeding", "Data",
                "types.json");
            if (!File.Exists(filePath)) return;
            var json = File.ReadAllText(filePath);
            var types = JsonSerializer.Deserialize<List<ProductType>>(json);
            if (types == null || types.Count == 0) return;
            await collection.InsertManyAsync(types);
        }

    }
}
