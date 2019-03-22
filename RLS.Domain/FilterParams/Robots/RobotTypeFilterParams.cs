using RLS.Domain.Shared.Shared.Models;

namespace RLS.Domain.Robots
{
    public class RobotTypeFilterParams : BaseFilterParams<RobotType>
    {
        public string Term { get; set; }
    }
}