using RLS.Domain.Models;

namespace RLS.Domain.Robots
{
    public class RobotCompanyFilterParams : BaseFilterParams<RobotCompany>
    {
        public string Term { get; set; }
    }
}