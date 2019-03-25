using RLS.BLL.DTOs.Robots.Companies;
using RLS.BLL.DTOs.Robots.Types;

namespace RLS.BLL.DTOs.Robots.Models
{
    public class GetRobotModelDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GetRobotTypeDto Type { get; set; }

        public GetRobotCompanyDto Company { get; set; }
    }
}