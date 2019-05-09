namespace RLS.BLL.DTOs.Robots
{
    public class GetRobotFeedbackDto
    {
        public int RobotId { get; set; }

        public double RobotRating { get; set; }

        public string CustomerFeedback { get; set; }
    }
}