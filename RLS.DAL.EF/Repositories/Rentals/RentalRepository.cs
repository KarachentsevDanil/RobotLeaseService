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
                .Include(x => x.User)
                .Include(x => x.Robot)
                .Include(x => x.Robot.User)
                .Include(x => x.Robot.Model)
                .Include(x => x.Robot.Model.Type)
                .Include(x => x.Robot.Model.Company)
                .AsQueryable();

            FillFilterExpression(filterParams);

            query = query.Where(filterParams.Expression);

            int totalCount = query.Count();

            List<Rental> items = await query
                .OrderByDescending(x => x.UpdatedAt)
                .ThenByDescending(x => x.Id)
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

        public override async Task<Rental> GetAsync(int id, CancellationToken ct = default)
        {
            return await DbContext.Rentals
                .Include(x => x.User)
                .Include(x => x.Robot).ThenInclude(x => x.Rentals)
                .Include(x => x.Robot).ThenInclude(x => x.User)
                .Include(x => x.Robot).ThenInclude(x => x.Model).ThenInclude(x => x.Company)
                .Include(x => x.Robot).ThenInclude(x => x.Model).ThenInclude(x => x.Type)
                .Include(x => x.Messages)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(r => r.Id == id, ct);
        }

        private void FillFilterExpression(RentalFilterParams filterParams)
        {
            Expression<Func<Rental, bool>> predicate = PredicateBuilder.New<Rental>(true);

            if (!string.IsNullOrEmpty(filterParams.UserId) && !filterParams.IsCalendarView)
            {
                predicate = filterParams.IsOwnerView
                    ? predicate.And(t => t.Robot.UserId == filterParams.UserId)
                    : predicate.And(t => t.UserId == filterParams.UserId);
            }

            if (!string.IsNullOrEmpty(filterParams.Term))
            {
                filterParams.Term = filterParams.Term.ToLower();

                predicate = predicate.And(t => t.Robot.Model.Name.ToLower().Contains(filterParams.Term) ||
                                               t.Robot.Model.Description.ToLower().Contains(filterParams.Term) ||
                                               t.Robot.Model.Type.Name.ToLower().Contains(filterParams.Term) ||
                                               t.Robot.Model.Company.Name.ToLower().Contains(filterParams.Term));
            }

            if (filterParams.Status.HasValue)
            {
                predicate = predicate.And(t => t.Status == filterParams.Status);
            }

            if (filterParams.RobotId.HasValue)
            {
                predicate = predicate.And(t => t.RobotId == filterParams.RobotId);
            }

            if (filterParams.Start.HasValue && filterParams.End.HasValue)
            {
                predicate = predicate.And(r => filterParams.Start.Value <= r.EndDate && filterParams.End.Value >= r.StartDate);
            }

            filterParams.Expression = predicate;
        }
    }
}