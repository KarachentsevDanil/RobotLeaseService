using System;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.UnitOfWork.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken token = default);
    }
}