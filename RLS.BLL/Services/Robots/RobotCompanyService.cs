using AutoMapper;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots.Companies;
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
    public class RobotCompanyService : IRobotCompanyService
    {
        private readonly IRentalUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public RobotCompanyService(IRentalUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetRobotCompanyDto> CreateRobotCompanyAsync(CreateRobotCompanyDto item, CancellationToken ct = default)
        {
            var newItem = _mapper.Map<RobotCompany>(item);

            _unitOfWork.RobotCompanyRepository.Create(newItem);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotCompanyDto>(newItem);
        }

        public async Task<GetRobotCompanyDto> UpdateRobotCompanyAsync(UpdateRobotCompanyDto item, CancellationToken ct = default)
        {
            var itemToUpdate = _mapper.Map<RobotCompany>(item);

            _unitOfWork.RobotCompanyRepository.Update(itemToUpdate);

            await _unitOfWork.CommitAsync(ct);

            return _mapper.Map<GetRobotCompanyDto>(itemToUpdate);
        }

        public async Task<GetRobotCompanyDto> GetRobotCompanyAsync(int id, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotCompanyRepository.GetAsync(id, ct);

            return _mapper.Map<GetRobotCompanyDto>(item);
        }

        public async Task<CollectionResult<GetRobotCompanyDto>> GetCompaniesByFilterParamsAsync(
            RobotCompanyFilterParamsDto filterParamsDto,
            CancellationToken ct = default)
        {
            var filterParams = _mapper.Map<RobotCompanyFilterParams>(filterParamsDto);

            var robotModels =
                await _unitOfWork.RobotCompanyRepository.GetCompaniesByFilterParamsAsync(filterParams, ct);

            return _mapper.Map<CollectionResult<GetRobotCompanyDto>>(robotModels);
        }

        public async Task<IEnumerable<GetRobotCompanyDto>> GetRobotCompaniesByTermAsync(string term, CancellationToken ct = default)
        {
            var item = await _unitOfWork.RobotCompanyRepository.GetRobotCompaniesByTermAsync(term, ct);

            return _mapper.Map<IEnumerable<GetRobotCompanyDto>>(item);
        }

        public async Task<IEnumerable<GetRobotCompanyPopularityDto>> GetTopNPopularCompaniesAsync(RobotPopularityFilterParamsDto filterParams, CancellationToken ct = default)
        {
            var robotModelFilterParams = _mapper.Map<RobotPopularityFilterParams>(filterParams);

            var items = await _unitOfWork.RobotCompanyRepository.GetTopNPopularCompaniesAsync(robotModelFilterParams, ct);

            return _mapper.Map<IEnumerable<GetRobotCompanyPopularityDto>>(items);
        }
    }
}