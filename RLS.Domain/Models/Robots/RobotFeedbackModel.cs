namespace RLS.Domain.Models.Robots
{
    public class RobotFeedbackModel
    {
        public int RobotId { get; set; }

        public double RobotRating { get; set; }

        public string CustomerFeedback { get; set; }
    }
}