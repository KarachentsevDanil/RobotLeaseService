﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RLS.DAL.EF.Context.Mappings.Contract;
using RLS.Domain.Robots;

namespace RLS.DAL.EF.Context.Mappings.Robots
{
    public class RobotMapping : IMappingContract<Robot>
    {
        public void MapEntity(EntityTypeBuilder<Robot> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.DailyCosts).IsRequired();

            builder.Property(p => p.UserId).HasMaxLength(50).IsRequired();

            builder.HasOne(p => p.User)
                .WithMany(p => p.Robots)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Model)
                .WithMany(p => p.Robots)
                .HasForeignKey(p => p.ModelId);

            builder.ToTable("Robots", "robot");
        }
    }
}