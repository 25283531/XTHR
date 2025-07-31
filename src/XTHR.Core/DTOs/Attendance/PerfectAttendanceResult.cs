using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 全勤奖结果DTO
    /// </summary>
    public class PerfectAttendanceResult
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
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 统计月份
        /// </summary>
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// 应出勤天数
        /// </summary>
        public int ShouldAttendanceDays { get; set; }

        /// <summary>
        /// 实际出勤天数
        /// </summary>
        public int ActualAttendanceDays { get; set; }

        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 旷工次数
        /// </summary>
        public int AbsentCount { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal LeaveDays { get; set; }

        /// <summary>
        /// 是否符合全勤奖条件
        /// </summary>
        public bool IsPerfectAttendance { get; set; }

        /// <summary>
        /// 全勤奖金额
        /// </summary>
        public decimal PerfectAttendanceBonus { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string? Remarks { get; set; }
    }
}