using RLS.Domain.Enums;

namespace RLS.WebApi.Models.Users
{
    public class UserPrincipal
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Interests { get; set; }

        public Role Role { get; set; }
    }
}