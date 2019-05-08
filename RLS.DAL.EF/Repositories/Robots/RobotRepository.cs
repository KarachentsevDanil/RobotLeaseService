using Dapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Extensions;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Models.Robots;
using RLS.Domain.Robots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Robots
{
    public class RobotRepository : BaseRepository<int, Robot, RentalDbContext>, IRobotRepository
    {
        public RobotRepository(RentalDbContext context) : base(context)
        {
        }

        public override async Task<Robot> GetAsync(int id, CancellationToken ct = default)
        {
            return await DbContext.Robots
                .Include(x => x.User)
                .Include(x => x.Model)
                .Include(x => x.Model.Type)
                .Include(x => x.Model.Company)
                .Include(x => x.Rentals)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(r => r.Id == id, ct);
        }

        public async Task<CollectionResult<Robot>> GetRobotByFilterParamsAsync(RobotFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<Robot> query = DbContext.Robots
                .Include(x => x.User)
                .Include(x => x.Model)
                .Include(x => x.Model.Type)
                .Include(x => x.Model.Company)
                .Include(x => x.Rentals)
                .ThenInclude(x => x.User)
                .AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<Robot> items = await query
                .OrderBy(x => x.Model.Name)
                .WithPagination(filterParams.Skip, filterParams.Take)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<Robot> result = new CollectionResult<Robot>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        public async Task<IEnumerable<ValuableRobotModel>> GetMostValuableRobotByFilterParamsAsync(RobotMostValuableFilterParams filterParams,
            CancellationToken ct = default)
        {
            var parameters = new DynamicParameters();

            parameters.Add("userId", dbType: DbType.String, value: filterParams.UserId, direction: ParameterDirection.Input);
            parameters.Add("typeId", dbType: DbType.Int32, value: filterParams.TypeId, direction: ParameterDirection.Input);
            parameters.Add("take", dbType: DbType.Int32, value: filterParams.Take, direction: ParameterDirection.Input);

            IEnumerable<ValuableRobotModel> result;

            var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync("[robot].[GetMostValuableRobots]", parameters,
                commandType: CommandType.StoredProcedure);

            using (reader)
            {
                result = reader.Read<ValuableRobotModel>();
            }

            return result;
        }

        private void FillFilterExpression(RobotFilterParams filterParams)
        {
            Expression<Func<Robot, bool>> predicate = PredicateBuilder.New<Robot>
                (t => t.DailyCosts >= filterParams.MinPrice && t.DailyCosts <= filterParams.MaxPrice);

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.And(
                    t => t.Model.Name.Contains(filterParams.Term) || t.Model.Type.Name.Contains(filterParams.Term) ||
                         t.Model.Description.Contains(filterParams.Term) || t.Model.Type.Description.Contains(filterParams.Term) ||
                         t.Model.Company.Name.Contains(filterParams.Term));
            }

            if (filterParams.MinRating > 0 || filterParams.MaxRating < 5)
            {
                predicate = predicate.And(
                    t => t.Rentals != null && t.Rentals.Any(r => r.RobotRating > 0) && t.Rentals.Where(r => r.RobotRating > 0).Average(r => r.RobotRating) >= filterParams.MinRating &&
                         t.Rentals.Where(r => r.RobotRating > 0).Average(r => r.RobotRating) <= filterParams.MaxRating);
            }

            if (!string.IsNullOrEmpty(filterParams.UserId))
            {
                predicate = filterParams.IsSearchView ?
                    predicate.And(t => t.UserId != filterParams.UserId) :
                    predicate.And(t => t.UserId == filterParams.UserId);
            }

            if (filterParams.ModelIds != null && filterParams.ModelIds.Any())
            {
                predicate = predicate.And(t => filterParams.ModelIds.Contains(t.ModelId));
            }

            if (filterParams.CompaniesIds != null && filterParams.CompaniesIds.Any())
            {
                predicate = predicate.And(t => filterParams.CompaniesIds.Contains(t.Model.CompanyId));
            }

            if (filterParams.TypeIds != null && filterParams.TypeIds.Any())
            {
                predicate = predicate.And(t => filterParams.TypeIds.Contains(t.Model.TypeId));
            }

            if (filterParams.StartDate.HasValue && filterParams.EndDate.HasValue)
            {
                predicate = predicate.And(t =>
                    !t.Rentals.Any(r => filterParams.StartDate.Value <= r.EndDate && filterParams.EndDate.Value >= r.StartDate));
            }

            filterParams.Expression = predicate;
        }
    }
}