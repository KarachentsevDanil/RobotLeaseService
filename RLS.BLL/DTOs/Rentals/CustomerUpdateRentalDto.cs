namespace RLS.BLL.DTOs.Rentals
{
    public class CustomerUpdateRentalDto
    {
        public int RentalId { get; set; }

        public double RobotRating { get; set; }

        public string CustomerFeedback { get; set; }
    }
}