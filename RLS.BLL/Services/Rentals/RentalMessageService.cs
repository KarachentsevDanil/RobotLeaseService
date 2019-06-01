using AutoMapper;
using RLS.BLL.Configurations;
using RLS.BLL.DTOs.Internal.Messages;
using RLS.BLL.DTOs.Rentals;
using RLS.BLL.Services.Contracts.Internal;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.Rentals;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Rentals
{
    public class RentalMessageService : IRentalMessageService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMessageSendService<EmailMessage> _emailSendService;

        private readonly EmailSenderConfiguration _emailSenderConfiguration;

        private readonly IMapper _mapper;

        public RentalMessageService(
            IRentalUnitOfWork unitOfWork,
            IMapper mapper,
            IMessageSendService<EmailMessage> emailSendService,
            EmailSenderConfiguration emailSenderConfiguration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSendService = emailSendService;
            _emailSenderConfiguration = emailSenderConfiguration;
        }

        public async Task<GetRentalMessageDto> CreateRentalMessageAsync(CreateRentalMessageDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<RentalMessage>(item);

            newItem.CreatedAt = DateTime.UtcNow;

            _unitOfWork.RentalMessageRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRentalMessageDto>(newItem);
        }

        public async Task SendEmailAsync(CreateRentalMessageDto item, CancellationToken ct = default)
        {
            var rental = await _unitOfWork.RentalRepository.GetAsync(item.RentalId, ct);

            string sendTo = string.Empty;

            string sendFrom = string.Empty;

            if (item.UserId == rental.UserId)
            {
                sendTo = rental.Robot.User.Email;
                sendFrom = rental.User.Email;
            }

            if (item.UserId == rental.Robot.UserId)
            {
                sendFrom = rental.Robot.User.Email;
                sendTo = rental.User.Email;
            }

            string messageToSend = string.Format(
                _emailSenderConfiguration.NewMessageNotificationTemplate,
                sendFrom,
                $"{rental.Id} - {rental.Robot.Model.Company.Name} {rental.Robot.Model.Name}",
                rental.Id,
                item.Message);

            EmailMessage message = new EmailMessage
            {
                Message = messageToSend,
                Subject = _emailSenderConfiguration.NewMessageNotificationSubject,
                SendToEmail = sendTo
            };

            await _emailSendService.SendMessageAsync(message, ct);
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