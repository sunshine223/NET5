using SqlSugar;
using System;

namespace CoreAPI.Model.Models
{
  public  class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string UserName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Address { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Email { get; set; }
        /// <summary>
        /// 邮箱是否激活
        /// </summary>
        public bool EmailVerify { get; set; }
        /// <summary>
        /// 是否绑定手机
        /// </summary>
        public bool PhoneVerify { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string RegisterIP { get; set; }
        /// <summary>
        /// 上次访问时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int VerifyStatus { get; set; }
        /// <summary>
        /// 是否为测试用户
        /// </summary>
        public bool IsTestUser { get; set; }
        /// <summary>
        /// 用户标识（对内） or 微信用户标识
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string EncryptCode { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Mobile { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string HeadImg { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public Nullable<int> Sex { get; set; }
        /// <summary>
        /// 是否是马甲用户：1代表是 2:微信登录用户 3:机器人
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Remark { get; set; }
        public Nullable<int> FromUserId { get; set; }
        /// <summary>
        /// 用户标识（对外开放）
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string UserCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int ForumUid { get; set; }
    }
}
