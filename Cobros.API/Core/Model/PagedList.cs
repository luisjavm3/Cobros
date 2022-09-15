namespace Cobros.API.Core.Model
{
    public class PagedList<T> : List<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPrevious => PageNumber > 0;
        public bool HasNext => PageNumber < TotalPages;

        public PagedList(IEnumerable<T> data, PaginationParameters paginationParameters, int count)
        {
            PageNumber = paginationParameters.PageNumber;
            PageSize = paginationParameters.PageSize;
            TotalPages = (int) Math.Ceiling(count / (double)PageSize);
            TotalCount = count;

            AddRange(data);
        }
    }
}
