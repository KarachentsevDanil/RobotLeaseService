using System;
using RLS.Domain.Enums;
using RLS.Domain.Models;
using RLS.Domain.Rentals;

namespace RLS.Domain.FilterParams.Rents
{
    public class RentalFilterParams : BaseFilterParams<Rental>
    {
        public string UserId { get; set; }

        public RentalStatus? Status { get; set; }

        public int? RobotId { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public string Term { get; set; }

        public bool IsCalendarView { get; set; }
    }
}