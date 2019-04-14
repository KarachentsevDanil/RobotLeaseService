using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.BLL.Services.Contracts.Robots;
using RLS.BLL.Services.Contracts.Users;
using RLS.BLL.Services.Rentals;
using RLS.BLL.Services.Robots;
using RLS.BLL.Services.Users;
using RLS.DAL.EF.Repositories.Rentals;
using RLS.DAL.EF.Repositories.Robots;
using RLS.DAL.EF.Repositories.Users;
using RLS.DAL.EF.UnitOfWork;
using RLS.DAL.Repositories.Contracts.Rentals;
using RLS.DAL.Repositories.Contracts.Robots;
using RLS.DAL.Repositories.Contracts.Users;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.WebApi.Configurations;

namespace RLS.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRentalUnitOfWork, RentalUnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRentalRepository, RentalRepository>();

            services.AddScoped<IRobotRepository, RobotRepository>();
            services.AddScoped<IRobotCompanyRepository, RobotCompanyRepository>();
            services.AddScoped<IRobotTypeRepository, RobotTypeRepository>();
            services.AddScoped<IRobotModelRepository, RobotModelRepository>();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRentalService, RentalService>();

            services.AddScoped<IRobotService, RobotService>();
            services.AddScoped<IRobotCompanyService, RobotCompanyService>();
            services.AddScoped<IRobotTypeService, RobotTypeService>();
            services.AddScoped<IRobotModelService, RobotModelService>();

            return services;
        }

        public static DatabaseConfiguration AddDatabaseConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbConfig = new DatabaseConfiguration();
            configuration.Bind("DatabaseConfiguration", dbConfig);
            services.AddSingleton(dbConfig);

            return dbConfig;
        }

        public static IServiceCollection ConfigureDatabase<TContext>(this IServiceCollection services, DatabaseConfiguration csConfig)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options => options.UseSqlServer(csConfig.ConnectionString));
            return services;
        }

        public static IApplicationBuilder InitializeMigration<TContext>(this IApplicationBuilder app)
            where TContext : DbContext
        {
            using (IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (TContext context = serviceScope.ServiceProvider.GetService<TContext>())
                {
                    context.Database.EnsureCreated();
                }
            }

            return app;
        }
    }
}