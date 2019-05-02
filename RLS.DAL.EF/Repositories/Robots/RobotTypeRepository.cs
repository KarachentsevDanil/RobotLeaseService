using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RLS.DAL.EF.Extensions;
using RLS.Domain.Enums;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;

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

        public async Task<IEnumerable<RobotType>> GetTopNPopularTypesAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotType> query = DbContext.RobotTypes
                .AsNoTracking()
                .Include(t => t.Models)
                .ThenInclude(t => t.Robots)
                .ThenInclude(t => t.Rentals);

            switch (filterParams.Type)
            {
                case RobotPopularity.ByRentCount:
                    query = query.OrderByDescending(t => t.Models.Sum(m => m.Robots.Sum(r => r.Rentals.Count)));
                    break;
                case RobotPopularity.ByRobotCount:
                    query = query.OrderByDescending(t => t.Models.Sum(m => m.Robots.Count));
                    break;
                case RobotPopularity.ByRobotAndRentCount:
                    query = query.OrderByDescending(t => t.Models.Sum(m => m.Robots.Count) + t.Models.Sum(m => m.Robots.Sum(r => r.Rentals.Count)));
                    break;
            }

            return await query
                .Take(filterParams.CountToTake)
                .ToListAsync(ct);
        }

        public async Task<CollectionResult<RobotType>> GetTypesByFilterParamsAsync(RobotTypeFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotType> query = DbContext.RobotTypes.AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<RobotType> items = await query
                .OrderBy(x => x.Name)
                .WithPagination(filterParams.Skip, filterParams.Take)
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