using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Models;
using RLS.BLL.Services.Contracts.Robots;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Robots
{
    public class RobotModelService : IRobotModelService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RobotModelService(IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRobotModelDto> CreateRobotModelAsync(CreateRobotModelDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<RobotModel>(item);

            _unitOfWork.RobotModelRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotModelDto>(newItem);
        }

        public async Task<GetRobotModelDto> UpdateRobotModelAsync(UpdateRobotModelDto item, CancellationToken ct = default)
        {
            var itemToUpdate = _mapper.Map<RobotModel>(item);

            _unitOfWork.RobotModelRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotModelDto>(itemToUpdate);
        }

        public async Task<GetRobotModelDto> GetRobotModelAsync(int id, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotModelRepository.GetAsync(id, ct);

            return _mapper.Map<GetRobotModelDto>(item);
        }

        public async Task<CollectionResult<GetRobotModelDto>> GetModelsByFilterParamsAsync(
            RobotModelFilterParamsDto filterParams,
            CancellationToken ct = default)
        {
            var robotModelFilterParams = _mapper.Map<RobotModelFilterParams>(filterParams);

            var robotModels =
                await _unitOfWork.RobotModelRepository.GetModelsByFilterParamsAsync(robotModelFilterParams, ct);

            return _mapper.Map<CollectionResult<GetRobotModelDto>>(robotModels);
        }

        public async Task<IEnumerable<GetRobotModelDto>> GetRobotModelsByTypeAsync(int typeId, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotModelRepository.GetRobotModelsByTypeAsync(typeId, ct);

            return _mapper.Map<IEnumerable<GetRobotModelDto>>(item);
        }
    }
}