using System.Collections.Generic;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Types;
using RLS.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotTypeService
    {
        Task<GetRobotTypeDto> CreateRobotTypeAsync(CreateRobotTypeDto item, CancellationToken ct = default);

        Task<GetRobotTypeDto> UpdateRobotTypeAsync(UpdateRobotTypeDto item, CancellationToken ct = default);

        Task<GetRobotTypeDto> GetRobotTypeAsync(int id, CancellationToken ct = default);

        Task<CollectionResult<GetRobotTypeDto>> GetRobotTypesByFilterParamsAsync(
            RobotTypeFilterParamsDto filterParams,
            CancellationToken ct = default);

        Task<IEnumerable<GetRobotTypeDto>> GetRobotTypesByTermAsync(
            string term,
            CancellationToken ct = default);
    }
}