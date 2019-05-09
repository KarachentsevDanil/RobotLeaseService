using System.Collections.Generic;

namespace RLS.BLL.DTOs.Robots
{
    public class GetDashboardRobotDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public double DailyCosts { get; set; }

        public string RobotTypeName { get; set; }

        public string Icon { get; set; }

        public string Photo { get; set; }

        public double AverageRating { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<GetRobotFeedbackDto> Feedback { get; set; }
    }
}