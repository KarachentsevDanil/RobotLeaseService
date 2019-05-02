﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using RLS.BLL.Configurations.MapperProfiles;
using RLS.DAL.EF.Context;
using RLS.WebApi.Configurations.MapperProfiles;
using RLS.WebApi.Extensions;

namespace RLS.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver
                    = new DefaultContractResolver();
            });

            var authConfig = services.AddAuthenticationConfiguration(Configuration);

            services.AddIdentityAuthorization(authConfig);

            var dbConfig = services.AddDatabaseConfiguration(Configuration);

            services.ConfigureDatabase<RentalDbContext>(dbConfig);

            services.RegisterRepositories();

            services.RegisterServices();

            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<RobotLeaseApiMapperProfile>();
                c.AddProfile<RobotLeaseMapperProfile>();
            });

            services.AddSingleton(c => config.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.InitializeMigration<RentalDbContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}