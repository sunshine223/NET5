﻿using CoreAPI.Common.Appseting;
using CoreAPI.Common.Helper;
using CoreAPI.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;

namespace CoreAPI.Extensions.ServiceExtensions
{
    /// <summary>
    /// 项目启动服务
    /// </summary>
    public static class AppConfigSetup
    {
        public static void AddAppConfigSetup(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (Appsettings.app(new string[] { "Startup", "AppConfigAlert", "Enabled" }).ObjToBool())
            {
                if (env.IsDevelopment())
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Console.OutputEncoding = Encoding.GetEncoding("GB2312");
                }
                if (services == null) throw new ArgumentNullException(nameof(services));
           
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Console.OutputEncoding = Encoding.GetEncoding("GB2312");
                Console.WriteLine("************CoreAPI Config Set *****************");

                ConsoleHelper.WriteSuccessLine("Current environment: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                // 授权策略方案
                if (Permissions.IsUseIds4)
                {
                    ConsoleHelper.WriteSuccessLine($"Current authorization scheme: " + (Permissions.IsUseIds4 ? "Ids4" : "JWT"));
                }
                else
                {
                    Console.WriteLine($"Current authorization scheme: " + (Permissions.IsUseIds4 ? "Ids4" : "JWT"));
                }

                // Redis缓存AOP
                if (!Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ObjToBool())
                {
                    Console.WriteLine($"Redis Caching AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Redis Caching AOP: True");
                }

                // 内存缓存AOP
                if (!Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
                {
                    Console.WriteLine($"Memory Caching AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Memory Caching AOP: True");
                }

                // 服务日志AOP
                if (!Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
                {
                    Console.WriteLine($"Service Log AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Service Log AOP: True");
                }
                // 事务AOP
                if (!Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ObjToBool())
                {
                    Console.WriteLine($"Transaction AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Transaction AOP: True");
                }

                // 数据库Sql执行AOP
                if (!Appsettings.app(new string[] { "AppSettings", "SqlAOP", "Enabled" }).ObjToBool())
                {
                    Console.WriteLine($"DB Sql AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"DB Sql AOP: True");
                }

                // SingnalR发送数据
                if (!Appsettings.app(new string[] { "Middleware", "SignalR", "Enabled" }).ObjToBool())
                {
                    Console.WriteLine($"SignalR send data: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"SignalR send data: True");
                }

                // IP限流
                if (!Appsettings.app("Middleware", "IpRateLimit", "Enabled").ObjToBool())
                {
                    Console.WriteLine($"IpRateLimiting: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"IpRateLimiting: True");
                }

                // 多库
                if (!Appsettings.app(new string[] { "MutiDBEnabled" }).ObjToBool())
                {
                    Console.WriteLine($"Is multi-DataBase: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Is multi-DataBase: True");
                }

                // 读写分离
                if (!Appsettings.app(new string[] { "CQRSEnabled" }).ObjToBool())
                {
                    Console.WriteLine($"Is CQRS: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Is CQRS: True");
                }

                Console.WriteLine();
            }
        }
    }
}
