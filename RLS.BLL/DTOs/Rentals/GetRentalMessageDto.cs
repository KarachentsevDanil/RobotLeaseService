using RLS.BLL.DTOs.Users;

namespace RLS.BLL.DTOs.Rentals
{
    public class GetRentalMessageDto
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string CreatedAt { get; set; }

        public bool IsRead { get; set; }

        public GetUserDto User { get; set; }
    }
}