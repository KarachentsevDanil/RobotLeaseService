using System;
using System.Linq.Expressions;

namespace RLS.Domain.Models
{
    public class BaseFilterParams<TEntity> where TEntity : class
    {
        public int Take { get; set; } = 25;

        public int Skip { get; set; } = 0;

        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}