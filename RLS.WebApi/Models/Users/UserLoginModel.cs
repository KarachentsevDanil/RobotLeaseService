﻿namespace RLS.WebApi.Models.Users
{
    public class UserLoginModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}