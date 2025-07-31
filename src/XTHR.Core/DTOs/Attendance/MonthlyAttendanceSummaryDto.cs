using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 月度考勤汇总DTO
    /// </summary>
    public class MonthlyAttendanceSummaryDto
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
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

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
        /// 加班小时数
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 考勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string AttendanceStatus { get; set; } = string.Empty;

        /// <summary>
        /// 详细记录
        /// </summary>
        public List<DailyAttendanceRecordDto> DailyRecords { get; set; } = new();
    }

    /// <summary>
    /// 每日考勤记录DTO
    /// </summary>
    public class DailyAttendanceRecordDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长（小时）
        /// </summary>
        public decimal WorkHours { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
}