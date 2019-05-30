using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Users;

namespace RLS.DAL.EF.Context.Mappings.Users
{
    public class UserInterestsSearchMapping : IMappingContract<UserInterestsSearch>
    {
        public void MapEntity(EntityTypeBuilder<UserInterestsSearch> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.UserId).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Interests).HasMaxLength(500);

            builder.HasOne(t => t.User).WithMany(t => t.Searches).HasForeignKey(t => t.UserId);

            builder.ToTable("UserInterestsSearches", "user");
        }
    }
}