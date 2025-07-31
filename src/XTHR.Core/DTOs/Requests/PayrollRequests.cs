using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 工资计算请求
    /// </summary>
    public class PayrollCalculationRequest
    {
        /// <summary>
        /// 员工ID列表（为空则计算所有员工）
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 工资期间
        /// </summary>
        [Required(ErrorMessage = "工资期间不能为空")]
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 是否重新计算（覆盖已有结果）
        /// </summary>
        public bool ForceRecalculate { get; set; } = false;
        
        /// <summary>
        /// 计算备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 单个员工工资计算请求
    /// </summary>
    public class SinglePayrollCalculationRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "员工ID必须大于0")]
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 工资期间
        /// </summary>
        [Required(ErrorMessage = "工资期间不能为空")]
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 是否重新计算
        /// </summary>
        public bool ForceRecalculate { get; set; } = false;
        
        /// <summary>
        /// 计算备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 工资查询请求
    /// </summary>
    public class PayrollQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// 工资期间
        /// </summary>
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 计算状态
        /// </summary>
        public string CalculationStatus { get; set; }
        
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; }
        
        /// <summary>
        /// 发放状态
        /// </summary>
        public string PaymentStatus { get; set; }
        
        /// <summary>
        /// 最小应发工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "最小应发工资不能小于0")]
        public decimal MinGrossPay { get; set; }
        
        /// <summary>
        /// 最大应发工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "最大应发工资不能小于0")]
        public decimal MaxGrossPay { get; set; }
        
        /// <summary>
        /// 最小实发工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "最小实发工资不能小于0")]
        public decimal MinNetPay { get; set; }
        
        /// <summary>
        /// 最大实发工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "最大实发工资不能小于0")]
        public decimal MaxNetPay { get; set; }
    }
    
    /// <summary>
    /// 工资审核请求
    /// </summary>
    public class PayrollApprovalRequest
    {
        /// <summary>
        /// 工资结果ID列表
        /// </summary>
        [Required(ErrorMessage = "工资结果ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一条工资记录")]
        public List<int> PayrollResultIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 审核状态（通过/拒绝）
        /// </summary>
        [Required(ErrorMessage = "审核状态不能为空")]
        [RegularExpression("^(通过|拒绝)$", ErrorMessage = "审核状态只能是'通过'或'拒绝'")]
        public string ApprovalStatus { get; set; }
        
        /// <summary>
        /// 审核意见
        /// </summary>
        [StringLength(1000, ErrorMessage = "审核意见长度不能超过1000字符")]
        public string ApprovalComments { get; set; }
    }
    
    /// <summary>
    /// 工资发放请求
    /// </summary>
    public class PayrollPaymentRequest
    {
        /// <summary>
        /// 工资结果ID列表
        /// </summary>
        [Required(ErrorMessage = "工资结果ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一条工资记录")]
        public List<int> PayrollResultIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 发放日期
        /// </summary>
        [Required(ErrorMessage = "发放日期不能为空")]
        public DateTime PaymentDate { get; set; }
        
        /// <summary>
        /// 发放方式
        /// </summary>
        [Required(ErrorMessage = "发放方式不能为空")]
        [StringLength(50, ErrorMessage = "发放方式长度不能超过50字符")]
        public string PaymentMethod { get; set; }
        
        /// <summary>
        /// 发放备注
        /// </summary>
        [StringLength(500, ErrorMessage = "发放备注长度不能超过500字符")]
        public string PaymentRemarks { get; set; }
    }
    
    /// <summary>
    /// 批量工资单生成请求
    /// </summary>
    public class BatchPayslipGenerationRequest
    {
        /// <summary>
        /// 员工ID列表
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new();

        /// <summary>
        /// 工资月份
        /// </summary>
        public string PayrollMonth { get; set; } = string.Empty;

        /// <summary>
        /// 是否包含已离职员工
        /// </summary>
        public bool IncludeResignedEmployees { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 工资基础信息更新请求
    /// </summary>
    public class UpdateSalaryBaseRequest
    {
        /// <summary>
        /// 工资基础ID
        /// </summary>
        [Required(ErrorMessage = "工资基础ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "工资基础ID必须大于0")]
        public int Id { get; set; }
        
        /// <summary>
        /// 基本工资
        /// </summary>
        [Required(ErrorMessage = "基本工资不能为空")]
        [Range(0, 999999.99, ErrorMessage = "基本工资必须在0-999999.99之间")]
        public decimal BasicSalary { get; set; }
        
        /// <summary>
        /// 岗位工资
        /// </summary>
        [Range(0, 999999.99, ErrorMessage = "岗位工资必须在0-999999.99之间")]
        public decimal PositionSalary { get; set; }
        
        /// <summary>
        /// 绩效工资
        /// </summary>
        [Range(0, 999999.99, ErrorMessage = "绩效工资必须在0-999999.99之间")]
        public decimal PerformanceSalary { get; set; }
        
        /// <summary>
        /// 津贴补助
        /// </summary>
        [Range(0, 999999.99, ErrorMessage = "津贴补助必须在0-999999.99之间")]
        public decimal Allowance { get; set; }
        
        /// <summary>
        /// 生效日期
        /// </summary>
        [Required(ErrorMessage = "生效日期不能为空")]
        public DateTime EffectiveDate { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 工资统计查询请求
    /// </summary>
    public class PayrollStatisticsRequest
    {
        /// <summary>
        /// 统计期间
        /// </summary>
        [Required(ErrorMessage = "统计期间不能为空")]
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 部门列表（为空则统计所有部门）
        /// </summary>
        public List<string> Departments { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否包含部门明细
        /// </summary>
        public bool IncludeDepartmentDetails { get; set; } = true;
        
        /// <summary>
        /// 统计类型
        /// </summary>
        [Required(ErrorMessage = "统计类型不能为空")]
        [RegularExpression("^(总体|部门|趋势|成本)$", ErrorMessage = "统计类型只能是'总体'、'部门'、'趋势'或'成本'")]
        public string StatisticsType { get; set; }
    }
    
    /// <summary>
    /// 工资趋势分析请求
    /// </summary>
    public class PayrollTrendAnalysisRequest
    {
        /// <summary>
        /// 开始年月
        /// </summary>
        [Required(ErrorMessage = "开始年月不能为空")]
        public DateTime StartPeriod { get; set; }
        
        /// <summary>
        /// 结束年月
        /// </summary>
        [Required(ErrorMessage = "结束年月不能为空")]
        public DateTime EndPeriod { get; set; }
        
        /// <summary>
        /// 分析维度
        /// </summary>
        [Required(ErrorMessage = "分析维度不能为空")]
        [RegularExpression("^(月度|季度|年度)$", ErrorMessage = "分析维度只能是'月度'、'季度'或'年度'")]
        public string AnalysisDimension { get; set; }
        
        /// <summary>
        /// 部门列表（为空则分析所有部门）
        /// </summary>
        public List<string> Departments { get; set; } = new List<string>();
        
        /// <summary>
        /// 分析指标
        /// </summary>
        public List<string> Metrics { get; set; } = new List<string> { "应发工资", "实发工资", "人均工资" };
    }
    
    /// <summary>
    /// 工资成本分析请求
    /// </summary>
    public class PayrollCostAnalysisRequest
    {
        /// <summary>
        /// 分析期间
        /// </summary>
        [Required(ErrorMessage = "分析期间不能为空")]
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 成本分析类型
        /// </summary>
        [Required(ErrorMessage = "成本分析类型不能为空")]
        [RegularExpression("^(人工成本|社保成本|税务成本|总成本)$", ErrorMessage = "成本分析类型只能是'人工成本'、'社保成本'、'税务成本'或'总成本'")]
        public string CostAnalysisType { get; set; }
        
        /// <summary>
        /// 部门列表（为空则分析所有部门）
        /// </summary>
        public List<string> Departments { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否包含明细
        /// </summary>
        public bool IncludeDetails { get; set; } = true;
    }
    
    /// <summary>
    /// 工资条生成请求
    /// </summary>
    public class PayslipGenerationRequest
    {
        /// <summary>
        /// 工资结果ID列表
        /// </summary>
        [Required(ErrorMessage = "工资结果ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一条工资记录")]
        public List<int> PayrollResultIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 生成格式
        /// </summary>
        [Required(ErrorMessage = "生成格式不能为空")]
        [RegularExpression("^(PDF|Excel|HTML)$", ErrorMessage = "生成格式只能是'PDF'、'Excel'或'HTML'")]
        public string Format { get; set; } = "PDF";
        
        /// <summary>
        /// 是否包含计算明细
        /// </summary>
        public bool IncludeCalculationDetails { get; set; } = false;
        
        /// <summary>
        /// 模板类型
        /// </summary>
        [StringLength(50, ErrorMessage = "模板类型长度不能超过50字符")]
        public string TemplateType { get; set; } = "标准模板";
    }
    
    /// <summary>
    /// 工资数据验证请求
    /// </summary>
    public class PayrollValidationRequest
    {
        /// <summary>
        /// 工资结果ID列表
        /// </summary>
        [Required(ErrorMessage = "工资结果ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一条工资记录")]
        public List<int> PayrollResultIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 验证类型
        /// </summary>
        [Required(ErrorMessage = "验证类型不能为空")]
        [RegularExpression("^(数据完整性|计算准确性|业务规则|全面验证)$", ErrorMessage = "验证类型只能是'数据完整性'、'计算准确性'、'业务规则'或'全面验证'")]
        public string ValidationType { get; set; }
        
        /// <summary>
        /// 验证规则
        /// </summary>
        public List<string> ValidationRules { get; set; } = new List<string>();
    }
}