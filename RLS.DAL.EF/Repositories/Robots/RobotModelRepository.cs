using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Extensions;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RLS.Domain.Enums;

namespace RLS.DAL.EF.Repositories.Robots
{
    public class RobotModelRepository : BaseRepository<int, RobotModel, RentalDbContext>, IRobotModelRepository
    {
        public RobotModelRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<CollectionResult<RobotModel>> GetModelsByFilterParamsAsync(RobotModelFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotModel> query = DbContext.RobotModels
                .Include(m => m.Company)
                .Include(m => m.Type)
                .AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<RobotModel> items = await query
                .OrderBy(x => x.Name)
                .WithPagination(filterParams.Skip, filterParams.Take)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<RobotModel> result = new CollectionResult<RobotModel>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<IEnumerable<RobotModel>> GetRobotModelsAsync(CancellationToken ct = default)
        {
            IEnumerable<RobotModel> items = await DbContext.RobotModels
                .AsNoTracking()
                .Include(t => t.Company)
                .Include(t => t.Type)
                .ToListAsync(ct);

            return items;
        }

        public async Task<IEnumerable<RobotModel>> GetTopNPopularModelsAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotModel> query = DbContext.RobotModels
                .AsNoTracking()
                .Include(t => t.Type)
                .Include(t => t.Company)
                .Include(t => t.Robots)
                .ThenInclude(t => t.Rentals);

            if (filterParams.CompanyId.HasValue)
            {
                query = query.Where(t => t.CompanyId == filterParams.CompanyId);
            }

            if (filterParams.TypeId.HasValue)
            {
                query = query.Where(t => t.TypeId == filterParams.TypeId);
            }

            switch (filterParams.Type)
            {
                case RobotPopularity.ByRentCount:
                    query = query.OrderByDescending(t => t.Robots.Sum(r => r.Rentals.Count));
                    break;
                case RobotPopularity.ByRobotCount:
                    query = query.OrderByDescending(t => t.Robots.Count);
                    break;
                case RobotPopularity.ByRobotAndRentCount:
                    query = query.OrderByDescending(t => t.Robots.Count + t.Robots.Sum(r => r.Rentals.Count));
                    break;
            }

            return await query
                .Take(filterParams.CountToTake)
                .ToListAsync(ct);
        }

        private void FillFilterExpression(RobotModelFilterParams filterParams)
        {
            Expression<Func<RobotModel, bool>> predicate = PredicateBuilder.New<RobotModel>(true);

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.Extend(t => t.Name.Contains(filterParams.Term));
            }

            filterParams.Expression = predicate;
        }
    }
}