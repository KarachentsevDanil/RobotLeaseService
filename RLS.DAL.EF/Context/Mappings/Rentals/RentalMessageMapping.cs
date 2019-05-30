using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Rentals;

namespace RLS.DAL.EF.Context.Mappings.Rentals
{
    public class RentalMessageMapping : IMappingContract<RentalMessage>
    {
        public void MapEntity(EntityTypeBuilder<RentalMessage> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Message)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(p => p.CreatedAt).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.Messages)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Rental)
                .WithMany(p => p.Messages)
                .HasForeignKey(p => p.RentalId);

            builder.ToTable("RentalMessages", "rental");
        }
    }
}