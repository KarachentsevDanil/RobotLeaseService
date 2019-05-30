using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Users;

namespace RLS.DAL.EF.Context.Mappings.Users
{
    public class FavoriteUserRobotMapping : IMappingContract<FavoriteUserRobot>
    {
        public void MapEntity(EntityTypeBuilder<FavoriteUserRobot> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.UserId).IsRequired().HasMaxLength(50);

            builder.HasOne(t => t.User)
                .WithMany(t => t.FavoriteRobots)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Robot)
                .WithMany(t => t.UserFavorites)
                .HasForeignKey(t => t.RobotId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("FavoriteUserRobots", "user");
        }
    }
}