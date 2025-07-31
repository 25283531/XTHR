using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工统计报表DTO
    /// </summary>
    public class EmployeeStatisticsReportDto
    {
        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 按部门统计
        /// </summary>
        public List<DepartmentStatistics> DepartmentStats { get; set; } = new();

        /// <summary>
        /// 按状态统计
        /// </summary>
        public List<StatusStatistics> StatusStats { get; set; } = new();

        /// <summary>
        /// 按性别统计
        /// </summary>
        public List<GenderStatistics> GenderStats { get; set; } = new();
    }

    /// <summary>
    /// 部门统计DTO
    /// </summary>
    public class DepartmentStatistics
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
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 状态统计DTO
    /// </summary>
    public class StatusStatistics
    {
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 员工数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }

    /// <summary>
    /// 性别统计DTO
    /// </summary>
    public class GenderStatistics
    {
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// 员工数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
    }
}