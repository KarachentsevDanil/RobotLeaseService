using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotModelRepository : IGenericRepository<int, RobotModel>
    {
        Task<CollectionResult<RobotModel>> GetModelsByFilterParamsAsync(RobotModelFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotModel>> GetRobotModelsAsync(CancellationToken ct = default);
    }
}