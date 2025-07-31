using System.Collections.Generic;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 批量更新员工状态请求DTO
    /// </summary>
    public class BatchUpdateEmployeeStatusRequest
    {
        /// <summary>
        /// 员工ID列表
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new();

        /// <summary>
        /// 新状态
        /// </summary>
        public string NewStatus { get; set; } = string.Empty;

        /// <summary>
        /// 更新原因
        /// </summary>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// 是否发送通知
        /// </summary>
        public bool SendNotification { get; set; } = true;
    }
}