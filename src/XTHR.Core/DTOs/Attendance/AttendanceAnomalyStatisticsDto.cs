using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤异常统计DTO
    /// </summary>
    public class AttendanceAnomalyStatisticsDto
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
        /// 缺勤次数
        /// </summary>
        public int AbsenceCount { get; set; }

        /// <summary>
        /// 旷工次数
        /// </summary>
        public int AbsenteeismCount { get; set; }

        /// <summary>
        /// 异常总次数
        /// </summary>
        public int TotalAnomalyCount { get; set; }

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