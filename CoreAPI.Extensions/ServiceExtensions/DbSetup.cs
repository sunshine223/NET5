using CoreAPI.Model.Seed;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreAPI.ServiceExtensions
{
    /// <summary>
    /// Db 启动服务
    /// </summary>
    public static class DbSetup
    {
        public static void AddDbSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //Sqlsugar尽量使用单列注入，否则会出现bug
            _ = services.AddScoped<DBSeed>();
            services.AddScoped<MyContext>();
        }
    }
}
