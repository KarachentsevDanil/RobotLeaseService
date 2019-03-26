using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotModelRepository : IGenericRepository<int, RobotModel>
    {
        Task<CollectionResult<RobotModel>> GetModelsByFilterParamsAsync(RobotModelFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotModel>> GetRobotModelsByTypeAsync(int typeId, CancellationToken ct = default);
    }
}