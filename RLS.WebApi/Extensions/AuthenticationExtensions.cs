using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RLS.DAL.EF.Context;
using RLS.Domain.Users;
using RLS.WebApi.Configurations;
using System.Text;

namespace RLS.WebApi.Extensions
{
    public static class AuthenticationExtensions
    {
        public static AuthenticationConfiguration AddAuthenticationConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var config = new AuthenticationConfiguration();

            configuration.Bind("AuthenticationConfiguration", config);
            services.AddSingleton(config);

            return config;
        }

        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<RentalDbContext>()
                .AddDefaultTokenProviders();

            services.AddJwtAuthentication(configuration);

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        IssuerSigningKey = configuration.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            return services;
        }

        public static SecurityKey GetSymmetricSecurityKey(this AuthenticationConfiguration configuration)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.TokenKey));
        }
    }
}