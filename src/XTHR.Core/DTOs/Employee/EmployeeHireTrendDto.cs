using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工入职趋势DTO
    /// </summary>
    public class EmployeeHireTrendDto
    {
        /// <summary>
        /// 趋势数据
        /// </summary>
        public List<HireTrendData> Trends { get; set; } = new();

        /// <summary>
        /// 总入职人数
        /// </summary>
        public int TotalHires { get; set; }

        /// <summary>
        /// 平均每月入职人数
        /// </summary>
        public decimal AverageMonthlyHires { get; set; }
    }

    /// <summary>
    /// 入职趋势数据DTO
    /// </summary>
    public class HireTrendData
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// 入职人数
        /// </summary>
        public int HireCount { get; set; }

        /// <summary>
        /// 同比增长率
        /// </summary>
        public decimal YearOverYearGrowth { get; set; }
    }
}