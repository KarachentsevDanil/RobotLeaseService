using System;
using System.Collections.Generic;

namespace RLS.BLL.DTOs.Rentals
{
    public class CreateRentalDto
    {
        public string UserId { get; set; }

        public IEnumerable<CreateRentalRobotDto> Robots { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}