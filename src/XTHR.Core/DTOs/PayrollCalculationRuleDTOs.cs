using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 工资计算规则DTO
    /// </summary>
    public class PayrollCalculationRuleDto
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 规则类型
        /// </summary>
        public string RuleType { get; set; } = string.Empty;

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; } = string.Empty;

        /// <summary>
        /// 适用条件
        /// </summary>
        public string Condition { get; set; } = string.Empty;

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 创建工资计算规则请求
    /// </summary>
    public class CreatePayrollCalculationRuleRequest
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 规则类型
        /// </summary>
        public string RuleType { get; set; } = string.Empty;

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; } = string.Empty;

        /// <summary>
        /// 适用条件
        /// </summary>
        public string Condition { get; set; } = string.Empty;

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; } = true;
    }

    /// <summary>
    /// 更新工资计算规则请求
    /// </summary>
    public class UpdatePayrollCalculationRuleRequest
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 规则名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 规则描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 规则类型
        /// </summary>
        public string RuleType { get; set; } = string.Empty;

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; } = string.Empty;

        /// <summary>
        /// 适用条件
        /// </summary>
        public string Condition { get; set; } = string.Empty;

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }

    /// <summary>
    /// 工资规则测试数据
    /// </summary>
    public class PayrollRuleTestData
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
        /// 部门ID
        /// </summary>
        public int DepartmentId { get; set; }

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
        /// 考勤天数
        /// </summary>
        public decimal AttendanceDays { get; set; }

        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount { get; set; }

        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }

        /// <summary>
        /// 缺勤天数
        /// </summary>
        public decimal AbsenceDays { get; set; }

        /// <summary>
        /// 加班小时数
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 自定义字段
        /// </summary>
        public Dictionary<string, object> CustomFields { get; set; } = new Dictionary<string, object>();
    }

    /// <summary>
    /// 工资规则测试结果
    /// </summary>
    public class PayrollRuleTestResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 计算结果
        /// </summary>
        public decimal CalculatedAmount { get; set; }

        /// <summary>
        /// 计算详情
        /// </summary>
        public List<CalculationDetail> Details { get; set; } = new List<CalculationDetail>();

        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> ErrorMessages { get; set; } = new List<string>();

        /// <summary>
        /// 警告信息
        /// </summary>
        public List<string> WarningMessages { get; set; } = new List<string>();
    }

    /// <summary>
    /// 计算详情
    /// </summary>
    public class CalculationDetail
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 计算表达式
        /// </summary>
        public string Expression { get; set; } = string.Empty;

        /// <summary>
        /// 计算结果
        /// </summary>
        public decimal Result { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
    }
}