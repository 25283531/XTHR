namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤打卡类型
    /// </summary>
    public enum AttendancePunchType
    {
        /// <summary>
        /// 上班打卡
        /// </summary>
        ClockIn = 1,

        /// <summary>
        /// 下班打卡
        /// </summary>
        ClockOut = 2,

        /// <summary>
        /// 外出打卡
        /// </summary>
        OutPunch = 3,

        /// <summary>
        /// 返回打卡
        /// </summary>
        ReturnPunch = 4,

        /// <summary>
        /// 加班开始
        /// </summary>
        OvertimeStart = 5,

        /// <summary>
        /// 加班结束
        /// </summary>
        OvertimeEnd = 6
    }
}