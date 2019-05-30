using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Rentals;

namespace RLS.DAL.EF.Context.Mappings.Rentals
{
    public class RentalMapping : IMappingContract<Rental>
    {
        public void MapEntity(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId).IsRequired().HasMaxLength(50);

            builder.Property(p => p.CustomerFeedback).HasMaxLength(500);

            builder.Property(p => p.OwnerFeedback).HasMaxLength(500);

            builder.Property(p => p.CancelReason).HasMaxLength(500);

            builder.Property(p => p.CreditCardNumber).HasMaxLength(30);

            builder.Property(p => p.CreditCardCvvCode).HasMaxLength(4);

            builder.Property(p => p.CreditCardExpireDate).HasMaxLength(50);

            builder.Property(p => p.CreditCardOwnerName).HasMaxLength(250);

            builder.Property(p => p.StartDate).IsRequired();

            builder.Property(p => p.EndDate).IsRequired();

            builder.Property(p => p.Status).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.Rentals)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Robot)
                .WithMany(p => p.Rentals)
                .HasForeignKey(p => p.RobotId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Rentals", "rental");
        }
    }
}