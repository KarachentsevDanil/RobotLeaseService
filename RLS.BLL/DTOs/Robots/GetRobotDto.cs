using RLS.BLL.DTOs.Robots.Models;
using RLS.BLL.DTOs.Users;

namespace RLS.BLL.DTOs.Robots
{
    public class GetRobotDto
    {
        public int Id { get; set; }

        public double DailyCosts { get; set; }

        public GetRobotModelDto Model { get; set; }

        public GetUserDto User { get; set; }
    }
}