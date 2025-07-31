using System;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 薪资基础数据实体
    /// </summary>
    public class SalaryBase
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
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// 员工实体
        /// </summary>
        public virtual Employee Employee { get; set; } = null!;
    }
}