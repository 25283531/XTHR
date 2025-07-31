using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Responses
{
    /// <summary>
    /// 考勤响应DTO
    /// </summary>
    public class AttendanceResponse
    {
        /// <summary>
        /// 响应状态
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 考勤数据
        /// </summary>
        public object Data { get; set; } = new();
    }

    /// <summary>
    /// 考勤统计响应DTO
    /// </summary>
    public class AttendanceStatisticsResponse
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
        /// 考勤月份
        /// </summary>
        public string Month { get; set; } = string.Empty;

        /// <summary>
        /// 应出勤天数
        /// </summary>
        public int ShouldAttendanceDays { get; set; }

        /// <summary>
        /// 实际出勤天数
        /// </summary>
        public int ActualAttendanceDays { get; set; }

        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 缺勤天数
        /// </summary>
        public int AbsentDays { get; set; }

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
    }

    /// <summary>
    /// 批量考勤响应DTO
    /// </summary>
    public class BatchAttendanceResponse
    {
        /// <summary>
        /// 成功数量
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// 失败数量
        /// </summary>
        public int FailureCount { get; set; }

        /// <summary>
        /// 失败详情
        /// </summary>
        public List<string> Failures { get; set; } = new();
    }
}