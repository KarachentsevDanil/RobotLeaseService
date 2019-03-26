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
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;

namespace RLS.DAL.EF.Repositories.Robots
{
    public class RobotRepository : BaseRepository<int, Robot, RentalDbContext>, IRobotRepository
    {
        public RobotRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<CollectionResult<Robot>> GetRobotByFilterParamsAsync(RobotFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<Robot> query = DbContext.Robots
                .Include(x=> x.Model)
                .Include(x=> x.Model.Type)
                .Include(x=> x.Model.Company)
                .AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<Robot> items = await query
                .OrderBy(x => x.Model.Name)
                .WithPagination(filterParams.PageNumber, filterParams.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<Robot> result = new CollectionResult<Robot>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        private void FillFilterExpression(RobotFilterParams filterParams)
        {
            Expression<Func<Robot, bool>> predicate = PredicateBuilder.New<Robot>(true);

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.And(
                    t => t.Model.Name.Contains(filterParams.Term) || t.Model.Type.Name.Contains(filterParams.Term));
            }

            if (!string.IsNullOrEmpty(filterParams.UserId))
            {
                predicate = predicate.And(t => t.UserId == filterParams.UserId);
            }

            if (filterParams.ModelIds != null && filterParams.ModelIds.Any())
            {
                predicate = predicate.And(t => filterParams.ModelIds.Contains(t.ModelId));
            }

            if (filterParams.TypeIds != null && filterParams.TypeIds.Any())
            {
                predicate = predicate.And(t => filterParams.TypeIds.Contains(t.Model.TypeId));
            }

            if (filterParams.StartDate.HasValue && filterParams.EndDate.HasValue)
            {
                predicate = predicate.And(t =>
                    !t.Rentals.Any(r => filterParams.StartDate.Value <= r.Rental.EndDate && filterParams.EndDate.Value >= r.Rental.StartDate));
            }

            filterParams.Expression = predicate;
        }
    }
}