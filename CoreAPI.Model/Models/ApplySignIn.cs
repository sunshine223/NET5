using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Model.Models
{
   public class ApplySignIn
    {
        /// <summary>
        /// 签到表ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int ApplyId { get; set; }
        /// <summary>
        /// 选手ID
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int ApplyItemId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary> 
        [SugarColumn(IsNullable = false)]
        public int UserId { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 签到状态 1签到
        /// </summary>
        [SugarColumn(IsNullable = false)]
        public int Status { get; set; } = 1;
    }
}
