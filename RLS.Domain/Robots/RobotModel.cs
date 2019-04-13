using System.Collections.Generic;

namespace RLS.Domain.Robots
{
    public class RobotModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public int TypeId { get; set; }

        public RobotType Type { get; set; }

        public int CompanyId { get; set; }

        public RobotCompany Company { get; set; }

        public ICollection<Robot> Robots { get; set; }

        public RobotModel()
        {
            Robots = new List<Robot>();
        }
    }
}