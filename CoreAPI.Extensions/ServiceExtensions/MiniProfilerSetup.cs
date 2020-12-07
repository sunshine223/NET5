using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreAPI.Extensions.ServiceExtensions
{
    /// <summary>
    /// MiniProfiler 启动服务
    /// </summary>
    public static class MiniProfilerSetup
    {
        public static void AddMiniProfilerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMiniProfiler();
            // 3.x使用MiniProfiler，必须要注册MemoryCache服务
           // services.AddMiniProfiler(options =>
           // {
           //     options.RouteBasePath = "/profiler";//注意这个路径要和下边 index.html 脚本配置中的一致，
           //     options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
           //     options.PopupShowTimeWithChildren = true;

           // }
           //);
        }
    }
}
