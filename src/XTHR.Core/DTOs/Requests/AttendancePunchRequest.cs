using System;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 考勤打卡请求
    /// </summary>
    public class AttendancePunchRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 打卡类型（上班/下班）
        /// </summary>
        public string PunchType { get; set; } = string.Empty;

        /// <summary>
        /// 打卡时间
        /// </summary>
        public DateTime PunchTime { get; set; }

        /// <summary>
        /// 打卡位置（GPS坐标）
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 打卡设备
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 图片证据（base64编码）
        /// </summary>
        public string PhotoEvidence { get; set;}
    }
}