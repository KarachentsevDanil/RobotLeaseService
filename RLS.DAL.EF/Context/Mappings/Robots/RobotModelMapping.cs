using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Robots;

namespace RLS.DAL.EF.Context.Mappings.Robots
{
    public class RobotModelMapping : IMappingContract<RobotModel>
    {
        public void MapEntity(EntityTypeBuilder<RobotModel> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany(p => p.Models)
                .HasForeignKey(p => p.CompanyId);

            builder.HasOne(p => p.Type)
                .WithMany(p => p.Models)
                .HasForeignKey(p => p.TypeId);

            builder.ToTable("Models", "robot");
        }
    }
}