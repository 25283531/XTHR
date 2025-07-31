using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 加班计算结果DTO
    /// </summary>
    public class OvertimeCalculationResult
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 标准工作时长（小时）
        /// </summary>
        public decimal StandardWorkHours { get; set; }

        /// <summary>
        /// 实际工作时长（小时）
        /// </summary>
        public decimal ActualWorkHours { get; set; }

        /// <summary>
        /// 加班时长（小时）
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 平时加班时长（小时）
        /// </summary>
        public decimal RegularOvertimeHours { get; set; }

        /// <summary>
        /// 周末加班时长（小时）
        /// </summary>
        public decimal WeekendOvertimeHours { get; set; }

        /// <summary>
        /// 节假日加班时长（小时）
        /// </summary>
        public decimal HolidayOvertimeHours { get; set; }

        /// <summary>
        /// 加班费率
        /// </summary>
        public decimal OvertimeRate { get; set; }

        /// <summary>
        /// 加班工资
        /// </summary>
        public decimal OvertimePay { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
    }
}