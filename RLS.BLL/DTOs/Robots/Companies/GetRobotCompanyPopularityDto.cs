namespace RLS.BLL.DTOs.Robots.Companies
{
    public class GetRobotCompanyPopularityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountOfRobots { get; set; }

        public int CountOfRents { get; set; }
    }
}