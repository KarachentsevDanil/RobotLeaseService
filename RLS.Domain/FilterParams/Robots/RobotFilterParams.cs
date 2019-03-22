using RLS.Domain.Shared.Shared.Models;
using System;
using System.Collections.Generic;

namespace RLS.Domain.Robots
{
    public class RobotFilterParams : BaseFilterParams<Robot>
    {
        public string UserId { get; set; }

        public IEnumerable<int> ModelIds { get; set; }

        public IEnumerable<int> TypeIds { get; set; }

        public string Term { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}