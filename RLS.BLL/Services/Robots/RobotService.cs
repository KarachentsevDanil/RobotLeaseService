using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.Services.Contracts.Robots;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RLS.BLL.DTOs.Dashboards;

namespace RLS.BLL.Services.Robots
{
    public class RobotService : IRobotService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RobotService(IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            return _mapper.Map<CollectionResult<GetRobotDto>>(robotModels);
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
    }
}