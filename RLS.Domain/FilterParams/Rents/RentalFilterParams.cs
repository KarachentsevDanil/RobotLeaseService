using RLS.Domain.Enums;
using RLS.Domain.Rentals;
using RLS.Domain.Shared.Models;

namespace RLS.Domain.FilterParams.Rents
{
    public class RentalFilterParams : BaseFilterParams<Rental>
    {
        public string UserId { get; set; }

        public RentalStatus? Status { get; set; }
    }
}