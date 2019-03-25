using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Users;
using System;
using System.Collections.Generic;

namespace RLS.BLL.DTOs.Rentals
{
    public class GetRentalDto
    {
        public int Id { get; set; }

        public GetUserDto User { get; set; }

        public IEnumerable<GetRobotDto> Robots { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}