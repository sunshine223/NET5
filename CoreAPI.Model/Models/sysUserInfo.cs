using SqlSugar;
using System;
using System.Collections.Generic;

namespace CoreAPI.Model.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class sysUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int uID { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(ColumnDataType ="nvarchar",Length = 200, IsNullable = true)]
        public string uLoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(ColumnDataType ="nvarchar",Length = 200, IsNullable = true)]
        public string uLoginPWD { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(ColumnDataType ="nvarchar",Length = 200, IsNullable = true)]
        public string uRealName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int uStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDataType ="nvarchar",Length = 2000, IsNullable = true)]
        public string uRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime uCreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime uUpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime uLastErrTime { get; set; }= DateTime.Now;

        /// <summary>
        ///错误次数 
        /// </summary>
        public int uErrorCount { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [SugarColumn(ColumnDataType ="nvarchar",Length = 200, IsNullable = true)]
        public string name { get; set; }
        /// <summary>
        /// // 性别
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int sex { get; set; } = 0;
        /// <summary>
        /// 年龄
        /// </summary>
        
        [SugarColumn(IsNullable = true)]
        public int age { get; set; }
        /// <summary>
        ///  生日
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime birth { get; set; } = DateTime.Now;
        /// <summary>
        /// 地址
        /// </summary>
        [SugarColumn(ColumnDataType ="nvarchar",Length = 200, IsNullable = true)]
        public string addr { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool tdIsDelete { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<int> RIDs { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<string> RoleNames { get; set; }

    }
}
