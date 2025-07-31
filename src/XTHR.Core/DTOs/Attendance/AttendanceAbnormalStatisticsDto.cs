using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤异常统计DTO
    /// </summary>
    public class AttendanceAbnormalStatisticsDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 异常类型
        /// </summary>
        public string AbnormalType { get; set; } = string.Empty;

        /// <summary>
        /// 异常次数
        /// </summary>
        public int AbnormalCount { get; set; }

        /// <summary>
        /// 异常时长（分钟）
        /// </summary>
        public int AbnormalDuration { get; set; }

        /// <summary>
        /// 异常日期列表
        /// </summary>
        public List<DateTime> AbnormalDates { get; set; } = new List<DateTime>();

        /// <summary>
        /// 统计期间开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 统计期间结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 应出勤天数
        /// </summary>
        public int ShouldAttendanceDays { get; set; }

        /// <summary>
        /// 实际出勤天数
        /// </summary>
        public int ActualAttendanceDays { get; set; }

        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }
    }
}