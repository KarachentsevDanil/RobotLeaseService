using RLS.BLL.DTOs.Robots;
using RLS.BLL.DTOs.Users;
using System;

namespace RLS.BLL.DTOs.Rentals
{
    public class GetRentalDto
    {
        public int Id { get; set; }

        public GetUserDto Owner { get; set; }

        public GetUserDto Customer { get; set; }

        public GetRobotDto Robot { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }
    }
}