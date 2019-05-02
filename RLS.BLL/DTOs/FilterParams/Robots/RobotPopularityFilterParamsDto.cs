using RLS.Domain.Enums;

namespace RLS.BLL.DTOs.FilterParams.Robots
{
    public class RobotPopularityFilterParamsDto
    {
        public RobotPopularity Type { get; set; }

        public int CountToTake { get; set; }

        public int? CompanyId { get; set; }

        public int? TypeId { get; set; }
    }
}