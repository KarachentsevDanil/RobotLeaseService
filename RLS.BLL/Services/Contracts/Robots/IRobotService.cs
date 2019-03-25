using RLS.BLL.DTOs.Robots;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotService
    {
        Task<GetRobotDto> CreateRobotAsync(CreateRobotDto item, CancellationToken ct = default);

        Task<GetRobotDto> UpdateRobotAsync(UpdateRobotDto item, CancellationToken ct = default);

        Task<GetRobotDto> GetRobotAsync(int id, CancellationToken ct = default);
    }
}