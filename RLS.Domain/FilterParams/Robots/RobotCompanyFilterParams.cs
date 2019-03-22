using RLS.Domain.Shared.Shared.Models;

namespace RLS.Domain.Robots
{
    public class RobotCompanyFilterParams : BaseFilterParams<RobotCompany>
    {
        public string Term { get; set; }
    }
}