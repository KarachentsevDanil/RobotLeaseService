using System.Collections.Generic;

namespace RLS.Domain.Robots
{
    public class RobotType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<RobotModel> Models { get; set; }

        public RobotType()
        {
            Models = new List<RobotModel>();
        }
    }
}