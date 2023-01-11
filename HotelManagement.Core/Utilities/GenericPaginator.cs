namespace HotelManagement.Application.Utility
{
    public class GenericPagination<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public GenericPagination(List<T> currentPageItems, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(currentPageItems);
        }
        public static GenericPagination<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var currentPageItems = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new GenericPagination<T>(currentPageItems, count, pageNumber, pageSize);
        }
    }
    //IQueryable Item = await _context.Customers.Where();
    //var gp = new GenericPagination<Datatype>();
    //var paginatedItem = GenericPagination.ToPagedList(item,3,5)
}


