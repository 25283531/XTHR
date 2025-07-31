using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤异常DTO
    /// </summary>
    public class AttendanceAnomalyDto
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
        /// 异常日期
        /// </summary>
        public DateTime AnomalyDate { get; set; }

        /// <summary>
        /// 异常类型（迟到、早退、缺勤、旷工）
        /// </summary>
        public string AnomalyType { get; set; } = string.Empty;

        /// <summary>
        /// 异常描述
        /// </summary>
        public string AnomalyDescription { get; set; } = string.Empty;

        /// <summary>
        /// 应上班时间
        /// </summary>
        public TimeSpan? ExpectedStartTime { get; set; }

        /// <summary>
        /// 实际上班时间
        /// </summary>
        public TimeSpan? ActualStartTime { get; set; }

        /// <summary>
        /// 应下班时间
        /// </summary>
        public TimeSpan? ExpectedEndTime { get; set; }

        /// <summary>
        /// 实际下班时间
        /// </summary>
        public TimeSpan? ActualEndTime { get; set; }

        /// <summary>
        /// 迟到/早退分钟数
        /// </summary>
        public int? MinutesLateOrEarly { get; set; }

        /// <summary>
        /// 是否已处理
        /// </summary>
        public bool IsProcessed { get; set; }

        /// <summary>
        /// 处理说明
        /// </summary>
        public string? ProcessNote { get; set; }
    }
}