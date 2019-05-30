using Microsoft.AspNetCore.Identity;
using RLS.Domain.Enums;
using RLS.Domain.Rentals;
using RLS.Domain.Robots;
using System.Collections.Generic;

namespace RLS.Domain.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public string Interests { get; set; }

        public ICollection<Robot> Robots { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public ICollection<RentalMessage> Messages { get; set; }

        public ICollection<UserInterestsSearch> Searches { get; set; }

        public ICollection<FavoriteUserRobot> FavoriteRobots { get; set; }

        public User()
        {
            Robots = new List<Robot>();
            Rentals = new List<Rental>();
            Messages = new List<RentalMessage>();
        }
    }
}