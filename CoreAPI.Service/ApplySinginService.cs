using AutoMapper;
using CoreAPI.DTO;
using CoreAPI.IService;
using CoreAPI.Model.Models;
using CoreAPI.Repository.BASE;
using CoreAPI.Repository.UnitOfWork;
using CoreAPI.Service.BASE;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Service
{
    public class ApplySinginService : BaseServices<ApplySignIn>, IApplySinginService
    {
        private readonly IBaseRepository<sysUserInfo> dal;
        private readonly IMapper mapper;
        private readonly SqlSugarClient db;

        public ApplySinginService(IBaseRepository<sysUserInfo> dal, IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.dal = dal;
            this.mapper = mapper;
            this.db = unitOfWork.GetDbClient();
        }


        public async Task<bool> AddSign(UserInfoDTO p)
        {
            bool reslut = false;
            ApplySignIn model = mapper.Map<ApplySignIn>(p);
            var list =await base.Add(model);
            if (list > 0)
            {
                reslut = true;
            }
            return reslut;
        }

        public async Task<object> GetList(ApplySignIn p)
        {
             
            var list = await db.Queryable<ApplySignIn, User, UserApply, ApplyItem, Apply>((a, b, c, d, e) => new
                 JoinQueryInfos(
                 JoinType.Left, a.ApplyItemId == d.Id,
                 JoinType.Inner, b.Id == c.UserId,
                 JoinType.Inner, e.Id == c.ParentId,
                 JoinType.Inner, d.ParentId == c.ParentId))
                .Where(a => a.ApplyItemId != p.ApplyItemId)
                .Where((a, b) => b.UserCode == "")
                .Where((a, b, c, d) => d.Id == p.Id)
                .Where((a, b, c, d, e) => e.SignInTime >= DateTime.Now && e.EndTime <= DateTime.Now)
                .ToListAsync();
            return list;
        }
    }
}
