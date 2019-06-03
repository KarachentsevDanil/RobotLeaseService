using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Rents;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.FilterParams.Rents;
using RLS.Domain.Models;
using RLS.Domain.Rentals;
using System;
using System.Threading;
using System.Threading.Tasks;
using RLS.BLL.Configurations;
using RLS.BLL.DTOs.Internal.Messages;
using RLS.BLL.Services.Contracts.Internal;
using RLS.Domain.Enums;

namespace RLS.BLL.Services.Rentals
{
    public class RentalService : IRentalService
    {
        private readonly IMessageSendService<EmailMessage> _emailSendService;

        private readonly EmailSenderConfiguration _configuration;

        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RentalService(IRentalUnitOfWork unitOfWork,
            IMapper mapper,
            IMessageSendService<EmailMessage> emailSendService,
            EmailSenderConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSendService = emailSendService;
            _configuration = configuration;
        }

        public async Task<CollectionResult<GetRentalDto>> GetRentalsByFilterParamsAsync(
            RentalFilterParamsDto filterParams,
            CancellationToken ct = default)
        {
            var rentalFilterParams = _mapper.Map<RentalFilterParams>(filterParams);

            var rentals =
                await _unitOfWork.RentalRepository.GetRentalsByFilterParamsAsync(rentalFilterParams, ct);

            return _mapper.Map<CollectionResult<GetRentalDto>>(rentals);

        }

        public async Task<CollectionResult<GetRentalForCalendarDto>> GetRentalsForCalendarByFilterParamsAsync(
            RentalFilterParamsDto filterParams, CancellationToken ct = default)
        {
            var rentalFilterParams = _mapper.Map<RentalFilterParams>(filterParams);

            var rentals =
                await _unitOfWork.RentalRepository.GetRentalsByFilterParamsAsync(rentalFilterParams, ct);

            return _mapper.Map<CollectionResult<GetRentalForCalendarDto>>(rentals);
        }

        public async Task<GetRentalDto> CreateRentalAsync(CreateRentalDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<Rental>(item);

            _unitOfWork.RentalRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalDto>(newItem);
        }

        public async Task<GetRentalDto> CustomerUpdateRentalAsync(CustomerUpdateRentalDto item, CancellationToken ct = default)
        {
            var itemToUpdate = await _unitOfWork.RentalRepository.GetAsync(item.RentalId, ct);

            _mapper.Map(item, itemToUpdate);

            itemToUpdate.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.RentalRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalDto>(itemToUpdate);
        }

        public async Task<GetRentalDto> OwnerUpdateRentalAsync(OwnerUpdateRentalDto item, CancellationToken ct = default)
        {
            var itemToUpdate = await _unitOfWork.RentalRepository.GetAsync(item.RentalId, ct);

            _mapper.Map(item, itemToUpdate);

            itemToUpdate.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.RentalRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalDto>(itemToUpdate);
        }

        public async Task<GetRentalDto> GetRentalAsync(int id, CancellationToken ct = default)
        {
            var result = await _unitOfWork.RentalRepository.GetAsync(id, ct);

            return _mapper.Map<GetRentalDto>(result);
        }

        public async Task SendRentalNotificationAsync(CancellationToken ct = default)
        {
            var uncompletedRents = await _unitOfWork.RentalRepository.GetUncompletedRentalsAsync(ct);

            foreach (var rent in uncompletedRents)
            {
                if (rent.EndDate.Date <= DateTime.Now.Date)
                {
                    rent.Status = RentalStatus.Completed;
                    _unitOfWork.RentalRepository.Update(rent);

                    await _unitOfWork.CommitAsync(ct);

                    EmailMessage message = new EmailMessage
                    {
                       SendToEmail = rent.User.Email,
                       Subject = _configuration.AutomaticallyCloseRentMessageSubject,
                       Message = string.Format(
                           _configuration.AutomaticallyCloseRentMessageTemplate,
                           $"{rent.Id} - {rent.Robot.Model.Company.Name} {rent.Robot.Model.Name}",
                           rent.Id)
                    };

                    await _emailSendService.SendMessageAsync(message, ct);

                    message.SendToEmail = rent.Robot.User.Email;

                    await _emailSendService.SendMessageAsync(message, ct);

                    continue;
                }

                //Send notification to user until
                int hoursToComplete = (rent.EndDate - DateTime.Now).Hours;

                if (hoursToComplete <= 24)
                {
                    EmailMessage message = new EmailMessage
                    {
                        SendToEmail = rent.User.Email,
                        Subject = _configuration.ReturnRobotMessageSubject,
                        Message = string.Format(
                            _configuration.ReturnRobotMessageTemplate,
                            $"{rent.Id} - {rent.Robot.Model.Company.Name} {rent.Robot.Model.Name}",
                            rent.Id,
                            hoursToComplete)
                    };

                    await _emailSendService.SendMessageAsync(message, ct);
                }
            }
        }

        public async Task<GetRentalDto> UpdateRentalAsync(UpdateRentalDto item, CancellationToken ct = default)
        {
            var itemToUpdate = await _unitOfWork.RentalRepository.GetAsync(item.Id, ct);

            _mapper.Map(item, itemToUpdate);

            itemToUpdate.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.RentalRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalDto>(itemToUpdate);
        }
    }
}