using RLS.BLL.DTOs.Rentals;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Rentals
{
    public interface IRentalMessageService
    {
        Task<GetRentalMessageDto> CreateRentalMessageAsync(CreateRentalMessageDto item, CancellationToken ct = default);

        Task SendEmailAsync(CreateRentalMessageDto item, CancellationToken ct = default);

        Task UpdateRentalMessagesAsync(UpdateRentalMessagesDto items, CancellationToken ct = default);
    }
}