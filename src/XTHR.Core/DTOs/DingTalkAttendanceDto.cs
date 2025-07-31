using System;
using XTHR.Common.Services;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 钉钉考勤导入数据传输对象
    /// </summary>
    public class DingTalkAttendanceDto
    {
        [ExcelColumn("姓名")]
        public string EmployeeName { get; set; }

        [ExcelColumn("工号")]
        public string EmployeeCode { get; set; }

        [ExcelColumn("部门")]
        public string Department { get; set; }

        [ExcelColumn("日期")]
        public DateTime AttendanceDate { get; set; }

        [ExcelColumn("上班时间")]
        public TimeSpan? PunchInTime { get; set; }

        [ExcelColumn("下班时间")]
        public TimeSpan? PunchOutTime { get; set; }

        [ExcelColumn("考勤状态")]
        public string Status { get; set; }

        [ExcelColumn("迟到时长(分钟)")]
        public int? LateMinutes { get; set; }

        [ExcelColumn("早退时长(分钟)")]
        public int? EarlyLeaveMinutes { get; set; }

        [ExcelColumn("缺卡次数")]
        public int? MissedPunches { get; set; }

        [ExcelColumn("加班时长(小时)")]
        public decimal? OvertimeHours { get; set; }

        [ExcelColumn("请假时长(小时)")]
        public decimal? LeaveHours { get; set; }

        [ExcelColumn("出勤时长(小时)")]
        public decimal? WorkHours { get; set; }

        [ExcelColumn("备注")]
        public string Remarks { get; set; }
    }
}