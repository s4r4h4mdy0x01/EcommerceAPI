namespace Catalog.Core.Specs
{
    // request parameters for pagination, filtering and sorting
    public class CatalogSpecsParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 5;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get => _pageSize; set => _pageSize = (value > MaxPageSize ? MaxPageSize : value); }
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
    }

}
