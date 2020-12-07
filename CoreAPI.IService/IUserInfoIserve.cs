using CoreAPI.IService.BASE;
using CoreAPI.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI.IService
{
    public interface IUserInfoIserve: IBaseServices<sysUserInfo>
    {
        Task<sysUserInfo> GetLists(string UserLog, string UserPwd);
        Task<bool> Updatle(int uID);
        Task<List<sysUserInfo>> GetList(string UserLog, string UserPwd);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
        Task<sysUserInfo> Adds(sysUserInfo p);
    }
}
