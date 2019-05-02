using RLS.BLL.DTOs.Users;

namespace RLS.BLL.DTOs.Robots
{
    public class GetShortRobotRentalDto
    {
        public int Id { get; set; }

        public GetUserDto Customer { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public double RobotRating { get; set; }

        public double CustomerRating { get; set; }

        public string CustomerFeedback { get; set; }

        public string OwnerFeedback { get; set; }
    }
}