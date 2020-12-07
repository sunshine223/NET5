using Consul;
using CoreAPI.Common.Appseting;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using static CoreAPI.ServiceExtensions.CustomApiVersion;

namespace CoreAPI.ServiceExtensions
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {

        private static readonly ILog log =
        LogManager.GetLogger(typeof(SwaggerSetup));

        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var ApiName = Appsettings.app(new string[] { "Startup", "ApiName" });

            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{ApiName} 接口文档——{RuntimeInformation.FrameworkDescription}",
                        Description = $"{ApiName} HTTP API " + version,
                        Contact = new OpenApiContact { Name = ApiName, Email = "CoreAPI@xxx.com", Url = new Uri("https://www.bing.com/") },
                        License = new OpenApiLicense { Name = ApiName + " 官方文档", Url = new Uri("http://apk.neters.club/.doc/") }
                    });
                    c.OrderActionsBy(o => o.RelativePath);
                });
                try
                {
                    // 使用反射获取xml文件。并构造出文件的路径
                    var xmlAPIlPath = Path.Combine(basePath, "CoreAPI.xml");//这个就是api层的xml文件名
                    //默认的第二个参数是false，这个是controller的注释，记得修改
                    c.IncludeXmlComments(xmlAPIlPath, true);
                    //这个就是Model层的xml文件名
                    var xmlModelPath = Path.Combine(basePath, "CoreAPI.Model.xml");
                    c.IncludeXmlComments(xmlModelPath);
                    //这个就是DTO层的xml文件名
                    var xmlModelPath1 = Path.Combine(basePath, "CoreAPI.DTO.xml");
                    c.IncludeXmlComments(xmlModelPath1);

                }
                catch (Exception ex)
                {
                    log.Error("Blog.Core.xml和Blog.Core.Model.xml 丢失，请检查并拷贝。\n" + ex.Message);
                }
                //开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                //必须是Oauth2  Token绑定到ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }

    /// <summary>
    /// 自定义版本
    /// </summary>
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,
            /// <summary>
            /// V2 版本
            /// </summary>
            V2 = 2,
        }
    }

}
