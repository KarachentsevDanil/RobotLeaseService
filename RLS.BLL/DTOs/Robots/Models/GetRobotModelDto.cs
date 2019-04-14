namespace RLS.BLL.DTOs.Robots.Models
{
    public class GetRobotModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public string TypeId { get; set; }

        public string CompanyId { get; set; }

        public string TypeName { get; set; }

        public string CompanyName { get; set; }
    }
}