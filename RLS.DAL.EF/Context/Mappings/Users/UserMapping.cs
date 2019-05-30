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
            builder.Property(p => p.Id).HasMaxLength(50);

            builder.Property(p => p.Role).IsRequired();

            builder.Property(p => p.FirstName).HasMaxLength(250).IsRequired();

            builder.Property(p => p.LastName).HasMaxLength(250).IsRequired();

            builder.Property(p => p.Interests).HasMaxLength(500);

            builder.ToTable("Users", "user");
        }
    }
}