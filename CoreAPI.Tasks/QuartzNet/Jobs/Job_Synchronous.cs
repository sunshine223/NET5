using CoreAPI.Common.Appseting;
using CoreAPI.Common.Helper;
using CoreAPI.IService;
using CoreAPI.IServices;
using Quartz;
using System;
using System.Threading.Tasks;

namespace CoreAPI.Tasks
{
    public class Job_Synchronous : JobBase, IJob
    {
        private readonly IRoleModulePermissionServices roleModulePermissionServices;
        private readonly ITasksQzServices tasksQzServices;

        public Job_Synchronous(IRoleModulePermissionServices roleModulePermissionServices, ITasksQzServices tasksQzServices)
        {
            this.roleModulePermissionServices = roleModulePermissionServices;
            this.tasksQzServices = tasksQzServices;
        }
        public async  Task Execute(IJobExecutionContext context)
        {
            //获取 JobDetail 的值
            var jobKey = context.JobDetail.Key;
            var jobId = jobKey.Name;
            var executeLog = await ExecuteJob(context, async () => await Run(context, jobId.ObjToInt()));
            // 也可以通过数据库配置，获取传递过来的参数
            JobDataMap data = context.JobDetail.JobDataMap;
        }

        private async Task Run(IJobExecutionContext context, int jobId)
        {
            var list = await roleModulePermissionServices.QueryMuchTable();
            if (jobId > 0)
            {
                var model = await tasksQzServices.QueryById(jobId);
                if (model != null)
                {
                    model.RunTimes += 1;
                    var separator = "<br>";
                    model.Remark =
                        $"【{DateTime.Now}】执行任务【Id：{context.JobDetail.Key.Name}，组别：{context.JobDetail.Key.Group}】【执行成功】{separator}"
                        + string.Join(separator, StringHelper.GetTopDataBySeparator(model.Remark, separator, 9));
                    await tasksQzServices.Update(model);
                }
            }
            await Console.Out.WriteLineAsync("菜单总数量" + list.Count.ToString());
        }
    }
}
