namespace RLS.BLL.DTOs.Rentals
{
    public class OwnerUpdateRentalDto
    {
        public int RentalId { get; set; }

        public double CustomerRating { get; set; }

        public string OwnerFeedback { get; set; }
    }
}