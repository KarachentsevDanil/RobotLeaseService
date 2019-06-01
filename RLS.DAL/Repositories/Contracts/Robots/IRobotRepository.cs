using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Models.Robots;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotRepository : IGenericRepository<int, Robot>
    {
        Task<CollectionResult<Robot>> GetRobotByFilterParamsAsync(RobotFilterParams filterParams, CancellationToken ct = default);

        Task<CollectionResult<DashboardRobotModel>> GetDashboardRobotByFilterParamsAsync(
            RobotFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<ValuableRobotModel>> GetMostValuableRobotByFilterParamsAsync(
            RobotMostValuableFilterParams filterParams, CancellationToken ct = default);

        Task<DashboardStatisticModel> GetDashboardStatisticModelAsync(CancellationToken ct = default);

        Task<Robot> GetRobotByUserInterestsAsync(string userId, string interests, CancellationToken ct = default);
    }
}