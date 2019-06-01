using Dapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Extensions;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.Enums;
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
                .Include(x => x.UserFavorites)
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
                .Include(x => x.UserFavorites)
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

            query = OrderRobotsQuery(query, filterParams.SortBy);

            List<Robot> items = await query
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

        public async Task<CollectionResult<DashboardRobotModel>> GetDashboardRobotByFilterParamsAsync(
            RobotFilterParams filterParams, CancellationToken ct = default)
        {
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(filterParams.Term))
            {
                parameters.Add("term", dbType: DbType.String, value: filterParams.Term, direction: ParameterDirection.Input);
            }

            parameters.Add("userId", dbType: DbType.String, value: filterParams.UserId, direction: ParameterDirection.Input);
            parameters.Add("take", dbType: DbType.Int32, value: filterParams.Take, direction: ParameterDirection.Input);
            parameters.Add("skip", dbType: DbType.Int32, value: filterParams.Skip, direction: ParameterDirection.Input);

            CollectionResult<DashboardRobotModel> result = new CollectionResult<DashboardRobotModel>();

            var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync("[robot].[GetRobots]", parameters,
                commandType: CommandType.StoredProcedure);

            using (reader)
            {
                result.TotalCount = reader.ReadSingle<int>();
                result.Collection = reader.Read<DashboardRobotModel>();

                var feedback = reader.Read<RobotFeedbackModel>();

                foreach (var dashboardRobotModel in result.Collection)
                {
                    dashboardRobotModel.Feedback = feedback.Where(t => t.RobotId == dashboardRobotModel.Id).ToList();
                }
            }

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

        public async Task<DashboardStatisticModel> GetDashboardStatisticModelAsync(CancellationToken ct = default)
        {
            DashboardStatisticModel result = new DashboardStatisticModel();

            var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync("[robot].[GetDashboardStatistics]",
                commandType: CommandType.StoredProcedure);

            using (reader)
            {
                result.UserCount = reader.ReadSingle<int>();
                result.UserWithRobotsCount = reader.ReadSingle<int>();
                result.UserWithRentsCount = reader.ReadSingle<int>();
                result.UserWithRobotsAndRentsCount = reader.ReadSingle<int>();

                result.RobotCount = reader.ReadSingle<int>();
                result.AvailableTodayRobotCount = reader.ReadSingle<int>();

                result.RentCount = reader.ReadSingle<int>();
                result.CompletedRentCount = reader.ReadSingle<int>();
                result.ActiveRentCount = reader.ReadSingle<int>();
                result.CanceledRentCount = reader.ReadSingle<int>();

                result.CustomerFeedbackCount = reader.ReadSingle<int>();
                result.OwnerFeedbackCount = reader.ReadSingle<int>();
                result.CompanyCount = reader.ReadSingle<int>();
                result.TypeCount = reader.ReadSingle<int>();
                result.ModelCount = reader.ReadSingle<int>();
            }

            return result;
        }

        public async Task<Robot> GetRobotByUserInterestsAsync(
            string userId, string interests, CancellationToken ct = default)
        {
            IQueryable<Robot> query = DbContext.Robots.Where(t => t.UserId != userId)
                .Include(x => x.Model)
                .Include(x => x.Model.Type)
                .Include(x => x.Model.Company)
                .AsQueryable();

            return await query.FirstOrDefaultAsync(
                t => t.Model.Name.Contains(interests) ||
                     t.Model.Type.Name.Contains(interests) ||
                     t.Model.Description.Contains(interests) ||
                     t.Model.Type.Description.Contains(interests) ||
                     t.Model.Company.Name.Contains(interests) ||
                     interests.Contains(t.Model.Name) ||
                     interests.Contains(t.Model.Type.Name) ||
                     interests.Contains(t.Model.Description) ||
                     interests.Contains(t.Model.Type.Description) ||
                     interests.Contains(t.Model.Company.Name), ct);
        }

        private IOrderedQueryable<Robot> OrderRobotsQuery(IQueryable<Robot> query, RobotSortType sortType)
        {
            switch (sortType)
            {
                case RobotSortType.NameAscending:
                    return query.OrderBy(t => t.Model.Company.Name).ThenBy(t => t.Model.Name);

                case RobotSortType.NameDescending:
                    return query.OrderByDescending(t => t.Model.Company.Name).ThenByDescending(t => t.Model.Name);

                case RobotSortType.PriceAscending:
                    return query.OrderBy(t => t.DailyCosts);

                case RobotSortType.PriceDescending:
                    return query.OrderByDescending(t => t.DailyCosts);

                default:
                    throw new ArgumentOutOfRangeException(nameof(sortType), sortType, null);
            }
        }

        private void FillFilterExpression(RobotFilterParams filterParams)
        {
            Expression<Func<Robot, bool>> predicate = PredicateBuilder.New<Robot>
                (t => t.DailyCosts >= filterParams.MinPrice && t.DailyCosts <= filterParams.MaxPrice);

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                predicate = predicate.And(
                    t => t.Model.Name.Contains(filterParams.Term) ||
                         t.Model.Type.Name.Contains(filterParams.Term) ||
                         t.Model.Description.Contains(filterParams.Term) ||
                         t.Model.Type.Description.Contains(filterParams.Term) ||
                         t.Model.Company.Name.Contains(filterParams.Term));
            }

            if (!string.IsNullOrEmpty(filterParams.UserInterests))
            {
                predicate = predicate.And(
                    t => t.Model.Name.Contains(filterParams.UserInterests) ||
                         t.Model.Type.Name.Contains(filterParams.UserInterests) ||
                         t.Model.Description.Contains(filterParams.UserInterests) ||
                         t.Model.Type.Description.Contains(filterParams.UserInterests) ||
                         t.Model.Company.Name.Contains(filterParams.UserInterests) ||
                         filterParams.UserInterests.Contains(t.Model.Name) ||
                         filterParams.UserInterests.Contains(t.Model.Type.Name) ||
                         filterParams.UserInterests.Contains(t.Model.Description) ||
                         filterParams.UserInterests.Contains(t.Model.Type.Description) ||
                         filterParams.UserInterests.Contains(t.Model.Company.Name));
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

            if (filterParams.FilterType == RobotFilterType.OnlyFavorites)
            {
                predicate = predicate.And(t => t.UserFavorites.Any(f => f.UserId == filterParams.UserId));
            }

            filterParams.Expression = predicate;
        }
    }
}