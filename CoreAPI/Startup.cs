
using Autofac;
using CoreAPI.Common;
using CoreAPI.Common.Appseting;
using CoreAPI.Common.Helper;
using CoreAPI.Core;
using CoreAPI.Extensions;
using CoreAPI.Extensions.Extensions;
using CoreAPI.Extensions.Middlewares;
using CoreAPI.Extensions.ServiceExtensions;
using CoreAPI.Filter;
using CoreAPI.IService;
using CoreAPI.Model.Seed;
using CoreAPI.ServiceExtensions;
using CoreAPI.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace CoreAPI
{
    public class Startup
    {
        private IServiceCollection _services;
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new Appsettings(Configuration));
            services.AddSingleton(new LogLock(Env.ContentRootPath));
            Permissions.IsUseIds4 = Appsettings.app(new string[] { "Startup", "IdentityServer4", "Enabled" }).ObjToBool();
            services.AddMemoryCacheSetup();
            services.AddRedisCacheSetup();

            services.AddSqlsugarSetup();
            services.AddDbSetup();
            services.AddAutoMapperSetup();
            services.AddCorsSetup();
            services.AddMiniProfilerSetup();
            services.AddSwaggerSetup();
            services.AddJobSetup();
            services.AddHttpContextSetup();
            services.AddAppConfigSetup(Env);
            services.AddHttpApi();
            services.AddRedisInitMqSetup();
            // 授权+认证 (jwt or ids4)
            services.AddAuthorizationSetup();
            if (Permissions.IsUseIds4)
            {
                services.AddAuthentication_Ids4Setup();
            }
            else
            {
                services.AddAuthentication_JWTSetup();
            }
            services.AddIpPolicyRateLimitSetup(Configuration);//对接口进行保护，IP限流
            services.AddSignalR().AddNewtonsoftJsonProtocol();

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                   .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            services.AddControllers(o =>
            {
                // 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                // 全局路由前缀，统一修改路由
                // 全局路由权限公约
                //o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                // 全局路由前缀，统一修改路由
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
             //全局配置Json序列化处理
            .AddNewtonsoftJson(options =>
             {
                 //忽略循环引用
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 //不使用驼峰样式的key
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 //设置时间格式
                 //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                 //忽略Model中为null的属性
                 //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
             });
            _services = services;
        }
        /// <summary>
        ///  注意在CreateDefaultBuilder中，添加Autofac服务工厂
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="myContext"></param>
        /// <param name="tasksQzServices"></param>
        /// <param name="schedulerCenter"></param>
        /// <param name="lifetime"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyContext myContext, ITasksQzServices tasksQzServices, ISchedulerCenter schedulerCenter, IHostApplicationLifetime lifetime)
        {
            // Ip限流,尽量放管道外层
            app.UseIpLimitMildd();
            // 记录请求与返回数据 
            app.UseReuestResponseLog();
            // signalr 
            app.UseSignalRSendMildd();
            // 记录ip请求
            app.UseIPLogMildd();
            // 查看注入的所有服务
            app.UseAllServicesMildd(_services);
            if (env.IsDevelopment())
            {
                // 在开发环境中，使用异常页面，这样可以暴露错误堆栈信息，所以不要放在生产环境。
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();
            }
            // 封装Swagger展示
            app.UseSwaggerMildd(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("CoreAPI.index.html"));
            // ↓↓↓↓↓↓ 注意下边这些中间件的顺序，很重要 ↓↓↓↓↓↓
            // CORS跨域
            app.UseCors(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }));
            // 使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            app.UseRouting();
            // 这种自定义授权中间件，可以尝试，但不推荐
            //app.UseJwtTokenAuth();
            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization();
            // 开启异常中间件，要放到最后
            //app.UseExceptionHandlerMidd();
            // 性能分析
            app.UseMiniProfiler();
            // 用户访问记录
            app.UseRecordAccessLogsMildd();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/api2/chatHub");
            });
            // 初始化生成表
            app.UseSeedDataMildd(myContext);
            // 开启QuartzNetJob调度服务
            app.UseQuartzJobMildd(tasksQzServices, schedulerCenter);
            //服务注册
            app.UseConsulMildd(Configuration, lifetime);
        }
    }
}
