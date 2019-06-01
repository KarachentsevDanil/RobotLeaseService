using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.Services.Contracts.Robots;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RLS.BLL.Configurations;
using RLS.BLL.DTOs.Dashboards;
using RLS.BLL.DTOs.Internal.Messages;
using RLS.BLL.Services.Contracts.Internal;
using RLS.BLL.Services.Internal.Messages;
using RLS.Domain.Users;

namespace RLS.BLL.Services.Robots
{
    public class RobotService : IRobotService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMessageSendService<EmailMessage> _emailSendService;

        private readonly EmailSenderConfiguration _configuration;

        private readonly IMapper _mapper;

        public RobotService(
            IRentalUnitOfWork unitOfWork,
            IMessageSendService<EmailMessage> emailSendService,
            IMapper mapper,
            EmailSenderConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _emailSendService = emailSendService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<GetRobotDto> CreateRobotAsync(CreateRobotDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<Robot>(item);

            _unitOfWork.RobotRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotDto>(newItem);
        }

        public async Task<GetRobotDto> UpdateRobotAsync(UpdateRobotDto item, CancellationToken ct = default)
        {
            var itemToUpdate = _mapper.Map<Robot>(item);

            _unitOfWork.RobotRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotDto>(itemToUpdate);
        }

        public async Task<GetRobotDto> GetRobotAsync(int id, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotRepository.GetAsync(id, ct);

            return _mapper.Map<GetRobotDto>(item);
        }

        public async Task<CollectionResult<GetRobotDto>> GetRobotsByFilterParamsAsync(
            RobotFilterParamsDto filterParamsDto,
            CancellationToken ct = default)
        {
            var filterParams = _mapper.Map<RobotFilterParams>(filterParamsDto);

            var robotModels =
                await _unitOfWork.RobotRepository.GetRobotByFilterParamsAsync(filterParams, ct);

            var result = _mapper.Map<CollectionResult<GetRobotDto>>(robotModels);

            foreach (var robot in result.Collection)
            {
                robot.IsFavorite = robot.UserFavorites.Any(t => t == filterParams.UserId);
            }

            return result;
        }

        public async Task<IEnumerable<GetValuableRobotModelDto>> GetMostValuableRobotByFilterParamsAsync(RobotMostValuableFilterParamsDto filterParamsDto,
            CancellationToken ct = default)
        {
            var filterParams = _mapper.Map<RobotMostValuableFilterParams>(filterParamsDto);

            var robotModels =
                await _unitOfWork.RobotRepository.GetMostValuableRobotByFilterParamsAsync(filterParams, ct);

            return _mapper.Map<IEnumerable<GetValuableRobotModelDto>>(robotModels);
        }

        public async Task<DashboardStatisticDto> GetDashboardStatisticModelAsync(CancellationToken ct = default)
        {
            var result =
                await _unitOfWork.RobotRepository.GetDashboardStatisticModelAsync(ct);

            return _mapper.Map<DashboardStatisticDto>(result);
        }

        public async Task<CollectionResult<GetDashboardRobotDto>> GetDashboardRobotByFilterParamsAsync(
            RobotFilterParamsDto filterParamsDto, CancellationToken ct = default)
        {
            var filterParams = _mapper.Map<RobotFilterParams>(filterParamsDto);

            var robotModels =
                await _unitOfWork.RobotRepository.GetDashboardRobotByFilterParamsAsync(filterParams, ct);

            return _mapper.Map<CollectionResult<GetDashboardRobotDto>>(robotModels);
        }

        public async Task SendNotificationByUserInterestsAsync(CancellationToken ct = default)
        {
            IEnumerable<UserInterestsSearch> userInterestsSearches =
                await _unitOfWork.UserInterestsSearchRepository.GetListAsync(ct);

            foreach (var item in userInterestsSearches)
            {
                Robot robot =
                    await _unitOfWork.RobotRepository.GetRobotByUserInterestsAsync(item.Interests, ct);

                if (robot != null)
                {
                    EmailMessage message = new EmailMessage
                    {
                        Subject = _configuration.NewRobotMessageSubject,
                        Message = string.Format(
                            _configuration.NewRobotMessageTemplate,
                            $"{robot.Model.Company.Name} {robot.Model.Name}",
                            robot.Id),
                        SendToEmail = item.User.Email
                    };

                    await _emailSendService.SendMessageAsync(message, ct);

                    _unitOfWork.UserInterestsSearchRepository.Delete(item.Id);

                    await _unitOfWork.CommitAsync(ct);
                }
            }
        }

        public async Task CreateFavoriteUserRobotAsync(string userId, int robotId, CancellationToken ct = default)
        {
            FavoriteUserRobot newItem = new FavoriteUserRobot
            {
                UserId = userId,
                RobotId = robotId
            };

            _unitOfWork.FavoriteUserRobotRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteFavoriteUserRobotAsync(string userId, int robotId, CancellationToken ct = default)
        {
            await _unitOfWork.FavoriteUserRobotRepository.DeleteRobotFromFavoriteAsync(userId, robotId, ct);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}