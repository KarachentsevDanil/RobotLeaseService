using RLS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Users
{
    public interface IUserRepository : IGenericRepository<string, User>
    {
        Task<User> GetUserTermAsync(string term, CancellationToken ct = default);
    }
}