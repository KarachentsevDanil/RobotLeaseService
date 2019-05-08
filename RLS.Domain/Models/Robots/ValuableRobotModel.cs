namespace RLS.Domain.Models.Robots
{
    public class ValuableRobotModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public double DailyCosts { get; set; }

        public string RobotTypeName { get; set; }

        public byte[] Icon { get; set; }

        public byte[] Photo { get; set; }

        public double AverageRating { get; set; }
    }
}