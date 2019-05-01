using RLS.Domain.Enums;

namespace RLS.BLL.DTOs.FilterParams.Robots
{
    public class RobotPopularityFilterParamsDto
    {
        public RobotPopularity Type { get; set; }

        public int CountToTake { get; set; }
    }
}