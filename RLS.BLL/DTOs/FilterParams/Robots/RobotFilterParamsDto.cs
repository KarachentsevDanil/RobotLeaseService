using System;
using System.Collections.Generic;

namespace RLS.BLL.DTOs.FilterParams.Robots
{
    public class RobotFilterParamsDto : BaseFilterParamsDto
    {
        public string UserId { get; set; }

        public IEnumerable<int> ModelIds { get; set; }

        public IEnumerable<int> TypeIds { get; set; }

        public IEnumerable<int> CompaniesIds { get; set; }

        public string Term { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsSearchView { get; set; }
    }
}