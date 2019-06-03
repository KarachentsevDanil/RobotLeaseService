using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RLS.BLL.Services.Contracts.Rentals;
using System;
using System.Threading;

namespace RLS.WebApi.Extensions
{
    public static class JobSchedulerExtensions
    {
        public static IApplicationBuilder ScheduleJobs(
            this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IBackgroundJobClient backgroundJobClient)
        {
            var rentalService = serviceProvider.GetService<IRentalService>();

            backgroundJobClient.Schedule(
                () => rentalService.SendRentalNotificationAsync(CancellationToken.None),
                TimeSpan.FromHours(12));

            return app;
        }
    }
}