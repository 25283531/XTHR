using System;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 工资期间
    /// </summary>
    public class PayrollPeriod
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 期间名称
        /// </summary>
        public string Name => $"{Year}年{Month:00}月";

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate => new DateTime(Year, Month, 1);

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate => StartDate.AddMonths(1).AddDays(-1);

        /// <summary>
        /// 构造函数
        /// </summary>
        public PayrollPeriod()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        public PayrollPeriod(int year, int month)
        {
            Year = year;
            Month = month;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}