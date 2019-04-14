using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Extensions;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.Models;
using RLS.Domain.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Rentals
{
    public class RentalRepository : BaseRepository<int, Rental, RentalDbContext>, IRentalRepository
    {
        public RentalRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<CollectionResult<Rental>> GetRentalsByFilterParamsAsync(RentalFilterParams filterParams, CancellationToken ct = default)
        {
            IQueryable<Rental> query = DbContext.Rentals
                .Include(x=> x.User)
                .Include(x=> x.Robot)
                .Include(x=> x.Robot.User)
                .Include(x=> x.Robot.Model)
                .Include(x=> x.Robot.Model.Type)
                .Include(x=> x.Robot.Model.Company)
                .AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<Rental> items = await query
                .OrderByDescending(x => x.StartDate)
                .WithPagination(filterParams.Skip, filterParams.Take)
                .AsNoTracking()
                .ToListAsync(ct);

            CollectionResult<Rental> result = new CollectionResult<Rental>
            {
                Collection = items,
                TotalCount = totalCount
            };

            return result;
        }

        private void FillFilterExpression(RentalFilterParams filterParams)
        {
            Expression<Func<Rental, bool>> predicate = PredicateBuilder.New<Rental>(true);

            if (!string.IsNullOrEmpty(filterParams.UserId))
            {
                predicate = predicate.And(t => t.UserId == filterParams.UserId);
            }

            if (filterParams.Status.HasValue)
            {
                predicate = predicate.And(t => t.Status == filterParams.Status);
            }

            filterParams.Expression = predicate;
        }
    }
}