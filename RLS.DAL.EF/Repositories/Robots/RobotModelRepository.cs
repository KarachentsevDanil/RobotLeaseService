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
    public class RobotModelRepository : BaseRepository<int, RobotModel, RentalDbContext>, IRobotModelRepository
    {
        public RobotModelRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<CollectionResult<RobotModel>> GetModelsByFilterParamsAsync(RobotModelFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotModel> query = DbContext.RobotModels.AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<RobotModel> items = await query
                .OrderBy(x => x.Name)
                .WithPagination(filterParams.PageNumber, filterParams.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<RobotModel> result = new CollectionResult<RobotModel>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<IEnumerable<RobotModel>> GetRobotModelsByTypeAsync(int typeId, CancellationToken ct = default)
        {
            IEnumerable<RobotModel> items = await DbContext.RobotModels.Where(t => t.TypeId == typeId).AsNoTracking()
                .Include(t => t.Company)
                .Include(t => t.Type)
                .ToListAsync(ct);

            return items;
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