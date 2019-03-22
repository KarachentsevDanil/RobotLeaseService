using RLS.Domain.Shared.Shared.Models;

namespace RLS.Domain.Robots
{
    public class RobotModelFilterParams : BaseFilterParams<RobotModel>
    {
        public string Term { get; set; }
    }
}