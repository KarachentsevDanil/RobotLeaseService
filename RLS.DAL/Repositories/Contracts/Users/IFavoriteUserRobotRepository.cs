using RLS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Users
{
    public interface IFavoriteUserRobotRepository : IGenericRepository<int, FavoriteUserRobot>
    {
        Task DeleteRobotFromFavoriteAsync(string userId, int robotId, CancellationToken ct = default);
    }
}