﻿using RLS.Domain.Enums;
using RLS.Domain.Users;
using System;
using System.Collections.Generic;

namespace RLS.Domain.Rentals
{
    public class Rental
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public RentalStatus Status { get; set; }

        public ICollection<RentalRobot> Robots { get; set; }

        public Rental()
        {
            Robots = new List<RentalRobot>();
        }
    }
}
