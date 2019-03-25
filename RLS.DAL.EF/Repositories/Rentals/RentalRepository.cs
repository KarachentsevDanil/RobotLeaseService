using LinqKit;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.EF.Shared.Extensions;
using RLS.DAL.EF.Shared.Repositories;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.Rentals;
using RLS.Domain.Shared.Models;
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
            IQueryable<Rental> query = DbContext.Rentals.AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<Rental> items = await query
                .OrderByDescending(x => x.StartDate)
                .WithPagination(filterParams.PageNumber, filterParams.PageSize)
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