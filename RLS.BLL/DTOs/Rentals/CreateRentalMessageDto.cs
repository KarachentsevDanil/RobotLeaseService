namespace RLS.BLL.DTOs.Rentals
{
    public class CreateRentalMessageDto
    {
        public int RentalId { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }
    }
}