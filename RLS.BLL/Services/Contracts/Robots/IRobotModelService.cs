using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Robots.Models;
using RLS.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotModelService
    {
        Task<GetRobotDto> CreateRobotModelAsync(CreateRobotModelDto item, CancellationToken ct = default);

        Task<GetRobotDto> UpdateRobotModelAsync(UpdateRobotModelDto item, CancellationToken ct = default);

        Task<GetRobotDto> GetRobotModelAsync(int id, CancellationToken ct = default);

        Task<CollectionResult<GetRobotModelDto>> GetModelsByFilterParamsAsync(
            RobotModelFilterParamsDto filterParams,
            CancellationToken ct = default);
    }
}