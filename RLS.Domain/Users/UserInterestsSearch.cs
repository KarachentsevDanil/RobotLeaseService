using System;

namespace RLS.Domain.Users
{
    public class UserInterestsSearch
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Interests { get; set; }

        public bool IsFound { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}