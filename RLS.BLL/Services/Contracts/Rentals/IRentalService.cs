using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.Rentals;
using RLS.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Rentals
{
    public interface IRentalService
    {
        Task<CollectionResult<GetRentalDto>> GetRentalsByFilterParamsAsync(
            RentalFilterParamsDto filterParams,
            CancellationToken ct = default);

        Task<CollectionResult<GetRentalForCalendarDto>> GetRentalsForCalendarByFilterParamsAsync(
            RentalFilterParamsDto filterParams,
            CancellationToken ct = default);

        Task<GetRentalDto> CreateRentalAsync(CreateRentalDto item, CancellationToken ct = default);

        Task<GetRentalDto> CustomerUpdateRentalAsync(CustomerUpdateRentalDto item, CancellationToken ct = default);

        Task<GetRentalDto> OwnerUpdateRentalAsync(OwnerUpdateRentalDto item, CancellationToken ct = default);

        Task<GetRentalDto> UpdateRentalAsync(UpdateRentalDto item, CancellationToken ct = default);

        Task<GetRentalDto> GetRentalAsync(int id, CancellationToken ct = default);
    }
}