using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 迟到早退统计DTO
    /// </summary>
    public class LateEarlyLeaveStatisticsDto
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
        /// 迟到总分钟数
        /// </summary>
        public int TotalLateMinutes { get; set; }

        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 早退总分钟数
        /// </summary>
        public int TotalEarlyLeaveMinutes { get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 统计开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 统计结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}