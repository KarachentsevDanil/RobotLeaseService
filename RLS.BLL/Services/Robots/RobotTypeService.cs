using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Types;
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
    public class RobotTypeService : IRobotTypeService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RobotTypeService(IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRobotTypeDto> CreateRobotTypeAsync(CreateRobotTypeDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<RobotType>(item);

            _unitOfWork.RobotTypeRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotTypeDto>(newItem);
        }

        public async Task<GetRobotTypeDto> UpdateRobotTypeAsync(UpdateRobotTypeDto item, CancellationToken ct = default)
        {
            var itemToUpdate = _mapper.Map<RobotType>(item);

            _unitOfWork.RobotTypeRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotTypeDto>(itemToUpdate);
        }

        public async Task<GetRobotTypeDto> GetRobotTypeAsync(int id, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotTypeRepository.GetAsync(id, ct);

            return _mapper.Map<GetRobotTypeDto>(item);
        }

        public async Task<CollectionResult<GetRobotTypeDto>> GetRobotTypesByFilterParamsAsync(
            RobotTypeFilterParamsDto filterParamsDto,
            CancellationToken ct = default)
        {
            var filterParams = _mapper.Map<RobotTypeFilterParams>(filterParamsDto);

            var robotModels =
                await _unitOfWork.RobotTypeRepository.GetTypesByFilterParamsAsync(filterParams, ct);

            return _mapper.Map<CollectionResult<GetRobotTypeDto>>(robotModels);
        }

        public async Task<IEnumerable<GetRobotTypeDto>> GetRobotTypesByTermAsync(string term, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotTypeRepository.GetRobotTypesByTermAsync(term, ct);

            return _mapper.Map<IEnumerable<GetRobotTypeDto>>(item);
        }

        public async Task<IEnumerable<GetRobotTypePopularityDto>> GetTopNPopularTypesAsync(RobotPopularityFilterParamsDto filterParams, CancellationToken ct = default)
        {
            var robotModelFilterParams = _mapper.Map<RobotPopularityFilterParams>(filterParams);

            var items = await _unitOfWork.RobotTypeRepository.GetTopNPopularTypesAsync(robotModelFilterParams, ct);

            return _mapper.Map<IEnumerable<GetRobotTypePopularityDto>>(items);
        }
    }
}