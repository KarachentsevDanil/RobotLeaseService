using RLS.Domain.Robots;
using RLS.Domain.Shared.Models;

namespace RLS.Domain.FilterParams.Robots
{
    public class RobotModelFilterParams : BaseFilterParams<RobotModel>
    {
        public string Term { get; set; }
    }
}