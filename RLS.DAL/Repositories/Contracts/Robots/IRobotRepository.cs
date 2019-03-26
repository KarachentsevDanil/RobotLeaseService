using RLS.Domain.Robots;
using System.Threading;
using System.Threading.Tasks;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotRepository : IGenericRepository<int, Robot>
    {
        Task<CollectionResult<Robot>> GetRobotByFilterParamsAsync(RobotFilterParams filterParams, CancellationToken ct = default);
    }
}