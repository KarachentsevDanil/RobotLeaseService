using RLS.Domain.Rentals;
using System.Threading;
using System.Threading.Tasks;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.Models;

namespace RLS.DAL.Repositories.Contracts.Rentals
{
    public interface IRentalRepository : IGenericRepository<int, Rental>
    {
        Task<CollectionResult<Rental>> GetRentalsByFilterParamsAsync(RentalFilterParams filterParams, CancellationToken ct = default);
    }
}