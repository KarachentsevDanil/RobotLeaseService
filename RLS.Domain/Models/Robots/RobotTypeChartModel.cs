namespace RLS.Domain.Models.Robots
{
    public class RobotTypeChartModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RobotCount { get; set; }

        public int RentalCount { get; set; }
    }
}