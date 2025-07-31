using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 员工考勤统计DTO
    /// </summary>
    public class EmployeeAttendanceStatisticsDto
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
        /// 统计开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 统计结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

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
        /// 迟到总分钟数
        /// </summary>
        public int TotalLateMinutes { get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 早退总分钟数
        /// </summary>
        public int TotalEarlyLeaveMinutes { get; set; }

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
        /// 迟到率
        /// </summary>
        public decimal LateRate { get; set; }

        /// <summary>
        /// 早退率
        /// </summary>
        public decimal EarlyLeaveRate { get; set; }

        /// <summary>
        /// 缺勤率
        /// </summary>
        public decimal AbsenceRate { get; set; }

        /// <summary>
        /// 平均工作时长（小时/天）
        /// </summary>
        public decimal AverageWorkHours { get; set; }

        /// <summary>
        /// 是否全勤
        /// </summary>
        public bool IsPerfectAttendance { get; set; }
    }
}