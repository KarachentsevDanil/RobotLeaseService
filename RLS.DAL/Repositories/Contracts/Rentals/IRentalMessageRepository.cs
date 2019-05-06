using RLS.Domain.Rentals;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Rentals
{
    public interface IRentalMessageRepository : IGenericRepository<int, RentalMessage>
    {
        Task<IEnumerable<RentalMessage>> GetRentalsByIdsAsync(int[] ids, CancellationToken ct = default);
    }
}