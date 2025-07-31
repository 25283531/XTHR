using System;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 员工合同实体
    /// </summary>
    public class EmployeeContract
    {
        /// <summary>
        /// 合同ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 试用期（月）
        /// </summary>
        public int? ProbationPeriod { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 合同文件路径
        /// </summary>
        public string? ContractFilePath { get; set; }

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
        /// 员工实体
        /// </summary>
        public virtual Employee Employee { get; set; } = null!;
    }
}