using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 工作日历DTO
    /// </summary>
    public class WorkingCalendarDto
    {
        /// <summary>
        /// 日历ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 是否工作日
        /// </summary>
        public bool IsWorkingDay { get; set; }

        /// <summary>
        /// 是否节假日
        /// </summary>
        public bool IsHoliday { get; set; }

        /// <summary>
        /// 节假日名称
        /// </summary>
        public string? HolidayName { get; set; }

        /// <summary>
        /// 工作时长（小时）
        /// </summary>
        public decimal WorkingHours { get; set; }

        /// <summary>
        /// 上班开始时间
        /// </summary>
        public TimeSpan? WorkStartTime { get; set; }

        /// <summary>
        /// 上班结束时间
        /// </summary>
        public TimeSpan? WorkEndTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}