using System.Linq;

namespace RLS.DAL.EF.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WithPagination<TEntity>(this IQueryable<TEntity> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }
    }
}