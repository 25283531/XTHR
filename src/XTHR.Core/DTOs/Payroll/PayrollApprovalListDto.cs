using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 工资审批列表DTO
    /// </summary>
    public class PayrollApprovalListDto
    {
        /// <summary>
        /// 工资结果ID
        /// </summary>
        public int PayrollResultId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 工资月份
        /// </summary>
        public DateTime PayrollMonth { get; set; }

        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetSalary { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmitTime { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string Submitter { get; set; } = string.Empty;

        /// <summary>
        /// 当前审批人
        /// </summary>
        public string? CurrentApprover { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}