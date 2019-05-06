using System;
using AutoMapper;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.Rentals;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Rentals
{
    public class RentalMessageService : IRentalMessageService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RentalMessageService(
            IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRentalMessageDto> CreateRentalMessageAsync(CreateRentalMessageDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<RentalMessage>(item);

            newItem.CreatedAt = DateTime.UtcNow;

            _unitOfWork.RentalMessageRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalMessageDto>(newItem);
        }

        public async Task UpdateRentalMessagesAsync(UpdateRentalMessagesDto items, CancellationToken ct = default)
        {
            var itemsToUpdate = await _unitOfWork.RentalMessageRepository.GetRentalsByIdsAsync(items.Ids, ct);

            foreach (var item in itemsToUpdate)
            {
                item.IsRead = true;
                _unitOfWork.RentalMessageRepository.Update(item);
            }

            await _unitOfWork.CommitAsync(ct);
        }
    }
}