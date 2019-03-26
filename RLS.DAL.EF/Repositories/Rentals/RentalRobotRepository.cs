using RLS.DAL.EF.Context;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.Domain.Rentals;

namespace RLS.DAL.EF.Repositories.Rentals
{
    public class RentalRobotRepository : BaseRepository<int, RentalRobot, RentalDbContext>, IRentalRobotRepository
    {
        public RentalRobotRepository(RentalDbContext context) : base(context)
        {
        }
    }
}