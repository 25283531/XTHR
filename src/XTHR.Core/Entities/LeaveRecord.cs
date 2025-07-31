using System;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 请假记录
    /// </summary>
    public class LeaveRecord : BaseEntity<int>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        public string LeaveType { get; set; } = string.Empty;

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        public decimal Days { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 审批人ID
        /// </summary>
        public int? ApproverId { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 关联员工
        /// </summary>
        public virtual Employee Employee { get; set; } = null!;
    }
}