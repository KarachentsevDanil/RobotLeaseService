using RLS.DAL.Shared.Repositories.Contracts;
using RLS.Domain.Rentals;

namespace RLS.DAL.Repositories.Contracts.Rentals
{
    public interface IRentalRobotRepository : IGenericRepository<int, RentalRobot>
    {
    }
}