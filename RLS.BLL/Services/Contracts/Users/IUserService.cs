using RLS.BLL.DTOs.Users;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Users
{
    public interface IUserService
    {
        Task<GetUserDto> GetUserByTermAsync(string term, CancellationToken ct = default);

        Task<GetUserDto> GetUserByIdAsync(string id, CancellationToken ct = default);

        Task AddUserSearchResultAsync(string userId, CancellationToken ct = default);
    }
}