using SqlSugar;
using System;

namespace CoreAPI.Model.Models
{
    public  class ApplyItem
    {
        /// <summary>
        /// 小金钟网络赛报名表
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Code { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(IsNullable =true)]
        public int Sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Nation { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 参赛年龄阶段空不限制
        /// </summary>
         [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Stage { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Parent { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Mobile { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string QQ { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Email { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Address { get; set; }
        /// <summary>
        /// 节目名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Program { get; set; }
        /// <summary>
        /// 自我介绍
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Descn { get; set; }
        /// <summary>
        /// 组别
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Levels { get; set; }
        /// <summary>
        /// 声乐类或器乐类
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Category { get; set; }
        /// <summary>
        /// 种类
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Kind { get; set; }
        /// <summary>
        /// 网盘链接
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Link { get; set; }
        /// <summary>
        /// 提取密码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string ExtractCode { get; set; }
        /// <summary>
        /// 选手头像
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string HeadImage { get; set; }
        /// <summary>
        /// 选送机构
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Organization { get; set; }
        /// <summary>
        /// 机构区域
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string OrganizationArea { get; set; }
        /// <summary>
        /// 推荐老师
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Recommended { get; set; }
        /// <summary>
        /// 老师电话
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string RecommendedMobile { get; set; }
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 参赛类型，个人或团队
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Type { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string People { get; set; }
        /// <summary>
        /// 所属活动id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int ParentId { get; set; }
        /// <summary>
        /// 原送选赛区原送选赛区
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string PrimaryRegion { get; set; }
        /// <summary>
        /// 节目时长
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string ProgramDuration { get; set; }
        /// <summary>
        /// 有无伴奏 0：所有 1 ：有伴奏 2：无伴奏
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int IsAccompaniment { get; set; }
        /// <summary>
        /// 乐器名称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string MusicalName { get; set; }
        /// <summary>
        /// 有无伴奏 0：所有 1 ：有伴奏 2：无伴奏
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int CompetitionForm { get; set; }
        /// <summary>
        /// 参赛作品形式
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string WorksForm { get; set; }
        /// <summary>
        /// 选手排名
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Rank { get; set; }
        /// <summary>
        /// 1:晋级 2：未晋级
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Result { get; set; }
        /// <summary>
        /// 来源统计(1.扫描 2.链接)
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Source { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int Status { get; set; }
    }
}
