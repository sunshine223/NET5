using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreAPI.Common.Appseting;
using CoreAPI.Common.Helper;
using CoreAPI.Core;
using CoreAPI.Extensions.Authorizations.Policys;
using CoreAPI.Filter;
using CoreAPI.IService;
using CoreAPI.IServices;
using CoreAPI.Model.BASE;
using CorePAI.Authorizations.OverWrite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using static CoreAPI.ServiceExtensions.CustomApiVersion;

namespace CoreAPI.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class LoginController : ControllerBase
    {
        /************************************************/
        // 如果不需要使用Http协议带名称的，比如这种 [HttpGet]
        // 就可以按照下边的写法去写，在方法上直接加特性 [CustomRoute(ApiVersions.v1, "apbs")]
        // 反之，如果你需要http协议带名称，请看 V2 文件夹的方法
        /************************************************/
       
        private readonly IUserInfoIserve _userInfo;

        private readonly PermissionRequirement _requirement;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;
        public LoginController(IUserInfoIserve userInfo, PermissionRequirement requirement, IRoleModulePermissionServices roleModulePermissionServices)
        {
            _userInfo = userInfo;
            _requirement = requirement;
            _roleModulePermissionServices = roleModulePermissionServices;
        }
        /// <summary>
        /// 登录获取token
        /// </summary>
        /// <param name="UserLog"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        [CustomRoute(ApiVersions.V1, "GetToken")]
        [HttpPut]
        public async Task<MessageModel<string>> GetToken(string UserLog, string UserPwd)
        {
            bool suc = false;
            var user = await _userInfo.GetLists(UserLog, UserPwd);
            string jwtStr;
            if (user != null)
            {
                TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = "Admin" };
                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
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
        /// 登录获取token方案2 30s过期
        /// </summary>
        /// <param name="UserLog"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        [CustomRoute(ApiVersions.V1, "GetJwtToken3")]
        [HttpPost]
        public async Task<MessageModel<TokenInfoViewModel>> GetJwtToken3(string UserLog, string UserPwd)
        {
            string jwtStr = string.Empty;
            if (string.IsNullOrEmpty(UserLog) || string.IsNullOrEmpty(UserPwd))
            {
                return new MessageModel<TokenInfoViewModel>()
                {
                    success = false,
                    msg = "用户名或密码不能为空",
                };
            }

            UserPwd = MD5Helper.MD5Encrypt32(UserPwd);
            var user = await _userInfo.GetList(UserLog, UserPwd);
            if (user != null)
            {
                var userRoles = await _userInfo.GetUserRoleNameStr(UserLog, UserPwd);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, UserLog),
                    new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().uID.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
                // ids4和jwt切换
                var idetity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                idetity.AddClaims(claims);
                // jwt
                if (!Permissions.IsUseIds4)
                {
                    var data = await _roleModulePermissionServices.GetRoleModule(user.FirstOrDefault().uID.ObjToInt());
                    var list = (from item in data
                                where item.IsDeleted == false
                                orderby item.Id
                                select new PermissionItem
                                {
                                    Url = item.Module?.LinkUrl,
                                    Role = item.Role?.Name.ObjToString(),
                                }).ToList();

                    _requirement.Permissions = list;
                }
                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                return new MessageModel<TokenInfoViewModel>()
                {
                    success = true,
                    msg = "获取成功",
                    response = token
                };
            }
            else
            {
                return new MessageModel<TokenInfoViewModel>()
                {
                    success = false,
                    msg = "认证失败",
                };
            }
        }


    }
}
