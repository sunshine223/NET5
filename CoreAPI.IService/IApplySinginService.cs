using CoreAPI.DTO;
using CoreAPI.IService.BASE;
using CoreAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.IService
{
   public interface IApplySinginService: IBaseServices<ApplySignIn>
    {
        Task<object> GetList(ApplySignIn p);
        Task<bool> AddSign(UserInfoDTO p);
    }
}
