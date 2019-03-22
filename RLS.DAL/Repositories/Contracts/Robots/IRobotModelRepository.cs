using RLS.DAL.Shared.Repositories.Contracts;
using RLS.Domain.Robots;
using RLS.Domain.Shared.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotModelRepository : IGenericRepository<int, RobotModel>
    {
        Task<CollectionResult<RobotModel>> GetModelsByFilterParamsAsync(RobotModelFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotModel>> GetRobotModelsByTypeAsync(int typeId, CancellationToken ct = default);
    }
}