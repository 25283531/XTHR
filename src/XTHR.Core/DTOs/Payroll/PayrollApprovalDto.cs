using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 薪资审批DTO
    /// </summary>
    public class PayrollApprovalDto
    {
        /// <summary>
        /// 审批ID
        /// </summary>
        public int ApprovalId { get; set; }

        /// <summary>
        /// 薪资结果ID
        /// </summary>
        public int PayrollResultId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 薪资期间
        /// </summary>
        public string PayrollPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 应发薪资
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// 实发薪资
        /// </summary>
        public decimal NetSalary { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApprovedBy { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovedAt { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmittedAt { get; set; }
    }
}