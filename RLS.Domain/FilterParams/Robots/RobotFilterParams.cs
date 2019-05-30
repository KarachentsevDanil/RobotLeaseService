using System;
using System.Collections.Generic;
using RLS.Domain.Enums;
using RLS.Domain.Models;
using RLS.Domain.Robots;

namespace RLS.Domain.FilterParams.Robots
{
    public class RobotFilterParams : BaseFilterParams<Robot>
    {
        public string UserId { get; set; }

        public IEnumerable<int> ModelIds { get; set; }

        public IEnumerable<int> CompaniesIds { get; set; }

        public IEnumerable<int> TypeIds { get; set; }

        public string Term { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsSearchView { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public double MinRating { get; set; }

        public double MaxRating { get; set; }

        public RobotSortType SortBy { get; set; }

        public string UserInterests { get; set; }

        public bool OnlyFavorite { get; set; }
    }
}