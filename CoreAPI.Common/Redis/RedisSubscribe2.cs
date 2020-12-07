using CoreAPI.Core;
using InitQ.Abstractions;
using InitQ.Attributes;
using System;
using System.Threading.Tasks;

namespace CoreAPI.Common.Redis
{
    public class RedisSubscribe2 : IRedisSubscribe
    {
        [Subscribe(RedisMqKey.Loging)]
        private async Task SubRedisLoging(string msg)
        {
            Console.WriteLine($"订阅者 2 从 队列{RedisMqKey.Loging} 消费到/接受到 消息:{msg}");
            await Task.CompletedTask;
        }
    }
}
