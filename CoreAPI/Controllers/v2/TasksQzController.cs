using CoreAPI.Common.Appseting;
using CoreAPI.Filter;
using CoreAPI.IService;
using CoreAPI.Model.BASE;
using CoreAPI.Model.Models;
using CoreAPI.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static CoreAPI.ServiceExtensions.CustomApiVersion;

namespace CoreAPI.Controllers.v2
{
    /// <summary>
    /// 任务调度
    /// </summary>
    [CustomRoute(ApiVersions.V2)]
    [ApiController]
    [Authorize]
    public class TasksQzController : ControllerBase
    {
        private readonly ITasksQzServices tasksQzServices;
        private readonly ISchedulerCenter schedulerCenter;

        public TasksQzController(ITasksQzServices tasksQzServices, ISchedulerCenter schedulerCenter)
        {
            this.tasksQzServices = tasksQzServices;
            this.schedulerCenter = schedulerCenter;
        }
        /// <summary>
        ///查看任务列表 分页获取
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet("GetPage")]
        public async Task<MessageModel<PageModels<TasksQz>>> GetPage(int pageIndex,int PageSize)
        {
            
            var data = await tasksQzServices.GetPage(pageIndex, PageSize);
            return new MessageModel<PageModels<TasksQz>>()
            {
                msg = "获取成功",
                success = true,
                response=new PageModels<TasksQz>()
                {
                    page= pageIndex,
                    PageSize = PageSize,
                    dataCount =data.dataCount,
                    data=data.data,
                    pageCount=data.pageCount,
                }
            };
        }
        /// <summary>
        /// 添加定时任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost("TasksQzAdd")]
        public async Task<MessageModel<string>> TasksQzAdd(TasksQz p)
        {
            var data = new MessageModel<string>();
            var id = (await tasksQzServices.TasksQzAdd(p));
            if (id > 0)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }
            return data;
        }
        /// <summary>
        /// 修改定时任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPut("TasksQzUpdate")]
        public async Task<MessageModel<string>> TasksQzUpdate(TasksQz p)
        {
            var data = new MessageModel<string>();
            var id = (await tasksQzServices.TasksQzUpdate(p));
            if (id != false)
            {
                data.response = id.ObjToString();
                data.msg = "修改成功";
            }
            return data;
        }
        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpGet("TasksQzStart")]
        public async Task<MessageModel<string>> TasksQzStart(TasksQz p)
        {
            var data = new MessageModel<string>();
            var id = (await tasksQzServices.TasksQzStart(p));
            if (id != false)
            {
                data.response = id.ObjToString();
                data.msg = "修改成功";
            }
            return data;
        }
        /// <summary>
        /// 停止定时任务
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpGet("TasksQzStop")]
        public async Task<MessageModel<string>> TasksQzStop(TasksQz p)
        {
            var data = new MessageModel<string>();
            var id = (await tasksQzServices.TasksQzStop(p));
            if (id != false)
            {
                data.response = id.ObjToString();
                data.msg = "修改成功";
            }
            return data;
        }
    }
}
