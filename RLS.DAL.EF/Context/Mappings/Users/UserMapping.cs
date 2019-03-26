using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Users;

namespace RLS.DAL.EF.Context.Mappings.Users
{
    public class UserMapping : IMappingContract<User>
    {
        public void MapEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Role).IsRequired();

            builder.Property(p => p.FirstName).IsRequired();

            builder.Property(p => p.LastName).IsRequired();

            builder.ToTable("Users", "user");
        }
    }
}