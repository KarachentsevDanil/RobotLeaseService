using RLS.Domain.Robots;

namespace RLS.Domain.Rentals
{
    public class RentalRobot
    {
        public int Id { get; set; }

        public int RentalId { get; set; }

        public Rental Rental { get; set; }

        public int RobotId { get; set; }

        public Robot Robot { get; set; }
    }
}