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
    public class RobotCompanyRepository : BaseRepository<int, RobotCompany, RentalDbContext>, IRobotCompanyRepository
    {
        public RobotCompanyRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<CollectionResult<RobotCompany>> GetCompaniesByFilterParamsAsync(RobotCompanyFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotCompany> query = DbContext.RobotCompanies.AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<RobotCompany> items = await query
                .OrderBy(x => x.Name)
                .WithPagination(filterParams.Skip, filterParams.Take)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<RobotCompany> result = new CollectionResult<RobotCompany>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<IEnumerable<RobotCompany>> GetRobotCompaniesByTermAsync(string term, CancellationToken ct = default)
        {
            IEnumerable<RobotCompany> items = await DbContext.RobotCompanies.Where(t => t.Name.Contains(term)).AsNoTracking()
                .ToListAsync(ct);

            return items;
        }

        public async Task<IEnumerable<RobotCompanyChartModel>> GetTopNPopularCompaniesAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default)
        {
            var parameters = new DynamicParameters();
            
            parameters.Add("orderBy", dbType: DbType.Int32, value: (int)filterParams.Type, direction: ParameterDirection.Input);
            parameters.Add("take", dbType: DbType.Int32, value: filterParams.CountToTake, direction: ParameterDirection.Input);

            IEnumerable<RobotCompanyChartModel> result;

            var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync("[robot].[GetRobotCompany]", parameters,
                commandType: CommandType.StoredProcedure);

            using (reader)
            {
                result = reader.Read<RobotCompanyChartModel>();
            }

            return result;
        }

        private void FillFilterExpression(RobotCompanyFilterParams filterParams)
        {
            Expression<Func<RobotCompany, bool>> predicate = PredicateBuilder.New<RobotCompany>(true);

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.Extend(t => t.Name.Contains(filterParams.Term));
            }

            filterParams.Expression = predicate;
        }
    }
}