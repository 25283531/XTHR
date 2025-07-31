using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤趋势DTO
    /// </summary>
    public class AttendanceTrendDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 应出勤人数
        /// </summary>
        public int ExpectedAttendanceCount { get; set; }

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

    /// <summary>
    /// 考勤趋势统计结果
    /// </summary>
    public class AttendanceTrendResult
    {
        /// <summary>
        /// 趋势数据列表
        /// </summary>
        public List<AttendanceTrendDto> Trends { get; set; } = new List<AttendanceTrendDto>();

        /// <summary>
        /// 统计开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 统计结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 平均出勤率
        /// </summary>
        public decimal AverageAttendanceRate { get; set; }

        /// <summary>
        /// 总迟到次数
        /// </summary>
        public int TotalLateCount { get; set; }

        /// <summary>
        /// 总早退次数
        /// </summary>
        public int TotalEarlyLeaveCount { get; set; }

        /// <summary>
        /// 总缺勤次数
        /// </summary>
        public int TotalAbsenceCount { get; set; }
    }
}