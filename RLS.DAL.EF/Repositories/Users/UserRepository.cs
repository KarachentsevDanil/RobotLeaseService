using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Users;
using RLS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Users
{
    public class UserRepository : BaseRepository<int, User, RentalDbContext>, IUserRepository
    {
        public UserRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserTermAsync(string term, CancellationToken ct = default)
        {
            return await DbContext.Users.FirstOrDefaultAsync(t => t.Email.Contains(term), ct);
        }
    }
}