using CoreAPI.Common;
using CoreAPI.Common.Appseting;
using CoreAPI.IService;
using CoreAPI.Model.Models;
using CoreAPI.Repository.BASE;
using CoreAPI.Repository.UnitOfWork;
using CoreAPI.Service.BASE;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Service
{
    public class UserInfoserve : BaseServices<sysUserInfo>, IUserInfoIserve 
    {
        private readonly IUnitOfWork Db;
        private readonly IUserRoleServices _userRoleServices;
        readonly IBaseRepository<Role> _roleRepository;
        public UserInfoserve(IUnitOfWork unitOfWork, IBaseRepository<sysUserInfo> dal, IUserRoleServices userRoleServices, IBaseRepository<Role> roleRepository)
        {
            this.Db = unitOfWork;
            this.BaseDal = dal;
            this._userRoleServices = userRoleServices;
            this._roleRepository = roleRepository;
        }
        
        public async Task<sysUserInfo> GetLists(string UserLog, string UserPwd)
        {
            sysUserInfo model = null;
            var list = await base.Query(a => a.uLoginName == UserLog && a.uLoginPWD == UserPwd&&a.tdIsDelete==false);
     
            if (list.Count > 0)
            {
                model = list.FirstOrDefault();
                var uselist = await Db.GetDbClient().Updateable<sysUserInfo>(it=>it.uStatus==1).Where(a => a.uID == model.uID).ExecuteCommandAsync();
            }
            return model;
        }
        public async Task<bool> Updatle(int uID)
        {
            bool c = false;
            var res=await Db.GetDbClient().Updateable<sysUserInfo>(it => it.uStatus == 0).Where(a => a.uID ==uID).ExecuteCommandAsync();
            if (res > 0)
            {
                c = true;
            }
            return c;
        }
       /// <summary>
       /// 测试缓存
       /// </summary>
       /// <param name="UserLog"></param>
       /// <param name="UserPwd"></param>
       /// <returns></returns>
        [Caching(AbsoluteExpiration = 30)]//缓存30分钟
        public async Task<List<sysUserInfo>> GetList(string UserLog, string UserPwd)
        {
            return await base.Query();
        }
        /// <summary>
        /// 测试添加
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public async Task<sysUserInfo> Adds(sysUserInfo p)
        {
            var list = await base.Add(p);
            sysUserInfo model = new sysUserInfo();
            if (list>0)
            {
               model= await base.QueryById(list);
            }
        return model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>

        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user=(await base.Query(a => a.uLoginName == loginName && a.uLoginPWD == loginPwd)).FirstOrDefault();
            var roleList = await _roleRepository.Query(a => a.IsDeleted == false);
            if (user != null)
            {
                var userRoles = await Db.GetDbClient().Queryable<UserRole>().Where(u=>u.Id==user.uID).ToListAsync();
                if (userRoles.Count > 0)
                {
                    var arr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(d => arr.Contains(d.Id.ObjToString()));

                    roleName = string.Join(',', roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }
    }
}
