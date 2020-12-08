using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreAPI.DTO
{
    public class UserInfoDTO
    {

        /// <summary>
        /// 活动ID
        /// </summary>
        public int ApplyId { get; set; }
        /// <summary>
        /// 选手ID
        /// </summary>
        public int ApplyItemId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary> 
        public int UserId { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 签到状态 1签到
        /// </summary>
        public int Status { get; set; } = 1;

    }
}
