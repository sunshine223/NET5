using SqlSugar;
using System;

namespace CoreAPI.Model.Models
{
    public  class UserApply
    {
        /// <summary>
        /// 用户报名表
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 报名用户
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int UserId { get; set; }
        /// <summary>
        /// 报名表Id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int ApplyItemId { get; set; }
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 报名状态
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Status { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>     
        [SugarColumn(IsNullable = true)]
        public int ParentId { get; set; }
    }
}
