using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 员工每日考勤DTO
    /// </summary>
    public class EmployeeDailyAttendanceDto
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
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public TimeSpan? CheckInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public TimeSpan? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长（小时）
        /// </summary>
        public decimal WorkHours { get; set; }

        /// <summary>
        /// 加班时长（小时）
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string AttendanceStatus { get; set; } = string.Empty;

        /// <summary>
        /// 迟到分钟数
        /// </summary>
        public int LateMinutes { get; set; }

        /// <summary>
        /// 早退分钟数
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }

        /// <summary>
        /// 是否异常
        /// </summary>
        public bool IsAbnormal { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string? AbnormalDescription { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
}