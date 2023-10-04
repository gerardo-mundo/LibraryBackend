using LibraryBackend.DTO;

namespace LibraryBackend.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPeerage)
                .Take(paginationDTO.RecordsPeerage);
        }
    }
}
