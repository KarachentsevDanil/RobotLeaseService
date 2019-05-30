using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Users;
using RLS.Domain.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Users
{
    public class UserInterestsSearchRepository :
        BaseRepository<int, UserInterestsSearch, RentalDbContext>, IUserInterestsSearchRepository
    {
        public UserInterestsSearchRepository(RentalDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<UserInterestsSearch>> GetListAsync(CancellationToken ct = default)
        {
            return await DbContext.UserInterestsSearches.Include(t => t.User)
                .AsNoTracking().ToListAsync(ct);
        }
    }
}