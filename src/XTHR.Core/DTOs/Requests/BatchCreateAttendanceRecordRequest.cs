using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 批量创建考勤记录请求
    /// </summary>
    public class BatchCreateAttendanceRecordRequest
    {
        /// <summary>
        /// 考勤记录列表
        /// </summary>
        [Required(ErrorMessage = "考勤记录列表不能为空")]
        [MinLength(1, ErrorMessage = "至少创建一条考勤记录")]
        public List<CreateAttendanceRecordRequest> AttendanceRecords { get; set; } = new List<CreateAttendanceRecordRequest>();

        /// <summary>
        /// 是否跳过重复记录
        /// </summary>
        public bool SkipDuplicates { get; set; } = true;

        /// <summary>
        /// 是否自动计算工作时长
        /// </summary>
        public bool AutoCalculateWorkHours { get; set; } = true;

        /// <summary>
        /// 是否自动判断异常
        /// </summary>
        public bool AutoDetectExceptions { get; set; } = true;
    }
}