using CoreAPI.Common;
using CoreAPI.Common.Helper;
using CoreAPI.DTO;
using CoreAPI.Filter;
using CoreAPI.IService;
using CoreAPI.Model.BASE;
using CoreAPI.Model.Models;
using CorePAI.Authorizations.OverWrite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CoreAPI.ServiceExtensions.CustomApiVersion;

namespace CoreAPIController.Controllers.v2
{
    /// <summary>
    /// 用户登录
    /// </summary>
    [CustomRoute(ApiVersions.V2)]
    //[Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserInfoIserve _userInfo;
        private readonly IRedisBasketRepository _cache;

        /************************************************/
        // 如果需要使用Http协议带名称的，比如这种 [HttpGet("apbs")]
        // 目前只能把[CustomRoute(ApiVersions.v2)] 提到 controller 的上边，做controller的特性
        // 并且去掉//[Route("api/[controller]")]路由特性，否则会有两个接口
        /************************************************/


        public LoginController(IUserInfoIserve userInfo, IRedisBasketRepository cache)
        {
            _userInfo = userInfo;
            _cache = cache;
        }
        /// <summary>
        /// 登录获取token方案1 1000s过期
        /// </summary>
        /// <param name="UserLog"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        [HttpPut("GetToken")]
        public async Task<MessageModel<string>> GetToken(string UserLog, string UserPwd)
        {
            bool suc = false;
            TimeSpan time = DateTime.Now.AddDays(60)-DateTime.Now;
            var user = await _userInfo.GetLists(UserLog, MD5Helper.MD5Encrypt32(UserPwd));
            string jwtStr;
            List<object> list = new();
            if (user!=null)
            {
                TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = "Admin" };
                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
                MessageModel<string> s = new();
                s.msg = user.name;
                s.response = jwtStr;
                list.Add(s);
               _=_cache.Set("Token", list, time);
            }
            else
            {
                jwtStr = "请输入正确的用户名或者密码！";
            }
            return new MessageModel<string>()
            {
                success = suc,
                msg = suc ? "获取成功" : "获取失败",
                response = jwtStr
            };
        }
        /// <summary>
        /// 清除用户登录信息
        /// </summary>
        /// <returns></returns>
       [HttpDelete("Delete")] 
       public async Task<MessageModel<string>> Delete(int uID)
        {
            var data = new MessageModel<string>();
            if (await _userInfo.Updatle(uID)==true)
            {
                data.success = true;
                data.msg = "修改成功";
            }
            return data;
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Authorize]//无策略授权 
        //[Authorize(Permissions.Name)]基于策略授权
        [HttpPost("Add")]
        public async Task<MessageModel<string>> Add(UserInfoDTO p)
        {
            var data = new MessageModel<string>();
            var list = await _userInfo.Adds(p);
            data.success = list.uLoginName !=null; 
            if (data.success)
            {
                data.success=true;
                data.msg = "添加成功";
            } 
            return data;
        }
        /// <summary>
        /// 测试 MD5 加密字符串
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Md5Password")]
        public string Md5Password(string password)
        {
            return MD5Helper.MD5Encrypt32(password);
        }
    }
}
