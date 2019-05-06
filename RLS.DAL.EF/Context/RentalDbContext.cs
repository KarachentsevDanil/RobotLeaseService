using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RLS.DAL.EF.Context.Mappings.Rentals;
using RLS.DAL.EF.Context.Mappings.Robots;
using RLS.DAL.EF.Context.Mappings.Users;
using RLS.Domain.Rentals;
using RLS.Domain.Robots;
using RLS.Domain.Users;

namespace RLS.DAL.EF.Context
{
    public class RentalDbContext : IdentityDbContext<User>
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
        { }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<RentalMessage> Messages { get; set; }

        public DbSet<Robot> Robots { get; set; }

        public DbSet<RobotType> RobotTypes { get; set; }

        public DbSet<RobotModel> RobotModels { get; set; }

        public DbSet<RobotCompany> RobotCompanies { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new RentalMapping().MapEntity(builder.Entity<Rental>());
            new RentalMessageMapping().MapEntity(builder.Entity<RentalMessage>());

            new RobotCompanyMapping().MapEntity(builder.Entity<RobotCompany>());
            new RobotModelMapping().MapEntity(builder.Entity<RobotModel>());
            new RobotTypeMapping().MapEntity(builder.Entity<RobotType>());
            new RobotMapping().MapEntity(builder.Entity<Robot>());

            new UserMapping().MapEntity(builder.Entity<User>());
        }
    }
}