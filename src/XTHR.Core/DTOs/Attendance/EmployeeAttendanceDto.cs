using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 员工考勤信息DTO
    /// </summary>
    public class EmployeeAttendanceDto
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
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

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
        /// 是否迟到
        /// </summary>
        public bool IsLate { get; set; }

        /// <summary>
        /// 迟到分钟数
        /// </summary>
        public int LateMinutes { get; set; }

        /// <summary>
        /// 是否早退
        /// </summary>
        public bool IsEarlyLeave { get; set; }

        /// <summary>
        /// 早退分钟数
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }

        /// <summary>
        /// 是否缺勤
        /// </summary>
        public bool IsAbsence { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public string? LeaveType { get; set; }

        /// <summary>
        /// 请假小时数
        /// </summary>
        public decimal LeaveHours { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
}