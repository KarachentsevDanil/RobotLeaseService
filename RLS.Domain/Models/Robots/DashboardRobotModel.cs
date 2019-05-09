using System.Collections.Generic;

namespace RLS.Domain.Models.Robots
{
    public class DashboardRobotModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public double DailyCosts { get; set; }

        public string RobotTypeName { get; set; }

        public byte[] Icon { get; set; }

        public byte[] Photo { get; set; }

        public double AverageRating { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<RobotFeedbackModel> Feedback { get; set; }

        public DashboardRobotModel()
        {
            Feedback = new List<RobotFeedbackModel>();
        }
    }
}