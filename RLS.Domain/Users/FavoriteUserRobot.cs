using RLS.Domain.Robots;

namespace RLS.Domain.Users
{
    public class FavoriteUserRobot
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int RobotId { get; set; }

        public Robot Robot { get; set; }

        public User User { get; set; }
    }
}