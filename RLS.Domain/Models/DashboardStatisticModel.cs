namespace RLS.Domain.Models
{
    public class DashboardStatisticModel
    {
        public int UserCount { get; set; }

        public int UserWithRobotsCount { get; set; }

        public int UserWithRentsCount { get; set; }

        public int UserWithRobotsAndRentsCount { get; set; }

        public int RobotCount { get; set; }

        public int AvailableTodayRobotCount { get; set; }

        public int RentCount { get; set; }

        public int CompletedRentCount { get; set; }

        public int ActiveRentCount { get; set; }

        public int CanceledRentCount { get; set; }

        public int CustomerFeedbackCount { get; set; }

        public int OwnerFeedbackCount { get; set; }

        public int CompanyCount { get; set; }

        public int TypeCount { get; set; }

        public int ModelCount { get; set; }
    }
}