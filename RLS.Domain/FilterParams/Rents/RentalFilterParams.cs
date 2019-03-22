using RLS.Domain.Enums;
using RLS.Domain.Shared.Shared.Models;

namespace RLS.Domain.Rentals
{
    public class RentalFilterParams : BaseFilterParams<Rental>
    {
        public string UserId { get; set; }

        public RentalStatus? Status { get; set; }
    }
}