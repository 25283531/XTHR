using System;
using System.Text.Json.Serialization;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤期间类型
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttendancePeriod
    {
        /// <summary>
        /// 日
        /// </summary>
        Daily,

        /// <summary>
        /// 周
        /// </summary>
        Weekly,

        /// <summary>
        /// 月
        /// </summary>
        Monthly,

        /// <summary>
        /// 季度
        /// </summary>
        Quarterly,

        /// <summary>
        /// 年
        /// </summary>
        Yearly,

        /// <summary>
        /// 自定义期间
        /// </summary>
        Custom
    }

    /// <summary>
    /// 考勤状态
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttendanceStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal,

        /// <summary>
        /// 迟到
        /// </summary>
        Late,

        /// <summary>
        /// 早退
        /// </summary>
        EarlyLeave,

        /// <summary>
        /// 缺勤
        /// </summary>
        Absent,

        /// <summary>
        /// 请假
        /// </summary>
        Leave,

        /// <summary>
        /// 出差
        /// </summary>
        BusinessTrip,

        /// <summary>
        /// 加班
        /// </summary>
        Overtime,

        /// <summary>
        /// 调休
        /// </summary>
        CompensatoryLeave,

        /// <summary>
        /// 异常
        /// </summary>
        Exception
    }

    /// <summary>
    /// 考勤异常类型
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttendanceExceptionType
    {
        /// <summary>
        /// 迟到
        /// </summary>
        Late,

        /// <summary>
        /// 早退
        /// </summary>
        EarlyLeave,

        /// <summary>
        /// 缺勤
        /// </summary>
        Absent,

        /// <summary>
        /// 未打卡
        /// </summary>
        NoCheckIn,

        /// <summary>
        /// 漏打卡
        /// </summary>
        MissCheckIn,

        /// <summary>
        /// 重复打卡
        /// </summary>
        DuplicateCheckIn,

        /// <summary>
        /// 时间异常
        /// </summary>
        TimeException,

        /// <summary>
        /// 地点异常
        /// </summary>
        LocationException
    }
}