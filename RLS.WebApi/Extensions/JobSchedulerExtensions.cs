using Hangfire;
using Microsoft.AspNetCore.Builder;
using System;

namespace RLS.WebApi.Extensions
{
    public static class JobSchedulerExtensions
    {
        public static IApplicationBuilder ScheduleJobs(
            this IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IBackgroundJobClient backgroundJobClient)
        {
            //backgroundJobClient.Schedule(() => );
            return app;
        }
    }
}