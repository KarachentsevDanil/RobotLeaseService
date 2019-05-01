namespace RLS.BLL.DTOs.Robots.Models
{
    public class GetRobotModelPopularityDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CountOfRobots { get; set; }

        public int CountOfRents { get; set; }
    }
}