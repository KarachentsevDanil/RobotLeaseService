using System;
using RLS.Domain.Users;

namespace RLS.Domain.Rentals
{
    public class RentalMessage
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsRead { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public Rental Rental { get; set; }
    }
}