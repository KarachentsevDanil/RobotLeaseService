using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Rentals;

namespace RLS.DAL.EF.Context.Mappings.Rentals
{
    public class RentalRobotMapping : IMappingContract<RentalRobot>
    {
        public void MapEntity(EntityTypeBuilder<RentalRobot> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasOne(p => p.Robot)
                .WithMany(p => p.Rentals)
                .HasForeignKey(p => p.RobotId);

            builder.HasOne(p => p.Rental)
                .WithMany(p => p.Robots)
                .HasForeignKey(p => p.RentalId);

            builder.ToTable("RentalRobots", "rental");
        }
    }
}