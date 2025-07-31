using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 工资模板DTO
    /// </summary>
    public class PayrollTemplateDto
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 工资项目列表
        /// </summary>
        public List<PayrollTemplateItemDto> Items { get; set; } = new List<PayrollTemplateItemDto>();

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// 工资模板项目DTO
    /// </summary>
    public class PayrollTemplateItemDto
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 项目类型（收入/扣款）
        /// </summary>
        public string ItemType { get; set; } = string.Empty;

        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; set; } = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; } = true;
    }

    /// <summary>
    /// 创建工资模板请求
    /// </summary>
    public class CreatePayrollTemplateRequest
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 模板描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 工资项目列表
        /// </summary>
        public List<PayrollTemplateItemDto> Items { get; set; } = new List<PayrollTemplateItemDto>();
    }

    /// <summary>
    /// 工资异常检查请求
    /// </summary>
    public class PayrollAnomalyCheckRequest
    {
        /// <summary>
        /// 员工ID列表
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();

        /// <summary>
        /// 工资期间
        /// </summary>
        public PayrollPeriod Period { get; set; } = new PayrollPeriod();

        /// <summary>
        /// 异常检查规则
        /// </summary>
        public List<string> CheckRules { get; set; } = new List<string>();
    }

    /// <summary>
    /// 工资异常检查结果
    /// </summary>
    public class PayrollAnomalyCheckResult
    {
        /// <summary>
        /// 异常列表
        /// </summary>
        public List<PayrollAnomalyDto> Anomalies { get; set; } = new List<PayrollAnomalyDto>();

        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime CheckTime { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// 工资异常DTO
    /// </summary>
    public class PayrollAnomalyDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 异常类型
        /// </summary>
        public string AnomalyType { get; set; } = string.Empty;

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 异常值
        /// </summary>
        public decimal AnomalyValue { get; set; }

        /// <summary>
        /// 参考值
        /// </summary>
        public decimal ReferenceValue { get; set; }
    }
}