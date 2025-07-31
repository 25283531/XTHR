using System;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 更新薪资基础数据请求
    /// </summary>
    public class UpdatePayrollBaseRequest
    {
        /// <summary>
        /// 薪资基础ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PositionSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴补贴
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// 社保基数
        /// </summary>
        public decimal SocialSecurityBase { get; set; }

        /// <summary>
        /// 公积金基数
        /// </summary>
        public decimal ProvidentFundBase { get; set; }

        /// <summary>
        /// 个税起征点
        /// </summary>
        public decimal TaxThreshold { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatedBy { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatedByName { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; }
    }
}