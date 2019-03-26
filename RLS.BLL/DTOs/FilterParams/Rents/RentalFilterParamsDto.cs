using RLS.Domain.Enums;

namespace RLS.BLL.DTOs.FilterParams.Rents
{
    public class RentalFilterParamsDto : BaseFilterParamsDto
    {
        public string UserId { get; set; }

        public RentalStatus? Status { get; set; }
    }
}