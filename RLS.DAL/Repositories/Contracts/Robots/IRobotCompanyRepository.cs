using RLS.Domain.Robots;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RLS.Domain.Models;

namespace RLS.DAL.Repositories.Contracts.Robots
{
    public interface IRobotCompanyRepository : IGenericRepository<int, RobotCompany>
    {
        Task<CollectionResult<RobotCompany>> GetCompaniesByFilterParamsAsync(RobotCompanyFilterParams filterParams, CancellationToken ct = default);

        Task<IEnumerable<RobotCompany>> GetRobotCompaniesByTermAsync(string term, CancellationToken ct = default);
    }
}