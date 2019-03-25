using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Robots.Types;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotTypeService
    {
        Task<GetRobotDto> CreateRobotTypeAsync(CreateRobotTypeDto item, CancellationToken ct = default);

        Task<GetRobotDto> UpdateRobotTypeAsync(UpdateRobotTypeDto item, CancellationToken ct = default);

        Task<GetRobotDto> GetRobotTypeAsync(int id, CancellationToken ct = default);
    }
}