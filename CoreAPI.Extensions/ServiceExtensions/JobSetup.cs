using CoreAPI.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;


namespace CoreAPI.Extensions.ServiceExtensions
{
    public static class JobSetup
    {
        public static void AddJobSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(nameof(services));
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddTransient<Job_Synchronous>();//Job使用瞬时依赖注入
            services.AddSingleton<ISchedulerCenter, SchedulerCenterServer>();
        }
        
    }
}
