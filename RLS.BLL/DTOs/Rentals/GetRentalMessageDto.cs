using RLS.BLL.DTOs.Users;
using System;

namespace RLS.BLL.DTOs.Rentals
{
    public class GetRentalMessageDto
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsRead { get; set; }

        public GetUserDto User { get; set; }
    }
}