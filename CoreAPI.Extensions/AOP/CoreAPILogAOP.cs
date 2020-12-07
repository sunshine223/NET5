using Castle.DynamicProxy;
using CoreAPI.Common.Helper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreAPI.Extensions.AOP
{
    public class CoreAPILogAOP : IInterceptor
    {
        private readonly IHttpContextAccessor _accessor;

        public CoreAPILogAOP(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        /// <summary>
        /// 实例化Intercept唯一方法
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            string UserName = _accessor.HttpContext?.User?.Identity?.Name;
            //该方法只针对同步，异步记录需要重新写，该方法留bug 简单记录日志需要扩展自定义
            // 事前处理: 在服务方法执行之前,做相应的逻辑处理
            var dataIntercept = "" +
                $"【当前操作用户】：{ UserName} \r\n" +
                $"【当前执行方法】：{ invocation.Method.Name} \r\n" +
                $"【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";

            // 执行当前访问的服务方法,(注意:如果下边还有其他的AOP拦截器的话,会跳转到其他的AOP里)
            try
            {
                // 就是这里！！
                MiniProfiler.Current.Step($"执行Service方法：{invocation.Method.Name}() -> ");
                invocation.Proceed();// 异步获取异常，先执行
                if (IsAsyncMethod(invocation.Method))
                {
                    #region 方案一
                    //Wait task execution and modify return value
                    if (invocation.Method.ReturnType == typeof(Task))
                    {
                        invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                            (Task)invocation.ReturnValue,
                            async () => await SuccessAction(invocation, dataIntercept),/*成功时执行*/
                            ex =>
                            {
                                LogEx(ex, dataIntercept);
                            });
                    }
                    //Task<TResult>
                    else
                    {
                        invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                         invocation.Method.ReturnType.GenericTypeArguments[0],
                         invocation.ReturnValue,
                          //async () => await SuccessAction(invocation, dataIntercept),/*成功时执行*/
                          async (o) => await SuccessAction(invocation, dataIntercept, o),/*成功时执行*/
                         ex =>
                         {
                             LogEx(ex, dataIntercept);
                         });
                    }
                    #endregion
                    // 如果方案一不行，试试这个方案
                    #region 方案二

                    //var type = invocation.Method.ReturnType;
                    //var resultProperty = type.GetProperty("Result");
                    //dataIntercept += ($"【执行完成结果】：{JsonConvert.SerializeObject(resultProperty.GetValue(invocation.ReturnValue))}");

                    //Parallel.For(0, 1, e =>
                    //{
                    //    LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
                    //});

                    #endregion
                }
                else
                {// 同步1

                    dataIntercept += ($"【执行完成结果】：{invocation.ReturnValue}");
                    Parallel.For(0, 1, e =>
                    {
                        LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
                    });
                }
            }
            catch (Exception ex)// 同步2
            {
                LogEx(ex, dataIntercept);

            }
         
        }

        private async Task SuccessAction(IInvocation invocation, string dataIntercept, object o = null)
        {

            dataIntercept += ($"【执行完成结果】：{JsonConvert.SerializeObject(o)}");


            await Task.Run(() =>
            {
                Parallel.For(0, 1, e =>
                {
                    LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
                });
            });
        }

        private void LogEx(Exception ex, string dataIntercept)
        {
            if (ex != null)
            {
                //执行的 service 中，收录异常
                MiniProfiler.Current.CustomTiming("Errors：", ex.Message);
                //执行的 service 中，捕获异常
                dataIntercept += ($"【执行完成结果】：方法中出现异常：{ex.Message + ex.InnerException}\r\n");

                // 异常日志里有详细的堆栈信息
                Parallel.For(0, 1, e =>
                {
                    LogLock.OutSql2Log("AOPLog", new string[] { dataIntercept });
                });
            }
        }


        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

    }


    internal static class InternalAsyncHelper
    {
        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
                await postAction();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                finalAction(exception);
            }
        }

        public static async Task<T> AwaitTaskWithPostActionAndFinallyAndGetResult<T>(Task<T> actualReturnValue, Func<object, Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;
            try
            {
                var result = await actualReturnValue;
                await postAction(result);
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        public static object CallAwaitTaskWithPostActionAndFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Func<object, Task> action, Action<Exception> finalAction)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, action, finalAction });
        }
    
}
}
