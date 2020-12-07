using CoreAPI.Common.DB;
using SqlSugar;
using System;

namespace CoreAPI.Service
{
    public interface ISqlSugarRepository
    {
        SqlSugarClient GetDb();
    }

    public class SqlSugarRepository : ISqlSugarRepository
    {

        SqlSugarClient ISqlSugarRepository.GetDb()
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = BaseDBConfig.ConnectionString,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                AopEvents = new AopEvents
                {
                    OnLogExecuting = (sql, p) =>
                    {
                        Console.WriteLine(sql);
                    }
                }
            });
        }
    }
}
