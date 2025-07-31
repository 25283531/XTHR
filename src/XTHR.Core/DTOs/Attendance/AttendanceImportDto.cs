using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤导入数据传输对象
    /// </summary>
    public class AttendanceImportDto
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 签退时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string AttendanceStatus { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 验证错误信息
        /// </summary>
        public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}