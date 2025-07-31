using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 员工月度考勤汇总DTO
    /// </summary>
    public class EmployeeMonthlyAttendanceSummaryDto
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
        public int ExpectedWorkDays { get; set; }

        /// <summary>
        /// 实际出勤天数
        /// </summary>
        public int ActualWorkDays { get; set; }

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
        /// 是否全勤
        /// </summary>
        public bool IsPerfectAttendance { get; set; }

        /// <summary>
        /// 考勤记录详情
        /// </summary>
        public List<DailyAttendanceDetailDto> DailyDetails { get; set; } = new List<DailyAttendanceDetailDto>();
    }

    /// <summary>
    /// 每日考勤详情DTO
    /// </summary>
    public class DailyAttendanceDetailDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 是否工作日
        /// </summary>
        public bool IsWorkDay { get; set; }

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
        /// 异常说明
        /// </summary>
        public string? AbnormalDescription { get; set; }
    }
}