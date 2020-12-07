﻿using CoreAPI.Common;
using CoreAPI.IRepository;
using CoreAPI.IServices;
using CoreAPI.Model.Models;
using CoreAPI.Repository.BASE;
using CoreAPI.Service.BASE;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Services
{
    /// <summary>
    /// RoleModulePermissionServices 应用服务 菜单
    /// </summary>	
    public class RoleModulePermissionServices : BaseServices<RoleModulePermission>, IRoleModulePermissionServices
    {
        readonly IRoleModulePermissionRepository _dal;
        readonly IBaseRepository<Modules> _moduleRepository;
        readonly IBaseRepository<Role> _roleRepository;

        // 将多个仓储接口注入
        public RoleModulePermissionServices(
            IRoleModulePermissionRepository dal, 
            IBaseRepository<Modules> moduleRepository, 
            IBaseRepository<Role> roleRepository)
        {
            this._dal = dal;
            this._moduleRepository = moduleRepository;
            this._roleRepository = roleRepository;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<RoleModulePermission>> GetRoleModule(int roleID)
        {
            var roleModulePermissions = await base.Query(a => a.IsDeleted == false&&a.RoleId==roleID);//菜单
            var roles = await _roleRepository.Query(a => a.IsDeleted == false&&a.ModifyId==roleID);//用户权限
            var modules = await _moduleRepository.Query(a => a.IsDeleted == false&&a.Id==roleID);//api接口
            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = roles.FirstOrDefault(d => d.Id == item.RoleId);
                    item.Module = modules.FirstOrDefault(d => d.Id == item.ModuleId);
                }
            }
            return roleModulePermissions;
        }

        public async Task<List<TestMuchTableResult>> QueryMuchTable()
        {
            return await _dal.QueryMuchTable();
        }

        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            return await _dal.RoleModuleMaps();
        }

        public async Task<List<RoleModulePermission>> GetRMPMaps()
        {
            return await _dal.GetRMPMaps();
        }

        /// <summary>
        /// 批量更新菜单与接口的关系
        /// </summary>
        /// <param name="permissionId">菜单主键</param>
        /// <param name="moduleId">接口主键</param>
        /// <returns></returns>
        public async Task UpdateModuleId(int permissionId, int moduleId)
        {
            await _dal.UpdateModuleId(permissionId, moduleId);
        }

       

       
    }
}
