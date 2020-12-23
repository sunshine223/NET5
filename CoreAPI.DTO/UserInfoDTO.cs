using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreAPI.DTO
{
    public class UserInfoDTO
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string uLoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string uLoginPWD { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string uRealName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int uStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string uRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? uCreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? uUpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime uLastErrTime { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        public int uErrorCount { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// // 性别
        /// </summary>
        public int sex { get; set; } = 0;
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }
        /// <summary>
        ///  生日
        /// </summary>
        public DateTime birth { get; set; } = DateTime.Now;
        /// <summary>
        /// 地址
        /// </summary>
        public string addr { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool tdIsDelete { get; set; } = false;


    }
}
