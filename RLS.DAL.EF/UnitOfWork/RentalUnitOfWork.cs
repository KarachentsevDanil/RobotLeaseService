using RLS.DAL.EF.Context;
using RLS.DAL.EF.Repositories.Robots;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.DAL.Repositories.Contracts.Users;
using RLS.DAL.UnitOfWork.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using RLS.DAL.EF.Repositories.Rentals;
using RLS.DAL.EF.Repositories.Users;

namespace RLS.DAL.EF.UnitOfWork
{
    public class RentalUnitOfWork : IRentalUnitOfWork
    {
        private readonly RentalDbContext _context;

        public IRentalRepository RentalRepository { get; }

        public IRentalMessageRepository RentalMessageRepository { get; }

        public IRobotRepository RobotRepository { get; }

        public IRobotModelRepository RobotModelRepository { get; }

        public IRobotTypeRepository RobotTypeRepository { get; }

        public IRobotCompanyRepository RobotCompanyRepository { get; }

        public IUserRepository UserRepository { get; }

        public IUserInterestsSearchRepository UserInterestsSearchRepository { get; }

        public IFavoriteUserRobotRepository FavoriteUserRobotRepository { get; }

        public RentalUnitOfWork(RentalDbContext context)
        {
            _context = context;

            RentalRepository = new RentalRepository(context);
            RentalMessageRepository = new RentalMessageRepository(context);
            RobotRepository = new RobotRepository(context);
            RobotModelRepository = new RobotModelRepository(context);
            RobotTypeRepository = new RobotTypeRepository(context);
            RobotCompanyRepository = new RobotCompanyRepository(context);
            UserRepository = new UserRepository(context);
            UserInterestsSearchRepository = new UserInterestsSearchRepository(context);
            FavoriteUserRobotRepository = new FavoriteUserRobotRepository(context);
        }

        public async Task CommitAsync(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
