namespace RLS.BLL.DTOs.Robots.Models
{
    public class UpdateRobotModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public int TypeId { get; set; }

        public int CompanyId { get; set; }
    }
}