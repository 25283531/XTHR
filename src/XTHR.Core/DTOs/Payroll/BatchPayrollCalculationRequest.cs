using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 批量薪资计算请求
    /// </summary>
    public class BatchPayrollCalculationRequest
    {
        /// <summary>
        /// 薪资期间ID
        /// </summary>
        [Required(ErrorMessage = "薪资期间ID不能为空")]
        public int PayrollPeriodId { get; set; }

        /// <summary>
        /// 员工ID列表，为空表示计算所有员工
        /// </summary>
        public List<int>? EmployeeIds { get; set; }

        /// <summary>
        /// 部门ID列表，为空表示计算所有部门
        /// </summary>
        public List<int>? DepartmentIds { get; set; }

        /// <summary>
        /// 是否重新计算已计算的数据
        /// </summary>
        public bool Recalculate { get; set; } = false;

        /// <summary>
        /// 计算类型：1-普通薪资，2-年终奖，3-离职补偿
        /// </summary>
        [Range(1, 3, ErrorMessage = "计算类型必须在1-3之间")]
        public int CalculationType { get; set; } = 1;

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string? OperatorName { get; set; }

        /// <summary>
        /// 计算描述
        /// </summary>
        [StringLength(200, ErrorMessage = "计算描述不能超过200个字符")]
        public string? Description { get; set; }
    }
}