using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 薪资成本分析DTO
    /// </summary>
    public class PayrollCostAnalysisDto
    {
        /// <summary>
        /// 分析月份
        /// </summary>
        public DateTime AnalysisMonth { get; set; }

        /// <summary>
        /// 总人工成本
        /// </summary>
        public decimal TotalLaborCost { get; set; }

        /// <summary>
        /// 部门成本列表
        /// </summary>
        public List<DepartmentCostDto> DepartmentCosts { get; set; } = new();

        /// <summary>
        /// 成本构成分析
        /// </summary>
        public List<CostComponentDto> CostComponents { get; set; } = new();

        /// <summary>
        /// 环比增长率
        /// </summary>
        public decimal MonthOverMonthGrowth { get; set; }

        /// <summary>
        /// 同比增长率
        /// </summary>
        public decimal YearOverYearGrowth { get; set; }
    }

    /// <summary>
    /// 部门成本DTO
    /// </summary>
    public class DepartmentCostDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 员工人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// 人均成本
        /// </summary>
        public decimal AverageCost { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 成本构成DTO
    /// </summary>
    public class CostComponentDto
    {
        /// <summary>
        /// 成本类型
        /// </summary>
        public string CostType { get; set; } = string.Empty;

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 薪资趋势分析DTO
    /// </summary>
    public class PayrollTrendAnalysisDto
    {
        /// <summary>
        /// 分析期间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 趋势数据
        /// </summary>
        public List<PayrollTrendDataDto> TrendData { get; set; } = new();

        /// <summary>
        /// 预测数据
        /// </summary>
        public List<PayrollForecastDto> ForecastData { get; set; } = new();
    }

    /// <summary>
    /// 薪资趋势数据DTO
    /// </summary>
    public class PayrollTrendDataDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 总薪资
        /// </summary>
        public decimal TotalSalary { get; set; }

        /// <summary>
        /// 员工人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 人均薪资
        /// </summary>
        public decimal AverageSalary { get; set; }
    }

    /// <summary>
    /// 薪资预测DTO
    /// </summary>
    public class PayrollForecastDto
    {
        /// <summary>
        /// 预测日期
        /// </summary>
        public DateTime ForecastDate { get; set; }

        /// <summary>
        /// 预测薪资
        /// </summary>
        public decimal ForecastSalary { get; set; }

        /// <summary>
        /// 置信区间下限
        /// </summary>
        public decimal ConfidenceLower { get; set; }

        /// <summary>
        /// 置信区间上限
        /// </summary>
        public decimal ConfidenceUpper { get; set; }
    }

    /// <summary>
    /// 薪资分布分析DTO
    /// </summary>
    public class PayrollDistributionAnalysisDto
    {
        /// <summary>
        /// 分析月份
        /// </summary>
        public DateTime AnalysisMonth { get; set; }

        /// <summary>
        /// 薪资区间分布
        /// </summary>
        public List<SalaryRangeDistributionDto> RangeDistribution { get; set; } = new();

        /// <summary>
        /// 部门分布
        /// </summary>
        public List<DepartmentDistributionDto> DepartmentDistribution { get; set; } = new();

        /// <summary>
        /// 职级分布
        /// </summary>
        public List<LevelDistributionDto> LevelDistribution { get; set; } = new();
    }

    /// <summary>
    /// 薪资区间分布DTO
    /// </summary>
    public class SalaryRangeDistributionDto
    {
        /// <summary>
        /// 薪资区间
        /// </summary>
        public string Range { get; set; } = string.Empty;

        /// <summary>
        /// 人数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 部门分布DTO
    /// </summary>
    public class DepartmentDistributionDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 人数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 平均薪资
        /// </summary>
        public decimal AverageSalary { get; set; }
    }

    /// <summary>
    /// 职级分布DTO
    /// </summary>
    public class LevelDistributionDto
    {
        /// <summary>
        /// 职级
        /// </summary>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// 人数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 平均薪资
        /// </summary>
        public decimal AverageSalary { get; set; }
    }

    /// <summary>
    /// 薪资分布分析请求DTO
    /// </summary>
    public class PayrollDistributionAnalysisRequest
    {
        /// <summary>
        /// 分析月份
        /// </summary>
        public DateTime AnalysisMonth { get; set; }

        /// <summary>
        /// 部门ID（可选）
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// 职级（可选）
        /// </summary>
        public string? Level { get; set; }

        /// <summary>
        /// 最小薪资（可选）
        /// </summary>
        public decimal? MinSalary { get; set; }

        /// <summary>
        /// 最大薪资（可选）
        /// </summary>
        public decimal? MaxSalary { get; set; }
    }
}