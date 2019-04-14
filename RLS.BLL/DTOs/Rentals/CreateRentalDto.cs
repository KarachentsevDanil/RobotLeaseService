using System;

namespace RLS.BLL.DTOs.Rentals
{
    public class CreateRentalDto
    {
        public string UserId { get; set; }

        public int RobotId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}