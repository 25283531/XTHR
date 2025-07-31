using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工考勤汇总DTO
    /// </summary>
    public class EmployeeAttendanceSummaryDto
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
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 统计月份
        /// </summary>
        public DateTime Month { get; set; }

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
        /// 缺勤天数
        /// </summary>
        public int AbsenceDays { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal LeaveDays { get; set; }

        /// <summary>
        /// 加班小时数
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string AttendanceStatus { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
}