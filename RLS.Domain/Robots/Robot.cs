using RLS.Domain.Rentals;
using RLS.Domain.Users;
using System.Collections.Generic;

namespace RLS.Domain.Robots
{
    public class Robot
    {
        public int Id { get; set; }

        public double DailyCosts { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int ModelId { get; set; }

        public RobotModel Model { get; set; }

        public ICollection<RentalRobot> Rentals { get; set; }

        public Robot()
        {
            Rentals = new List<RentalRobot>();
        }
    }
}