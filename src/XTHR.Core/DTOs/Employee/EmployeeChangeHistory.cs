using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工变更历史记录DTO
    /// </summary>
    public class EmployeeChangeHistory
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 变更前值
        /// </summary>
        public string OldValue { get; set; }

        /// <summary>
        /// 变更后值
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}