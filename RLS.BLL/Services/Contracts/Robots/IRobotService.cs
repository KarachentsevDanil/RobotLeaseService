using RLS.BLL.DTOs.Dashboards;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotService
    {
        Task<GetRobotDto> CreateRobotAsync(CreateRobotDto item, CancellationToken ct = default);

        Task<GetRobotDto> UpdateRobotAsync(UpdateRobotDto item, CancellationToken ct = default);

        Task<GetRobotDto> GetRobotAsync(int id, CancellationToken ct = default);

        Task<CollectionResult<GetRobotDto>> GetRobotsByFilterParamsAsync(
            RobotFilterParamsDto filterParams,
            CancellationToken ct = default);

        Task<IEnumerable<GetValuableRobotModelDto>> GetMostValuableRobotByFilterParamsAsync(
            RobotMostValuableFilterParamsDto filterParams, CancellationToken ct = default);

        Task<DashboardStatisticDto> GetDashboardStatisticModelAsync(CancellationToken ct = default);

        Task<CollectionResult<GetDashboardRobotDto>> GetDashboardRobotByFilterParamsAsync(
            RobotFilterParamsDto filterParams, CancellationToken ct = default);
    }
}