using CoreAPI.Commo.DB;
using CoreAPI.Common.Appseting;
using CoreAPI.Common.DB;
using CoreAPI.Common.Helper;
using CoreAPI.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Model.Seed
{
    public class DBSeed
    {
        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        public static async Task SeedAsync(MyContext myContext)
        {
            try
            {
                Console.WriteLine("************ CoreAPI DataBase Set *****************");
                Console.WriteLine($"Is multi-DataBase: {Appsettings.app(new string[] { "MutiDBEnabled" })}");
                Console.WriteLine($"Is CQRS: {Appsettings.app(new string[] { "CQRSEnabled" })}");
                Console.WriteLine();
                Console.WriteLine($"Master DB ConId: {MyContext.ConnId}");
                Console.WriteLine($"Master DB Type: {MyContext.DbType}");
                Console.WriteLine($"Master DB ConnectString: {MyContext.ConnectionString}");
                Console.WriteLine();
                if (Appsettings.app(new string[] { "MutiDBEnabled" }).ObjToBool())
                {
                    var slaveIndex = 0;
                    BaseDBConfig.MutiConnectionString.Item1.Where(x => x.ConnId != MainDb.CurrentDbConnId).ToList().ForEach(m =>
                    {
                        slaveIndex++;
                        Console.WriteLine($"Slave{slaveIndex} DB ID: {m.ConnId}");
                        Console.WriteLine($"Slave{slaveIndex} DB Type: {m.DbType}");
                        Console.WriteLine($"Slave{slaveIndex} DB ConnectString: {m.Connection}");
                        Console.WriteLine($"--------------------------------------");
                    });
                }
                else if (Appsettings.app(new string[] { "CQRSEnabled" }).ObjToBool())
                {
                    var slaveIndex = 0;
                    BaseDBConfig.MutiConnectionString.Item2.Where(x => x.ConnId != MainDb.CurrentDbConnId).ToList().ForEach(m =>
                    {
                        slaveIndex++;
                        Console.WriteLine($"Slave{slaveIndex} DB ID: {m.ConnId}");
                        Console.WriteLine($"Slave{slaveIndex} DB Type: {m.DbType}");
                        Console.WriteLine($"Slave{slaveIndex} DB ConnectString: {m.Connection}");
                        Console.WriteLine($"--------------------------------------");
                    });
                }
                else
                {
                }

                Console.WriteLine();
                // 创建数据库
                Console.WriteLine($"Create Database(The Db Id:{MyContext.ConnId})...");
                myContext.Db.DbMaintenance.CreateDatabase();
                ConsoleHelper.WriteSuccessLine($"Database created successfully!");
                // 创建数据库表，遍历指定命名空间下的class，
                // 注意不要把其他命名空间下的也添加进来。
                Console.WriteLine("Create Tables...");
                var modelTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.IsClass && t.Namespace == "CoreAPI.Model.Models"
                        select t;
                modelTypes.ToList().ForEach(t =>
                {
                    if (!myContext.Db.DbMaintenance.IsAnyTable(t.Name))
                    {
                        Console.WriteLine(t.Name);
                        myContext.Db.CodeFirst.InitTables(t);
                    }
                });
                ConsoleHelper.WriteSuccessLine($"Tables created successfully!");
                Console.WriteLine();
                //if (Appsettings.app(new string[] { "AppSettings", "SeedDBDataEnabled" }).ObjToBool())
                //{
                //    Console.WriteLine($"Seeding database data (The Db Id:{MyContext.ConnId})...");
                //    #region BlogArticle
                //    if (!await myContext.Db.Queryable<BlogArticle>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:BlogArticle already exists...");
                //    }
                //    #endregion
                //    #region Modules
                //    if (!await myContext.Db.Queryable<Modules>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:Modules already exists...");
                //    }
                //    #endregion
                //    #region Permission
                //    if (!await myContext.Db.Queryable<Permission>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:Permission already exists...");
                //    }
                //    #endregion
                //    #region Role
                //    if (!await myContext.Db.Queryable<Role>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:Role already exists...");
                //    }
                //    #endregion
                //    #region RoleModulePermission
                //    if (!await myContext.Db.Queryable<RoleModulePermission>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:RoleModulePermission already exists...");
                //    }
                //    #endregion
                //    #region Topic
                //    if (!await myContext.Db.Queryable<Topic>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:Topic already exists...");
                //    }
                //    #endregion
                //    #region TopicDetail
                //    if (!await myContext.Db.Queryable<TopicDetail>().AnyAsync())
                //    {
                    
                //        Console.WriteLine("Table:TopicDetail already exists...");
                //    }
                //    #endregion
                //    #region UserRole
                //    if (!await myContext.Db.Queryable<UserRole>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:UserRole already exists...");
                //    }
                //    #endregion
                //    #region ApplySignIn
                //    if (!await myContext.Db.Queryable<ApplySignIn>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:ApplySignIn already exists...");
                //    }
                //    #endregion
                //    #region Apply
                //    if (!await myContext.Db.Queryable<Apply>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:Apply already exists...");
                //    }
                //    #endregion
                //    #region ApplyItem
                //    if (!await myContext.Db.Queryable<ApplyItem>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:ApplyItem already exists...");
                //    }
                //    #endregion
                //    #region User
                //    if (!await myContext.Db.Queryable<User>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:User already exists...");
                //    }
                //    #endregion
                //    #region UserApply
                //    if (!await myContext.Db.Queryable<UserApply>().AnyAsync())
                //    {
                //        Console.WriteLine("Table:UserApply already exists...");
                //    }
                //    #endregion
                //    ConsoleHelper.WriteSuccessLine($"Done seeding database!");
                //}
                //Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception("1、如果使用的是Mysql，生成的数据库字段字符集可能不是utf8的，手动修改下，或者尝试方案：删掉数据库，在连接字符串后加上CharSet=UTF8mb4，重新生成数据库. \n 2、其他错误：" + ex.Message);
            }
        }
    }
}