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

        public PaginationResult(IEnumerable<T> source, PaginationParameters paginationParameters)
        {
            PageNumber = paginationParameters.PageNumber;
            PageSize = paginationParameters.PageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            int skip = (PageNumber - 1) * PageSize;
            Data = source.Skip(skip).Take(PageSize);
            //Data = source.Skip(skip).Take(PageSize).AsEnumerable();
        }

        public PaginationResult(IEnumerable<T> data, PaginationParameters paginationParameters, int count)
        {
            PageNumber = paginationParameters.PageNumber;
            PageSize = paginationParameters.PageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            Data = data;
        }

    }
}
