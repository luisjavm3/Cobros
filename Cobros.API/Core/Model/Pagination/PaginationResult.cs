using Cobros.API.Entities;

namespace Cobros.API.Core.Model.Pagination
{
    public class PaginationResult<T> where T : class
    {
        public IEnumerable<T> Data { get; private set; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public PaginationResult(IEnumerable<T> data, PaginationParameters paginationParameters, int count)
        {
            Data = data;

            PageNumber = paginationParameters.PageNumber;
            PageSize = paginationParameters.PageSize;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            TotalCount = count;
        }
    }
}
