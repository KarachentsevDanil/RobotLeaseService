using System.Collections.Generic;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Models;
using RLS.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotModelService
    {
        Task<GetRobotModelDto> CreateRobotModelAsync(CreateRobotModelDto item, CancellationToken ct = default);

        Task<GetRobotModelDto> UpdateRobotModelAsync(UpdateRobotModelDto item, CancellationToken ct = default);

        Task<GetRobotModelDto> GetRobotModelAsync(int id, CancellationToken ct = default);

        Task<CollectionResult<GetRobotModelDto>> GetModelsByFilterParamsAsync(
            RobotModelFilterParamsDto filterParams,
            CancellationToken ct = default);

        Task<IEnumerable<GetRobotModelDto>> GetRobotModelsAsync(CancellationToken ct = default);
    }
}