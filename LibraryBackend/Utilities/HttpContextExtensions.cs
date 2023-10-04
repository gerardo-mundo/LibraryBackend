using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Utilities
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersIntoHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double quantity = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalOfRegistries", quantity.ToString());
        }
    }
}
