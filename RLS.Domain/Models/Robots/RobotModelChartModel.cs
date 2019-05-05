namespace RLS.Domain.Models.Robots
{
    public class RobotModelChartModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public int RobotCount { get; set; }

        public int RentalCount { get; set; }
    }
}