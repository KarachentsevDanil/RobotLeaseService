using RLS.DAL.EF.Context;
using RLS.DAL.EF.Shared.Repositories;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.Domain.Rentals;

namespace RLS.DAL.EF.Repositories.Robots
{
    public class RentalRobotRepository : BaseRepository<int, RentalRobot, RentalDbContext>, IRentalRobotRepository
    {
        public RentalRobotRepository(RentalDbContext context) : base(context)
        {
        }
    }
}