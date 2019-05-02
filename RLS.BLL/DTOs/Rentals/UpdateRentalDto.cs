using RLS.Domain.Enums;

namespace RLS.BLL.DTOs.Rentals
{
    public class UpdateRentalDto
    {
        public int Id { get; set; }

        public RentalStatus Status { get; set; }

        public string CancelReason { get; set; }
    }
}