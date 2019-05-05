using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.Robots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using RLS.DAL.EF.Extensions;
using RLS.Domain.Enums;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Models.Robots;

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

        public async Task<IEnumerable<RobotTypeChartModel>> GetTopNPopularTypesAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default)
        {
            var parameters = new DynamicParameters();

            parameters.Add("orderBy", dbType: DbType.Int32, value: (int)filterParams.Type, direction: ParameterDirection.Input);
            parameters.Add("take", dbType: DbType.Int32, value: filterParams.CountToTake, direction: ParameterDirection.Input);

            IEnumerable<RobotTypeChartModel> result;

            var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync("[robot].[GetRobotType]", parameters,
                commandType: CommandType.StoredProcedure);

            using (reader)
            {
                result = reader.Read<RobotTypeChartModel>();
            }

            return result;
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