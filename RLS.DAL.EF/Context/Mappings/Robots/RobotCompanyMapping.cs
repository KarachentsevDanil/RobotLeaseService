using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Robots;

namespace RLS.DAL.EF.Context.Mappings.Robots
{
    public class RobotCompanyMapping : IMappingContract<RobotCompany>
    {
        public void MapEntity(EntityTypeBuilder<RobotCompany> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();

            builder.Property(p => p.Country).HasMaxLength(250).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(500);

            builder.ToTable("Companies", "robot");
        }
    }
}