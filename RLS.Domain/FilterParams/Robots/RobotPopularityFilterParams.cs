using RLS.Domain.Enums;

namespace RLS.Domain.FilterParams.Robots
{
    public class RobotPopularityFilterParams
    {
        public RobotPopularity Type { get; set; }

        public int CountToTake { get; set; }

        public int? CompanyId { get; set; }

        public int? TypeId { get; set; }
    }
}