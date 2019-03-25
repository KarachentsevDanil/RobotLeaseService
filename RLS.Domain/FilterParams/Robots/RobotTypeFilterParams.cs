using RLS.Domain.Robots;
using RLS.Domain.Shared.Models;

namespace RLS.Domain.FilterParams.Robots
{
    public class RobotTypeFilterParams : BaseFilterParams<RobotType>
    {
        public string Term { get; set; }
    }
}