﻿namespace RLS.WebApi.Models.Users
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Interests { get; set; }

        public string Phone { get; set; }
    }
}