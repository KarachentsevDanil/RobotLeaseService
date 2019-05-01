namespace RLS.BLL.DTOs.Robots.Types
{
    public class GetRobotTypePopularityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountOfRobots { get; set; }

        public int CountOfRents { get; set; }
    }
}