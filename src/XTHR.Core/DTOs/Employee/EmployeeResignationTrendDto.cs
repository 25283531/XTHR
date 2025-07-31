using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工离职趋势DTO
    /// </summary>
    public class EmployeeResignationTrendDto
    {
        /// <summary>
        /// 趋势数据
        /// </summary>
        public List<ResignationTrendData> Trends { get; set; } = new();

        /// <summary>
        /// 总离职人数
        /// </summary>
        public int TotalResignations { get; set; }

        /// <summary>
        /// 平均每月离职人数
        /// </summary>
        public decimal AverageMonthlyResignations { get; set; }

        /// <summary>
        /// 离职率
        /// </summary>
        public decimal ResignationRate { get; set; }
    }

    /// <summary>
    /// 离职趋势数据DTO
    /// </summary>
    public class ResignationTrendData
    {
        /// <summary>
        /// 月份
        /// </summary>
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// 离职人数
        /// </summary>
        public int ResignationCount { get; set; }

        /// <summary>
        /// 同比增长率
        /// </summary>
        public decimal YearOverYearGrowth { get; set; }

        /// <summary>
        /// 离职原因统计
        /// </summary>
        public Dictionary<string, int> ResignationReasons { get; set; } = new();
    }
}