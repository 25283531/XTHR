using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 薪资期间
    /// </summary>
    public class PayrollPeriod
    {
        /// <summary>
        /// 期间开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 期间结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 期间名称
        /// </summary>
        public string PeriodName { get; set; } = string.Empty;

        /// <summary>
        /// 期间类型（月度/季度/年度）
        /// </summary>
        public string PeriodType { get; set; } = string.Empty;

        /// <summary>
        /// 是否已结算
        /// </summary>
        public bool IsSettled { get; set; }

        /// <summary>
        /// 工作天数
        /// </summary>
        public int WorkingDays { get; set; }

        /// <summary>
        /// 获取期间描述
        /// </summary>
        public string Description => $"{PeriodName} ({StartDate:yyyy-MM-dd} 至 {EndDate:yyyy-MM-dd})";
    }
}