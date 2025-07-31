using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 工资审核历史DTO
    /// </summary>
    public class PayrollApprovalHistoryDto
    {
        /// <summary>
        /// 审核历史ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 工资记录ID
        /// </summary>
        public int PayrollRecordId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public int ApproverId { get; set; }

        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string ApproverName { get; set; } = string.Empty;

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime ApprovalTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// 批量工资审核请求
    /// </summary>
    public class BatchPayrollApprovalRequest
    {
        /// <summary>
        /// 工资记录ID列表
        /// </summary>
        public List<int> PayrollRecordIds { get; set; } = new();

        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        public int ApproverId { get; set; }
    }

    /// <summary>
    /// 批量工资发放请求
    /// </summary>
    public class BatchPayrollPaymentRequest
    {
        /// <summary>
        /// 工资记录ID列表
        /// </summary>
        public List<int> PayrollRecordIds { get; set; } = new();

        /// <summary>
        /// 发放日期
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// 发放方式
        /// </summary>
        public string PaymentMethod { get; set; } = string.Empty;

        /// <summary>
        /// 发放备注
        /// </summary>
        public string? PaymentRemarks { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }
    }
}