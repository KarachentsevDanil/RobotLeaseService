namespace RLS.Domain.Models.Robots
{
    public class RobotCompanyChartModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RobotCount { get; set; }

        public int RentalCount { get; set; }
    }
}