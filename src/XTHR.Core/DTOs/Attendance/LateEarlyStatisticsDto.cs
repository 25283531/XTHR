using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 迟到早退统计DTO
    /// </summary>
    public class LateEarlyStatisticsDto
    {
        /// <summary>
        /// 统计开始日期
        /// </summary>
        public string StartDate { get; set; } = string.Empty;

        /// <summary>
        /// 统计结束日期
        /// </summary>
        public string EndDate { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称（可选）
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 迟到总次数
        /// </summary>
        public int TotalLateCount { get; set; }

        /// <summary>
        /// 早退总次数
        /// </summary>
        public int TotalEarlyLeaveCount { get; set; }

        /// <summary>
        /// 迟到员工统计
        /// </summary>
        public List<EmployeeLateEarlyCountDto> LateEmployees { get; set; } = new();

        /// <summary>
        /// 早退员工统计
        /// </summary>
        public List<EmployeeLateEarlyCountDto> EarlyLeaveEmployees { get; set; } = new();

        /// <summary>
        /// 迟到时长分布
        /// </summary>
        public Dictionary<string, int> LateDurationDistribution { get; set; } = new();

        /// <summary>
        /// 早退时长分布
        /// </summary>
        public Dictionary<string, int> EarlyLeaveDurationDistribution { get; set; } = new();
    }

    /// <summary>
    /// 员工迟到早退统计DTO
    /// </summary>
    public class EmployeeLateEarlyCountDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 平均迟到时长（分钟）
        /// </summary>
        public decimal AverageLateDuration { get; set; }

        /// <summary>
        /// 平均早退时长（分钟）
        /// </summary>
        public decimal AverageEarlyLeaveDuration { get; set; }
    }
}