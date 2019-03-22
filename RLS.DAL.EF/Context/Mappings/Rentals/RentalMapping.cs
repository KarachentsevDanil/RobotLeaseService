using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Shared.Context.Mappings.Contract;
using RLS.Domain.Rentals;

namespace RLS.DAL.EF.Context.Mappings.Rentals
{
    public class RentalMapping : IMappingContract<Rental>
    {
        public void MapEntity(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.StartDate).IsRequired();

            builder.Property(p => p.EndDate).IsRequired();

            builder.Property(p => p.Status).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.Rentals)
                .HasForeignKey(p => p.UserId);

            builder.ToTable("Rentals", "rental");
        }
    }
}