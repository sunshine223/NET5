using CoreAPI.IService.BASE;
using CoreAPI.Model.BASE;
using CoreAPI.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI.IService
{
    /// <summary>
    /// ITasksQzServices
    /// </summary>
    public interface ITasksQzServices:IBaseServices<TasksQz>
    {
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <returns></returns>
        Task<PageModels<TasksQz>> GetPage(int pageIndex, int PageSize);

        /// <summary>
        ///  添加计划任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Task<int> TasksQzAdd(TasksQz p);
        /// <summary>
        /// 修改计划任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Task<bool> TasksQzUpdate(TasksQz p);
        /// <summary>
        /// 启动计划任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Task<bool> TasksQzStart(TasksQz p);
        /// <summary>
        /// 停止计划任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Task<bool> TasksQzStop(TasksQz p);
    }
}
