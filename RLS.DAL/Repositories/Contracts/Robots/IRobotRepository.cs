using RLS.DAL.Shared.Repositories.Contracts;
using RLS.Domain.Robots;
using RLS.Domain.Shared.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotRepository : IGenericRepository<int, Robot>
    {
        Task<CollectionResult<Robot>> GetRobotByFilterParamsAsync(RobotFilterParams filterParams, CancellationToken ct = default);
    }
}