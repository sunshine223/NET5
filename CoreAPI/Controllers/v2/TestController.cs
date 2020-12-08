using CoreAPI.Common;
using CoreAPI.Core;
using CoreAPI.DTO;
using CoreAPI.Filter;
using CoreAPI.IService;
using CoreAPI.Model.BASE;
using CoreAPI.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreAPI.ServiceExtensions.CustomApiVersion;

namespace CoreAPI.Controllers.v2
{
    /// <summary>
    /// 测试
    /// </summary>
    [CustomRoute(ApiVersions.V2)]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITasksQzServices tasksQzServices;
        private readonly IRedisBasketRepository _cache;
        private readonly IApplySinginService _apply;

        public TestController(ITasksQzServices _tasksQzServices, IRedisBasketRepository cache, IApplySinginService apply)
        {
            tasksQzServices = _tasksQzServices;
            _cache = cache;
            _apply = apply;
        }
        /// <summary>
        /// 测试Automapper
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost("Automapper")]
        [AllowAnonymous]
        public async Task<object> Automapper(UserInfoDTO p)
        {
            var list =await _apply.AddSign(p);
            bool c = false;
            if (list !=false)
            {
                c = true;
            }
            return c;
        }
        /// <summary>
        /// 测试Redis消息队列
        /// </summary>
        /// <param name="_redisBasketRepository"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task RedisMq([FromServices] IRedisBasketRepository _redisBasketRepository)
        {
            var msg = $"这里是一条日志{DateTime.Now}";
            await _redisBasketRepository.ListLeftPushAsync(RedisMqKey.Loging, msg);
        }
        /// <summary>
        /// 测试Redis缓存
        /// </summary>
        /// <returns></returns>
        [HttpPost("RedisChche")]
        public async Task<object> RedisChche()
        {
            var data = new MessageModel<object>();
            var list = _cache.Exist("list");
            object c = new();
            if (list.Result== true)
            {
                c = _cache.GetValue("list").Result;
            }
            else
            {
                c = await tasksQzServices.Query();
                TimeSpan c1 = DateTime.Now.AddDays(60) - DateTime.Now;
                c = _cache.Set("list", c, c1);
            }
            return c;
        }
    }
}
