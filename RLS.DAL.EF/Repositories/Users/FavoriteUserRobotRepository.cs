using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Users;
using RLS.Domain.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.EF.Repositories.Users
{
    public class FavoriteUserRobotRepository :
        BaseRepository<int, FavoriteUserRobot, RentalDbContext>, IFavoriteUserRobotRepository
    {
        public FavoriteUserRobotRepository(RentalDbContext context) : base(context)
        {
        }

        public async Task DeleteRobotFromFavoriteAsync(string userId, int robotId, CancellationToken ct = default)
        {
            FavoriteUserRobot favoriteUserRobot = await DbContext.FavoriteUserRobots
                .FirstOrDefaultAsync(t => t.RobotId == robotId && t.UserId == userId, ct);

            DbContext.Remove(favoriteUserRobot);
        }
    }
}