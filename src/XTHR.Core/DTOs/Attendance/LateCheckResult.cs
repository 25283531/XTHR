using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 迟到检查结果
    /// </summary>
    public class LateCheckResult
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
        /// 标准上班时间
        /// </summary>
        public DateTime StandardStartTime { get; set; }

        /// <summary>
        /// 实际打卡时间
        /// </summary>
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 迟到时长（分钟）
        /// </summary>
        public int LateMinutes { get; set; }

        /// <summary>
        /// 是否迟到
        /// </summary>
        public bool IsLate { get; set; }

        /// <summary>
        /// 迟到等级（1-轻微，2-一般，3-严重）
        /// </summary>
        public int LateLevel { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string? Remark { get; set; }
    }
}