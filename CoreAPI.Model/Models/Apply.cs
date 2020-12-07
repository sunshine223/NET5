using SqlSugar;
using System;

namespace CoreAPI.Model.Models
{
    public  class Apply
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 节目名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Img { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Descn { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报名地址
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Url { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 上限人数
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Number { get; set; }
        /// <summary>
        ///  1:全类型(个人和团体) 2：个人 3：团队
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Type { get; set; }
        /// <summary>
        /// 是否付费 0：否 1：是
        /// </summary>
        public bool IsPay { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Price { get; set; }
        /// <summary>
        /// 是否开启评选助力 0：否 1：是
        /// </summary>
        public bool IsHelpSelection { get; set; }
        /// <summary>
        /// 比赛时间
        /// </summary>
        public DateTime MatchTime { get; set; }
        /// <summary>
        /// 主办方电话
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string HostPhone { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime SignInTime { get; set; }
        /// <summary>
        /// 比赛地点
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Venue { get; set; }
        /// <summary>
        /// 是否开启投票助力 0：否 1：是
        /// </summary>
        public bool IsHelpActivity { get; set; }
        /// <summary>
        /// 1通过 2禁止 3等待
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Status { get; set; }
        /// <summary>
        /// 用户报名限制
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int TriesLimit { get; set; }
        /// <summary>
        /// 报名阶段（1.预约，2.正在报名）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Stage { get; set; }
        /// <summary>
        /// 白名单
        /// </summary>
        public bool IsWhite { get; set; }

    }
}
