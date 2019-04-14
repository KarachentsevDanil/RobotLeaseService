namespace RLS.BLL.DTOs.Robots
{
    public class GetRobotDto
    {
        public int Id { get; set; }

        public double DailyCosts { get; set; }

        public string ModelName { get; set; }

        public string TypeName { get; set; }

        public string CompanyName { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}