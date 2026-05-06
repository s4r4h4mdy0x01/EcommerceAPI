namespace Catalog.Core.Specs
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; } = 1;
        public int PageCount { get; set; }
        public int PageSize { get; set; } = 5;
        public IReadOnlyList<T> Data { get; set; }

        public Pagination() { }
        public Pagination(int pageIndex, int pageCount, int pageSize, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageCount = pageCount;
            PageSize = pageSize;
            Data = data;
        }

    }
}

