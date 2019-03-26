using System.Linq;

namespace RLS.DAL.EF.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WithPagination<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}