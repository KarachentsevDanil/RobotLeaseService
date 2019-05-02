﻿using RLS.Domain.Enums;
using RLS.Domain.Robots;
using RLS.Domain.Users;
using System;

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

        public int RobotId { get; set; }

        public Robot Robot { get; set; }

        public double RobotRating { get; set; }

        public double CustomerRating { get; set; }

        public string CustomerFeedback { get; set; }

        public string OwnerFeedback { get; set; }

        public string CancelReason { get; set; }
    }
}