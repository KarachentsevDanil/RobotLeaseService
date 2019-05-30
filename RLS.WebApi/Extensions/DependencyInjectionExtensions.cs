using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RLS.BLL.Configurations;
using RLS.BLL.DTOs.Internal.Messages;
using RLS.BLL.Services.Contracts.Internal;
using RLS.BLL.Services.Contracts.Rentals;
using RLS.BLL.Services.Contracts.Robots;
using RLS.BLL.Services.Contracts.Users;
using RLS.BLL.Services.Internal.Messages;
using RLS.BLL.Services.Rentals;
using RLS.BLL.Services.Robots;
using RLS.BLL.Services.Users;
using RLS.DAL.EF.UnitOfWork;
using RLS.DAL.UnitOfWork.Contracts;
using RLS.WebApi.Configurations;
using SendGrid;

namespace RLS.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRentalUnitOfWork, RentalUnitOfWork>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IRentalMessageService, RentalMessageService>();

            services.AddScoped<IRobotService, RobotService>();
            services.AddScoped<IRobotCompanyService, RobotCompanyService>();
            services.AddScoped<IRobotTypeService, RobotTypeService>();
            services.AddScoped<IRobotModelService, RobotModelService>();
            services.AddScoped<IMessageSendService<EmailMessage>, EmailSendService>();
            
            return services;
        }

        public static DatabaseConfiguration AddDatabaseConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new DatabaseConfiguration();

            configuration.Bind("DatabaseConfiguration", config);
            services.AddSingleton(config);

            return config;
        }

        public static EmailSenderConfiguration AddEmailSenderConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new EmailSenderConfiguration();

            configuration.Bind("EmailSenderSettings", config);
            services.AddSingleton(config);

            return config;
        }

        public static void AddEmailSenderService(
            this IServiceCollection services,
            EmailSenderConfiguration configuration)
        {
            SendGridClient client = new SendGridClient(configuration.ApiKey);
            services.AddScoped<ISendGridClient, SendGridClient>(x => client);
        }

        public static IServiceCollection ConfigureDatabase<TContext>(
            this IServiceCollection services, DatabaseConfiguration csConfig)
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