using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 批量更新员工部门请求
    /// </summary>
    public class BatchUpdateEmployeeDepartmentRequest
    {
        /// <summary>
        /// 员工ID列表
        /// </summary>
        [Required(ErrorMessage = "员工ID列表不能为空")]
        public List<int> EmployeeIds { get; set; } = new();

        /// <summary>
        /// 新部门ID
        /// </summary>
        [Required(ErrorMessage = "新部门ID不能为空")]
        public int NewDepartmentId { get; set; }

        /// <summary>
        /// 新职位ID
        /// </summary>
        public int? NewPositionId { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        [StringLength(500, ErrorMessage = "变更原因不能超过500个字符")]
        public string? Reason { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public System.DateTime EffectiveDate { get; set; } = System.DateTime.Now;

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string? OperatorName { get; set; }
    }
}