using RLS.Domain.FilterParams.Rents;
using RLS.Domain.Models;
using RLS.Domain.Rentals;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Rentals
{
    public interface IRentalRepository : IGenericRepository<int, Rental>
    {
        Task<CollectionResult<Rental>> GetRentalsByFilterParamsAsync(RentalFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<Rental>> GetUncompletedRentalsAsync(CancellationToken ct = default);
    }
}