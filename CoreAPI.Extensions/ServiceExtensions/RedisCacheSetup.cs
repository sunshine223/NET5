﻿
using CoreAPI.Common;
using CoreAPI.Common.Appseting;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace CoreAPI.ServiceExtensions
{
    /// <summary>
    /// Redis缓存 启动服务
    /// </summary>
    public static class RedisCacheSetup
    {
        public static void AddRedisCacheSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services)); 

            services.AddTransient<IRedisBasketRepository, RedisBasketRepository>();

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                //获取连接字符串
                string redisConfiguration = Appsettings.app(new string[] { "Redis", "ConnectionString" });
                var configuration = ConfigurationOptions.Parse(redisConfiguration, true);

                configuration.ResolveDns = true;
                return ConnectionMultiplexer.Connect(configuration);
            });

        }
    }
}
