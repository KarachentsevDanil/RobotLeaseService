using RLS.Domain.Models;
using RLS.Domain.Robots;

namespace RLS.Domain.FilterParams.Robots
{
    public class RobotTypeFilterParams : BaseFilterParams<RobotType>
    {
        public string Term { get; set; }
    }
}