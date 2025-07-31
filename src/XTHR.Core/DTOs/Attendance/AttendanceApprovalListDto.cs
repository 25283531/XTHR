using System;

namespace XTHR.Core.DTOs.Attendance
{
    /// <summary>
    /// 考勤审批列表DTO
    /// </summary>
    public class AttendanceApprovalListDto
    {
        /// <summary>
        /// 审批ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 申请类型：1-补卡，2-请假，3-加班，4-出差
        /// </summary>
        public int ApplicationType { get; set; }

        /// <summary>
        /// 申请类型名称
        /// </summary>
        public string ApplicationTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 时长（小时）
        /// </summary>
        public decimal Duration { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 状态：0-待审批，1-已批准，2-已拒绝，3-已撤销
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// 申请人ID
        /// </summary>
        public int ApplicantId { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; } = string.Empty;

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplicationTime { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        public int? ApproverId { get; set; }

        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string? ApproverName { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }
    }
}