using RLS.DAL.Shared.Repositories.Contracts;
using RLS.Domain.Rentals;
using RLS.Domain.Shared.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Rentals
{
    public interface IRentalRepository : IGenericRepository<int, Rental>
    {
        Task<CollectionResult<Rental>> GetRentalsByFilterParamsAsync(RentalFilterParams filterParams, CancellationToken ct = default);
    }
}