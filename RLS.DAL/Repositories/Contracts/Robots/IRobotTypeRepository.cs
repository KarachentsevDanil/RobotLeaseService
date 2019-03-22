using RLS.DAL.Shared.Repositories.Contracts;
using RLS.Domain.Robots;
using RLS.Domain.Shared.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotTypeRepository : IGenericRepository<int, RobotType>
    {
        Task<CollectionResult<RobotType>> GetTypesByFilterParamsAsync(RobotTypeFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotType>> GetRobotTypesByTermAsync(string term, CancellationToken ct = default);
    }
}