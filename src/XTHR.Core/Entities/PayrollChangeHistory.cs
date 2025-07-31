using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 工资变更历史实体
    /// </summary>
    public class PayrollChangeHistory
    {
        /// <summary>
        /// 变更历史ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        [MaxLength(50)]
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变更前值
        /// </summary>
        [MaxLength(100)]
        public string OldValue { get; set; } = string.Empty;

        /// <summary>
        /// 变更后值
        /// </summary>
        [MaxLength(100)]
        public string NewValue { get; set; } = string.Empty;

        /// <summary>
        /// 变更描述
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 变更人
        /// </summary>
        [MaxLength(50)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remarks { get; set; } = string.Empty;
    }

    /// <summary>
    /// 工资结果实体（用于区分DTO和实体）
    /// </summary>
    public class PayrollResultEntity
    {
        /// <summary>
        /// 工资结果ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 工资月份
        /// </summary>
        public DateTime PayrollMonth { get; set; }

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
        /// 加班费
        /// </summary>
        public decimal OvertimePay { get; set; }

        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// 社保扣除
        /// </summary>
        public decimal SocialInsurance { get; set; }

        /// <summary>
        /// 公积金扣除
        /// </summary>
        public decimal HousingFund { get; set; }

        /// <summary>
        /// 个税扣除
        /// </summary>
        public decimal IncomeTax { get; set; }

        /// <summary>
        /// 其他扣除
        /// </summary>
        public decimal OtherDeductions { get; set; }

        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetSalary { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}