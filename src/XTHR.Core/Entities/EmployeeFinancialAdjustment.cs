using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    public enum AdjustmentType
    {
        Bonus,      // 奖金
        Deduction   // 扣款
    }

    /// <summary>
    /// 员工财务调整（奖金/扣款）
    /// </summary>
    public class EmployeeFinancialAdjustment : BaseEntity<int>
    {
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        [Required]
        public AdjustmentType Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 调整发生的月份
        /// </summary>
        [Required]
        public DateTime AdjustmentPeriod { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}