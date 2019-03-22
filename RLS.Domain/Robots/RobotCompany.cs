using System.Collections.Generic;

namespace RLS.Domain.Robots
{
    public class RobotCompany
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<RobotModel> Models { get; set; }

        public RobotCompany()
        {
            Models = new List<RobotModel>();
        }
    }
}