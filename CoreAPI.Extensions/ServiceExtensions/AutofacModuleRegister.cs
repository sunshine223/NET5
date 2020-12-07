
using Autofac;
using Autofac.Extras.DynamicProxy;
using CoreAPI.Common.Appseting;
using CoreAPI.Extensions.AOP;
using CoreAPI.Repository.BASE;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CoreAPI.Extensions.ServiceExtensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AutofacModuleRegister));
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            // AOP 开关，如果想要打开指定的功能，只需要在 appsettigns.json 对应对应 true 就行。                                    
            var cacheType = new List<Type>();
            if (Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<CoreAPIRedisCacheAOP>();
                cacheType.Add(typeof(CoreAPIRedisCacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<CoreAPICacheAOP>();
                cacheType.Add(typeof(CoreAPICacheAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<CoreAPITranAOP>();
                cacheType.Add(typeof(CoreAPITranAOP));
            }
            if (Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
            {
                builder.RegisterType<CoreAPILogAOP>();
                cacheType.Add(typeof(CoreAPILogAOP));
            }


            //要记得!!!这个注入的是实现类层，不是接口层 IServices 
            var servicesDllFile = Path.Combine(basePath, "CoreAPI.Service.dll");
            var repositoryDllFile = Path.Combine(basePath, "CoreAPI.Repository.dll");

            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                var msg = "Repository.dll和service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                log.Error(msg);
                throw new Exception(msg);
            }
           builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储
            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                   .AsImplementedInterfaces()
                   .InstancePerDependency()
                   .EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;对目标类型启用接口拦截。
                   .InterceptedBy(cacheType.ToArray());//允许将拦截器服务的列表分配给注册。

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                   .AsImplementedInterfaces()
                   .InstancePerDependency();
        }
    }
}
