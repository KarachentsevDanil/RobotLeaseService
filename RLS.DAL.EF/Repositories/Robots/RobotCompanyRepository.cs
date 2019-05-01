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

        public async Task<IEnumerable<RobotCompany>> GetTopNPopularCompaniesAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<RobotCompany> query = DbContext.RobotCompanies
                .AsNoTracking()
                .Include(t => t.Models)
                .ThenInclude(t => t.Robots)
                .ThenInclude(t => t.Rentals);

            switch (filterParams.Type)
            {
                case RobotPopularity.ByRentCount:
                    query = query.OrderByDescending(t => t.Models.OrderByDescending(m => m.Robots.OrderByDescending(r => r.Rentals.Count)));
                    break;
                case RobotPopularity.ByRobotCount:
                    query = query.OrderByDescending(t => t.Models.OrderByDescending(m => m.Robots.Count));
                    break;
            }

            return await query
                .Take(filterParams.CountToTake)
                .ToListAsync(ct);
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