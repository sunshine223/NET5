using CoreAPI.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CoreAPI.Extensions.ServiceExtensions
{  /// <summary>
   /// WebApiClientSetup 启动服务
   /// </summary>
    public static class WebApiClientSetup
    {
        /// <summary>
        /// 注册WebApiClient接口
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpApi(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //services.AddHttpApi<UserInfoserve>().ConfigureHttpApiConfig(c =>
            //{
            //    c.HttpHost = new Uri("http://apk.neters.club/");
            //    c.FormatOptions.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            //});
          
        }
    }
}
