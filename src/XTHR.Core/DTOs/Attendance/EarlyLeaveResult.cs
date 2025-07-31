using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 早退检查结果
    /// </summary>
    public class EarlyLeaveResult
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
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 标准下班时间
        /// </summary>
        public DateTime StandardEndTime { get; set; }

        /// <summary>
        /// 实际打卡时间
        /// </summary>
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 早退时长（分钟）
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }

        /// <summary>
        /// 是否早退
        /// </summary>
        public bool IsEarlyLeave { get; set; }

        /// <summary>
        /// 早退等级（1-轻微，2-一般，3-严重）
        /// </summary>
        public int EarlyLeaveLevel { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string? Remark { get; set; }
    }
}