using RLS.Domain.FilterParams.Robots;
using RLS.Domain.Models;
using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotCompanyRepository : IGenericRepository<int, RobotCompany>
    {
        Task<CollectionResult<RobotCompany>> GetCompaniesByFilterParamsAsync(RobotCompanyFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotCompany>> GetRobotCompaniesByTermAsync(string term, CancellationToken ct = default);

        Task<IEnumerable<RobotCompany>> GetTopNPopularCompaniesAsync(RobotPopularityFilterParams filterParams, CancellationToken ct = default);
    }
}