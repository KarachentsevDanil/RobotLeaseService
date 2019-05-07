using System.Collections.Generic;

namespace RLS.BLL.DTOs.Robots
{
    public class GetRobotDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double DailyCosts { get; set; }

        public string ModelId { get; set; }

        public string ModelName { get; set; }

        public string Icon { get; set; }

        public string Photo { get; set; }

        public string TypeId { get; set; }

        public string TypeName { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserFullName { get; set; }

        public string UserPhone { get; set; }

        public double AvarageRating { get; set; }

        public IEnumerable<GetShortRobotRentalDto> Rentals { get; set; }
    }
}