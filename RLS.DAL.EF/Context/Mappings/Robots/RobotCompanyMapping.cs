using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Shared.Context.Mappings.Contract;
using RLS.Domain.Robots;

namespace RLS.DAL.EF.Context.Mappings.Robots
{
    public class RobotCompanyMapping : IMappingContract<RobotCompany>
    {
        public void MapEntity(EntityTypeBuilder<RobotCompany> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired();

            builder.ToTable("Companies", "robot");
        }
    }
}