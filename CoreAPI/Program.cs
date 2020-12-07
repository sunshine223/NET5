using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //��ʼ��Ĭ������Builder
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())//autofac
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder
                 .UseStartup<Startup>()
                 .UseUrls("http://*:8080")
                 .ConfigureLogging((hostingContext, builder) =>
                 {
                     //���˵�ϵͳĬ�ϵ�һЩ��־
                     builder.AddFilter("System", LogLevel.Error);
                     builder.AddFilter("Microsoft", LogLevel.Error);
                     builder.AddFilter("CoreAPI.Common.Token.ApiResponseHandler", LogLevel.Error);

                     //�������ļ�
                     var path = Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config");
                     builder.AddLog4Net(path);
                 });
             })
            // ���ɳ��� web Ӧ�ó���� Microsoft.AspNetCore.Hosting.IWebHost��Build��WebHostBuilder���յ�Ŀ�ģ�������һ�������WebHost����������������
             .Build()
            // ���� web Ӧ�ó�����ֹ�����߳�, ֱ�������رա�
            // �������� ���쳣���鿴 Log �ļ����µ��쳣��־ ��������  
             .Run();
        }
    }
}
