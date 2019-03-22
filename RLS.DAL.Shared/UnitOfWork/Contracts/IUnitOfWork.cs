using System;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Shared.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken token = default);
    }
}