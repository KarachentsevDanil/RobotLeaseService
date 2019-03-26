using RLS.Domain.Models;
using RLS.Domain.Robots;

namespace RLS.Domain.FilterParams.Robots
{
    public class RobotModelFilterParams : BaseFilterParams<RobotModel>
    {
        public string Term { get; set; }
    }
}