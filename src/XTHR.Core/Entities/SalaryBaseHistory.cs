using System;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 薪资基数历史记录实体
    /// </summary>
    public class SalaryBaseHistory : BaseEntity<int>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工实体
        /// </summary>
        public virtual Employee Employee { get; set; } = null!;

        /// <summary>
        /// 原薪资基数
        /// </summary>
        public decimal OriginalSalaryBase { get; set; }

        /// <summary>
        /// 新薪资基数
        /// </summary>
        public decimal NewSalaryBase { get; set; }

        /// <summary>
        /// 变更类型（增加/减少/调整）
        /// </summary>
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string ChangeReason { get; set; } = string.Empty;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 变更前月薪
        /// </summary>
        public decimal OriginalMonthlySalary { get; set; }

        /// <summary>
        /// 变更后月薪
        /// </summary>
        public decimal NewMonthlySalary { get; set; }

        /// <summary>
        /// 变更幅度
        /// </summary>
        public decimal ChangeAmount { get; set; }

        /// <summary>
        /// 变更比例
        /// </summary>
        public decimal ChangePercentage { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>
        /// 审批人ID
        /// </summary>
        public int? ApproverId { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public virtual Employee? Approver { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 是否已生效
        /// </summary>
        public bool IsEffective { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual Employee Creator { get; set; } = null!;

        /// <summary>
        /// 最后更新人
        /// </summary>
        public virtual Employee? Updater { get; set; }
    }
}