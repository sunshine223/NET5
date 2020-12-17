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
        private readonly IUnitOfWork db;

        public ApplySinginService(IBaseRepository<sysUserInfo> dal, IUnitOfWork unitOfWork)
        {
            this.dal = dal;
            this.db = unitOfWork;
        }


        //public async Task<ApplySignIn> AddSign(ApplySignIn p)
        //{
        //    var list =await db.GetDbClient().Queryable<ApplySignIn,User,UserApply,ApplyItem,Apply>((a,b,c,d,e)=> new
        //    JoinQueryInfos(JoinType.Left,a.ApplyItemId==d.Id,
        //    JoinType.Inner,b.Id==c.UserId,
        //    JoinType.Inner,e.Id==c.ParentId,
        //    JoinType.Inner,d.ParentId==c.ParentId))
        //        .Where(a=>a.ApplyItemId != p.ApplyItemId)
        //        .Where((a, b) => b.UserCode == "")
        //        .Where((a,b,c,d)=>d.Id==p.Id)
        //        .Where((a,b,c,d,e)=>e.SignInTime>=DateTime.Now&&e.EndTime<=DateTime.Now)
        //        .ToListAsync();
        //    return list;
        //}

        public Task<List<ApplySignIn>> GetList(ApplySignIn p)
        {
            throw new NotImplementedException();
        }
    }
}
