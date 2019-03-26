using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.DAL.Repositories.Contracts.Users;

namespace RLS.DAL.UnitOfWork.Contracts
{
    public interface IRentalUnitOfWork : IUnitOfWork
    {
        IRentalRepository RentalRepository { get; }

        IRentalRobotRepository RentalRobotRepository { get; }

        IRobotRepository RobotRepository { get; }

        IRobotModelRepository RobotModelRepository { get; }

        IRobotTypeRepository RobotTypeRepository { get; }

        IRobotCompanyRepository RobotCompanyRepository { get; }

        IUserRepository UserRepository { get; }
    }
}