﻿using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Extensions;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using RLS.Domain.Enums;
using RLS.Domain.Models.Robots;

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

        public async Task<IEnumerable<RobotModelChartModel>> GetTopNPopularModelsAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default)
        {
            var parameters = new DynamicParameters();

            parameters.Add("companyId", dbType: DbType.Int32, value: filterParams.CompanyId, direction: ParameterDirection.Input);
            parameters.Add("typeId", dbType: DbType.Int32, value: filterParams.TypeId, direction: ParameterDirection.Input);
            parameters.Add("orderBy", dbType: DbType.Int32, value: (int)filterParams.Type, direction: ParameterDirection.Input);
            parameters.Add("take", dbType: DbType.Int32, value: filterParams.CountToTake, direction: ParameterDirection.Input);

            IEnumerable<RobotModelChartModel> result;

            var reader = await DbContext.Database.GetDbConnection().QueryMultipleAsync("[robot].[GetRobotModel]", parameters,
                commandType: CommandType.StoredProcedure);

            using (reader)
            {
                result = reader.Read<RobotModelChartModel>();
            }

            return result;
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