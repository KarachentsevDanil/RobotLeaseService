namespace RLS.BLL.DTOs.Robots
{
    public class GetValuableRobotModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public double DailyCosts { get; set; }

        public string RobotTypeName { get; set; }

        public string Icon { get; set; }

        public string Photo { get; set; }

        public double AverageRating { get; set; }
    }
}