using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Shared.Extensions;
using RLS.DAL.EF.Shared.Repositories;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.Robots;
using RLS.Domain.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Robots
{
    public class RobotTypeRepository : BaseRepository<int, RobotType, RentalDbContext>, IRobotTypeRepository
    {
        public RobotTypeRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RobotType>> GetRobotTypesByTermAsync(string term, CancellationToken ct = default)
        {
            IEnumerable<RobotType> items = await DbContext.RobotTypes.Where(t => t.Name.Contains(term)).AsNoTracking()
                .ToListAsync(ct);

            return items;
        }

        public async Task<CollectionResult<RobotType>> GetTypesByFilterParamsAsync(RobotTypeFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotType> query = DbContext.RobotTypes.AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<RobotType> items = await query
                .OrderBy(x => x.Name)
                .WithPagination(filterParams.PageNumber, filterParams.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<RobotType> result = new CollectionResult<RobotType>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        private void FillFilterExpression(RobotTypeFilterParams filterParams)
        {
            Expression<Func<RobotType, bool>> predicate = PredicateBuilder.New<RobotType>(true);

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.Extend(t => t.Name.Contains(filterParams.Term));
            }

            filterParams.Expression = predicate;
        }
    }
}