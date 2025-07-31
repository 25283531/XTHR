using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 薪资期间DTO
    /// </summary>
    public class PayrollPeriodDto
    {
        /// <summary>
        /// 薪资期间ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 期间名称
        /// </summary>
        public string PeriodName { get; set; } = string.Empty;

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 是否当前期间
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 状态：0-未开始，1-进行中，2-已完成，3-已关闭
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
    }
}