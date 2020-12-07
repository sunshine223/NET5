using CoreAPI.IService;
using CoreAPI.Model.BASE;
using CoreAPI.Model.Models;
using CoreAPI.Repository.BASE;
using CoreAPI.Service.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI.Service
{
    /// <summary>
    ///任务调度
    /// </summary>
    public partial class TasksQzServices : BaseServices<TasksQz>, ITasksQzServices
    {
        IBaseRepository<TasksQz> _dal;
        public TasksQzServices(IBaseRepository<TasksQz> dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }

        public async Task<PageModels<TasksQz>> GetPage(int pageIndex, int PageSize)
        {
            PageModels<TasksQz> models = new PageModels<TasksQz>();
            models = await base.QueryPage(a => a.IsDeleted != true, pageIndex, PageSize, "Id desc ");
            return models;
        }

        public async  Task<int> TasksQzAdd(TasksQz p)
        {
            return await base.Add(p);
        }

        public async Task<bool> TasksQzStart(TasksQz p)
        {
            return await base.Update(p);
        }

        public async Task<bool> TasksQzStop(TasksQz p)
        {
            return await base.Update(p);
        }

        public async Task<bool> TasksQzUpdate(TasksQz p)
        {
            return await base.Update(p);
        }
    }
}
