using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.Domain.Rentals;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Rentals
{
    public class RentalMessageRepository : BaseRepository<int, RentalMessage, RentalDbContext>, IRentalMessageRepository
    {
        public RentalMessageRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RentalMessage>> GetRentalsByIdsAsync(int[] ids, CancellationToken ct = default)
        {
            return await DbContext.Messages.Where(t => ids.Contains(t.Id)).ToListAsync(ct);
        }
    }
}