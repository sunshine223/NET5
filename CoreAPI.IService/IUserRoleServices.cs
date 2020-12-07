using CoreAPI.IService.BASE;
using CoreAPI.Model.Models;
using System.Threading.Tasks;

namespace CoreAPI.IService
{
    /// <summary>
    /// UserRoleServices
    /// </summary>	
    public interface IUserRoleServices : IBaseServices<UserRole>
    {

        Task<UserRole> SaveUserRole(int uid, int rid);
        Task<int> GetRoleIdByUid(int uid);
    }
}

