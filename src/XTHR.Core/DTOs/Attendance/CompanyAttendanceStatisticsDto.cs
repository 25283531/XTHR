using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 公司考勤统计DTO
    /// </summary>
    public class CompanyAttendanceStatisticsDto
    {
        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatisticsDate { get; set; }

        /// <summary>
        /// 应出勤总人数
        /// </summary>
        public int TotalExpectedAttendance { get; set; }

        /// <summary>
        /// 实际出勤人数
        /// </summary>
        public int ActualAttendanceCount { get; set; }

        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }

        /// <summary>
        /// 迟到人数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 迟到率
        /// </summary>
        public decimal LateRate { get; set; }

        /// <summary>
        /// 早退人数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 早退率
        /// </summary>
        public decimal EarlyLeaveRate { get; set; }

        /// <summary>
        /// 缺勤人数
        /// </summary>
        public int AbsenceCount { get; set; }

        /// <summary>
        /// 缺勤率
        /// </summary>
        public decimal AbsenceRate { get; set; }

        /// <summary>
        /// 请假人数
        /// </summary>
        public int LeaveCount { get; set; }

        /// <summary>
        /// 请假率
        /// </summary>
        public decimal LeaveRate { get; set; }

        /// <summary>
        /// 加班人数
        /// </summary>
        public int OvertimeCount { get; set; }

        /// <summary>
        /// 部门统计列表
        /// </summary>
        public List<DepartmentAttendanceStatisticsDto> DepartmentStatistics { get; set; } = new List<DepartmentAttendanceStatisticsDto>();
    }

    /// <summary>
    /// 部门考勤统计DTO
    /// </summary>
    public class DepartmentAttendanceStatisticsDto
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
        /// 应出勤人数
        /// </summary>
        public int ExpectedAttendance { get; set; }

        /// <summary>
        /// 实际出勤人数
        /// </summary>
        public int ActualAttendance { get; set; }

        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }

        /// <summary>
        /// 迟到人数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 早退人数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 缺勤人数
        /// </summary>
        public int AbsenceCount { get; set; }

        /// <summary>
        /// 请假人数
        /// </summary>
        public int LeaveCount { get; set; }

        /// <summary>
        /// 加班人数
        /// </summary>
        public int OvertimeCount { get; set; }
    }
}