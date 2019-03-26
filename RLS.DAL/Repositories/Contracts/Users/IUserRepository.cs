using RLS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Users
{
    public interface IUserRepository : IGenericRepository<int, User>
    {
        Task<User> GetUserTermAsync(string term, CancellationToken ct = default);
    }
}