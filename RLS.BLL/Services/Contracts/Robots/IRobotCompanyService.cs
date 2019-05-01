using System.Collections.Generic;
using RLS.BLL.DTOs.FilterParams.Robots;
using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Robots.Companies;
using RLS.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Robots
{
    public interface IRobotCompanyService
    {
        Task<GetRobotCompanyDto> CreateRobotCompanyAsync(CreateRobotCompanyDto item, CancellationToken ct = default);

        Task<GetRobotCompanyDto> UpdateRobotCompanyAsync(UpdateRobotCompanyDto item, CancellationToken ct = default);

        Task<GetRobotCompanyDto> GetRobotCompanyAsync(int id, CancellationToken ct = default);

        Task<CollectionResult<GetRobotCompanyDto>> GetCompaniesByFilterParamsAsync(
            RobotCompanyFilterParamsDto filterParamsDto,
            CancellationToken ct = default);

        Task<IEnumerable<GetRobotCompanyDto>> GetRobotCompaniesByTermAsync(
            string term,
            CancellationToken ct = default);

        Task<IEnumerable<GetRobotCompanyPopularityDto>> GetTopNPopularCompaniesAsync(RobotPopularityFilterParamsDto filterParams, CancellationToken ct = default);
    }
}