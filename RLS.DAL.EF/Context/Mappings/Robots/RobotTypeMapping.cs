using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Shared.Context.Mappings.Contract;
using RLS.Domain.Robots;

namespace RLS.DAL.EF.Context.Mappings.Robots
{
    public class RobotTypeMapping : IMappingContract<RobotType>
    {
        public void MapEntity(EntityTypeBuilder<RobotType> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired();

            builder.ToTable("Types", "robot");
        }
    }
}