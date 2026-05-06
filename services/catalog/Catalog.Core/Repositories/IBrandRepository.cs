using Catalog.Core.Entities;

namespace Catalog.Core.Repository
{
    public interface IBrandRepository
    {
        public Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();
    }
}
