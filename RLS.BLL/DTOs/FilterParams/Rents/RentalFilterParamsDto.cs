using System;
using RLS.Domain.Enums;

namespace RLS.BLL.DTOs.FilterParams.Rents
{
    public class RentalFilterParamsDto : BaseFilterParamsDto
    {
        public string UserId { get; set; }

        public bool IsOwnerView { get; set; }

        public RentalStatus? Status { get; set; }

        public int? RobotId { get; set; }

        public string Term { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
    }
}