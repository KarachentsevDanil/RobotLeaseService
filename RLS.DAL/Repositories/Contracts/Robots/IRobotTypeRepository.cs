using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Models.Robots;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotTypeRepository : IGenericRepository<int, RobotType>
    {
        Task<CollectionResult<RobotType>> GetTypesByFilterParamsAsync(RobotTypeFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotType>> GetRobotTypesByTermAsync(string term, CancellationToken ct = default);

        Task<IEnumerable<RobotTypeChartModel>> GetTopNPopularTypesAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default);
    }
}