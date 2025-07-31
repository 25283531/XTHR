using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Payroll;
using XTHR.Core.DTOs.Requests;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Services
{
    /// <summary>
    /// 工资管理服务接口
    /// </summary>
    public interface IPayrollService : IBaseService<PayrollResult, int, PayrollResultDetailDto, PayrollResultListDto, PayrollCalculationRequest, UpdatePayrollBaseRequest>
    {
        #region 工资计算
        
        /// <summary>
        /// 计算单个员工工资
        /// </summary>
        /// <param name="request">工资计算请求</param>
        /// <returns>计算结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollCalculationResult>> CalculatePayrollAsync(PayrollCalculationRequest request);
        
        /// <summary>
        /// 批量计算员工工资
        /// </summary>
        /// <param name="request">批量计算请求</param>
        /// <returns>批量计算结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchPayrollCalculationResult>> BatchCalculatePayrollAsync(BatchPayrollCalculationRequest request);
        
        /// <summary>
        /// 重新计算工资
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <param name="reason">重新计算原因</param>
        /// <returns>重新计算结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollCalculationResult>> RecalculatePayrollAsync(int payrollResultId, string reason = "");
        
        /// <summary>
        /// 预览工资计算结果
        /// </summary>
        /// <param name="request">工资计算请求</param>
        /// <returns>预览结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollCalculationResult>> PreviewPayrollCalculationAsync(PayrollCalculationRequest request);
        
        /// <summary>
        /// 验证工资计算数据
        /// </summary>
        /// <param name="request">验证请求</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollValidationResult>> ValidatePayrollDataAsync(PayrollValidationRequest request);
        
        #endregion
        
        #region 工资查询
        
        /// <summary>
        /// 查询工资结果
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>查询结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<PayrollResultListDto>>> QueryPayrollResultsAsync(PayrollQueryRequest request);
        
        /// <summary>
        /// 获取员工工资历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>工资历史</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollHistoryDto>>> GetEmployeePayrollHistoryAsync(int employeeId, DateTime startDate = default, DateTime endDate = default);
        
        /// <summary>
        /// 获取部门工资汇总
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>部门工资汇总</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<DepartmentPayrollSummaryDto>> GetDepartmentPayrollSummaryAsync(int departmentId, int year, int month);
        
        /// <summary>
        /// 获取工资期间列表
        /// </summary>
        /// <param name="year">年份（可选）</param>
        /// <returns>工资期间列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollPeriodDto>>> GetPayrollPeriodsAsync(int year = 0);
        
        #endregion
        
        #region 工资基础信息管理
        
        /// <summary>
        /// 获取员工工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollBaseDetailDto>> GetPayrollBaseAsync(int employeeId);
        
        /// <summary>
        /// 创建员工工资基础信息
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollBaseDetailDto>> CreatePayrollBaseAsync(CreatePayrollBaseRequest request);
        
        /// <summary>
        /// 更新员工工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollBaseDetailDto>> UpdatePayrollBaseAsync(int employeeId, UpdatePayrollBaseRequest request);
        
        /// <summary>
        /// 获取工资基础信息变更历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>变更历史</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollBaseChangeHistoryDto>>> GetPayrollBaseHistoryAsync(int employeeId);
        
        /// <summary>
        /// 批量更新工资基础信息
        /// </summary>
        /// <param name="request">批量更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchUpdateResult>> BatchUpdatePayrollBaseAsync(BatchUpdatePayrollBaseRequest request);
        
        #endregion
        
        #region 工资审核
        
        /// <summary>
        /// 提交工资审核
        /// </summary>
        /// <param name="request">审核请求</param>
        /// <returns>提交结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SubmitPayrollForApprovalAsync(PayrollApprovalRequest request);
        
        /// <summary>
        /// 审批工资
        /// </summary>
        /// <param name="request">审批请求</param>
        /// <returns>审批结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ApprovePayrollAsync(PayrollApprovalRequest request);
        
        /// <summary>
        /// 拒绝工资审批
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <param name="reason">拒绝原因</param>
        /// <returns>拒绝结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> RejectPayrollAsync(int payrollResultId, string reason);
        
        /// <summary>
        /// 获取待审核工资列表
        /// </summary>
        /// <param name="approverId">审批人ID（可选）</param>
        /// <returns>待审核列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollApprovalListDto>>> GetPendingApprovalsAsync(int approverId = 0);
        
        /// <summary>
        /// 获取工资审核历史
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <returns>审核历史</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollApprovalHistoryDto>>> GetApprovalHistoryAsync(int payrollResultId);
        
        /// <summary>
        /// 批量审批工资
        /// </summary>
        /// <param name="request">批量审批请求</param>
        /// <returns>审批结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchApprovalResult>> BatchApprovePayrollAsync(BatchPayrollApprovalRequest request);
        
        #endregion
        
        #region 工资发放
        
        /// <summary>
        /// 标记工资已发放
        /// </summary>
        /// <param name="request">发放请求</param>
        /// <returns>发放结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> MarkPayrollAsPaidAsync(PayrollPaymentRequest request);
        
        /// <summary>
        /// 批量标记工资已发放
        /// </summary>
        /// <param name="request">批量发放请求</param>
        /// <returns>发放结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchPaymentResult>> BatchMarkPayrollAsPaidAsync(BatchPayrollPaymentRequest request);
        
        /// <summary>
        /// 生成工资条
        /// </summary>
        /// <param name="request">工资条生成请求</param>
        /// <returns>工资条</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayslipDto>> GeneratePayslipAsync(PayslipGenerationRequest request);
        
        /// <summary>
        /// 批量生成工资条
        /// </summary>
        /// <param name="request">批量生成请求</param>
        /// <returns>生成结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchPayslipGenerationResult>> BatchGeneratePayslipsAsync(BatchPayslipGenerationRequest request);
        
        /// <summary>
        /// 发送工资条
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <param name="sendMethod">发送方式</param>
        /// <returns>发送结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SendPayslipAsync(int payrollResultId, PayslipSendMethod sendMethod);
        
        #endregion
        
        #region 工资统计分析
        
        /// <summary>
        /// 获取工资汇总统计
        /// </summary>
        /// <param name="request">统计请求</param>
        /// <returns>汇总统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollSummaryStatistics>> GetPayrollSummaryStatisticsAsync(PayrollStatisticsRequest request);
        
        /// <summary>
        /// 获取部门工资统计
        /// </summary>
        /// <param name="request">统计请求</param>
        /// <returns>部门统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<XTHR.Core.DTOs.Payroll.DepartmentPayrollStatisticsDto>>> GetDepartmentPayrollStatisticsAsync(PayrollStatisticsRequest request);
        
        /// <summary>
        /// 获取工资成本分析
        /// </summary>
        /// <param name="request">分析请求</param>
        /// <returns>成本分析</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollCostAnalysisDto>> GetPayrollCostAnalysisAsync(PayrollCostAnalysisRequest request);
        
        /// <summary>
        /// 获取工资趋势分析
        /// </summary>
        /// <param name="request">趋势分析请求</param>
        /// <returns>趋势分析</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollTrendAnalysisDto>> GetPayrollTrendAnalysisAsync(PayrollTrendAnalysisRequest request);
        
        /// <summary>
        /// 获取工资分布分析
        /// </summary>
        /// <param name="request">分布分析请求</param>
        /// <returns>分布分析</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollDistributionAnalysisDto>> GetPayrollDistributionAnalysisAsync(PayrollDistributionAnalysisRequest request);
        
        #endregion
        
        #region 工资报表
        
        /// <summary>
        /// 生成工资明细报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>明细报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollDetailReportDto>> GeneratePayrollDetailReportAsync(PayrollReportRequest request);
        
        /// <summary>
        /// 生成工资汇总报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>汇总报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollSummaryReportDto>> GeneratePayrollSummaryReportAsync(PayrollReportRequest request);
        
        /// <summary>
        /// 生成部门工资报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>部门报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<DepartmentPayrollReportDto>> GenerateDepartmentPayrollReportAsync(PayrollReportRequest request);
        
        /// <summary>
        /// 生成工资统计报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>统计报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollStatisticsReportDto>> GeneratePayrollStatisticsReportAsync(PayrollReportRequest request);
        
        /// <summary>
        /// 导出工资数据
        /// </summary>
        /// <param name="request">导出请求</param>
        /// <returns>导出结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<PayrollResultListDto>>> ExportPayrollDataAsync(PayrollExportRequest request);
        
        #endregion
        
        #region 工资规则管理
        
        /// <summary>
        /// 获取工资计算规则
        /// </summary>
        /// <param name="ruleType">规则类型</param>
        /// <returns>计算规则</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollCalculationRuleDto>>> GetPayrollCalculationRulesAsync(PayrollRuleType? ruleType = null);
        
        /// <summary>
        /// 创建工资计算规则
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollCalculationRuleDto>> CreatePayrollCalculationRuleAsync(CreatePayrollCalculationRuleRequest request);
        
        /// <summary>
        /// 更新工资计算规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollCalculationRuleDto>> UpdatePayrollCalculationRuleAsync(int ruleId, UpdatePayrollCalculationRuleRequest request);
        
        /// <summary>
        /// 删除工资计算规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> DeletePayrollCalculationRuleAsync(int ruleId);
        
        /// <summary>
        /// 测试工资计算规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <param name="testData">测试数据</param>
        /// <returns>测试结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollRuleTestResult>> TestPayrollCalculationRuleAsync(int ruleId, PayrollRuleTestData testData);
        
        #endregion
        
        #region 工资模板管理
        
        /// <summary>
        /// 获取工资模板列表
        /// </summary>
        /// <returns>模板列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<PayrollTemplateDto>>> GetPayrollTemplatesAsync();
        
        /// <summary>
        /// 创建工资模板
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollTemplateDto>> CreatePayrollTemplateAsync(CreatePayrollTemplateRequest request);
        
        /// <summary>
        /// 应用工资模板
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="employeeIds">员工ID列表</param>
        /// <returns>应用结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchUpdateResult>> ApplyPayrollTemplateAsync(int templateId, IEnumerable<int> employeeIds);
        
        #endregion
        
        #region 工资数据验证
        
        /// <summary>
        /// 验证工资数据完整性
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollDataIntegrityResult>> ValidatePayrollDataIntegrityAsync(int year, int month);
        
        /// <summary>
        /// 检查工资计算异常
        /// </summary>
        /// <param name="request">检查请求</param>
        /// <returns>异常检查结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<PayrollAnomalyCheckResult>> CheckPayrollAnomaliesAsync(PayrollAnomalyCheckRequest request);
        
        /// <summary>
        /// 修复工资数据异常
        /// </summary>
        /// <param name="anomalyId">异常ID</param>
        /// <param name="fixAction">修复操作</param>
        /// <returns>修复结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> FixPayrollAnomalyAsync(int anomalyId, PayrollAnomalyFixAction fixAction);
        
        #endregion
    }
    
    /// <summary>
    /// 工资条发送方式
    /// </summary>
    public enum PayslipSendMethod
    {
        /// <summary>
        /// 邮件
        /// </summary>
        Email,
        
        /// <summary>
        /// 短信
        /// </summary>
        SMS,
        
        /// <summary>
        /// 系统消息
        /// </summary>
        SystemMessage,
        
        /// <summary>
        /// 微信
        /// </summary>
        WeChat
    }
    
    /// <summary>
    /// 工资规则类型
    /// </summary>
    public enum PayrollRuleType
    {
        /// <summary>
        /// 基本工资计算
        /// </summary>
        BasicSalary,
        
        /// <summary>
        /// 津贴计算
        /// </summary>
        Allowance,
        
        /// <summary>
        /// 奖金计算
        /// </summary>
        Bonus,
        
        /// <summary>
        /// 加班费计算
        /// </summary>
        Overtime,
        
        /// <summary>
        /// 扣款计算
        /// </summary>
        Deduction,
        
        /// <summary>
        /// 税费计算
        /// </summary>
        Tax,
        
        /// <summary>
        /// 社保计算
        /// </summary>
        SocialInsurance,
        
        /// <summary>
        /// 公积金计算
        /// </summary>
        HousingFund
    }
    
    /// <summary>
    /// 工资异常修复操作
    /// </summary>
    public enum PayrollAnomalyFixAction
    {
        /// <summary>
        /// 重新计算
        /// </summary>
        Recalculate,
        
        /// <summary>
        /// 手动修正
        /// </summary>
        ManualCorrection,
        
        /// <summary>
        /// 忽略异常
        /// </summary>
        Ignore,
        
        /// <summary>
        /// 删除记录
        /// </summary>
        Delete
    }
    
    /// <summary>
    /// 批量审批结果
    /// </summary>
    public class BatchApprovalResult
    {
        /// <summary>
        /// 审批是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功审批数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败审批数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<BatchApprovalFailure> Failures { get; set; } = new List<BatchApprovalFailure>();
    }
    
    /// <summary>
    /// 批量审批失败记录
    /// </summary>
    public class BatchApprovalFailure
    {
        /// <summary>
        /// 工资结果ID
        /// </summary>
        public int PayrollResultId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 批量发放结果
    /// </summary>
    public class BatchPaymentResult
    {
        /// <summary>
        /// 发放是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功发放数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败发放数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<BatchPaymentFailure> Failures { get; set; } = new List<BatchPaymentFailure>();
    }
    
    /// <summary>
    /// 批量发放失败记录
    /// </summary>
    public class BatchPaymentFailure
    {
        /// <summary>
        /// 工资结果ID
        /// </summary>
        public int PayrollResultId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 批量工资条生成结果
    /// </summary>
    public class BatchPayslipGenerationResult
    {
        /// <summary>
        /// 生成是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功生成数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败生成数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 生成的工资条
        /// </summary>
        public List<PayslipDto> Payslips { get; set; } = new List<PayslipDto>();
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<BatchPayslipGenerationFailure> Failures { get; set; } = new List<BatchPayslipGenerationFailure>();
    }
    
    /// <summary>
    /// 批量工资条生成失败记录
    /// </summary>
    public class BatchPayslipGenerationFailure
    {
        /// <summary>
        /// 工资结果ID
        /// </summary>
        public int PayrollResultId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 工资验证结果
    /// </summary>
    public class PayrollValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 验证错误
        /// </summary>
        public List<PayrollValidationError> Errors { get; set; } = new List<PayrollValidationError>();
        
        /// <summary>
        /// 验证警告
        /// </summary>
        public List<PayrollValidationWarning> Warnings { get; set; } = new List<PayrollValidationWarning>();
    }
    
    /// <summary>
    /// 工资验证错误
    /// </summary>
    public class PayrollValidationError
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 错误类型
        /// </summary>
        public string ErrorType { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
    }
    
    /// <summary>
    /// 工资验证警告
    /// </summary>
    public class PayrollValidationWarning
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 警告类型
        /// </summary>
        public string WarningType { get; set; }
        
        /// <summary>
        /// 警告信息
        /// </summary>
        public string WarningMessage { get; set; }
    }
    
    /// <summary>
    /// 工资数据完整性结果
    /// </summary>
    public class PayrollDataIntegrityResult
    {
        /// <summary>
        /// 数据是否完整
        /// </summary>
        public bool IsIntegrity { get; set; }
        
        /// <summary>
        /// 检查的员工总数
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// 有工资数据的员工数
        /// </summary>
        public int EmployeesWithPayroll { get; set; }
        
        /// <summary>
        /// 缺失工资数据的员工
        /// </summary>
        public List<EmployeeMissingPayrollDto> MissingPayrollEmployees { get; set; } = new List<EmployeeMissingPayrollDto>();
        
        /// <summary>
        /// 数据异常的员工
        /// </summary>
        public List<EmployeePayrollAnomalyDto> AnomalyEmployees { get; set; } = new List<EmployeePayrollAnomalyDto>();
    }
    
    /// <summary>
    /// 缺失工资数据的员工
    /// </summary>
    public class EmployeeMissingPayrollDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        
        /// <summary>
        /// 缺失原因
        /// </summary>
        public string MissingReason { get; set; }
    }
    
    /// <summary>
    /// 工资异常员工
    /// </summary>
    public class EmployeePayrollAnomalyDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 异常类型
        /// </summary>
        public string AnomalyType { get; set; }
        
        /// <summary>
        /// 异常描述
        /// </summary>
        public string AnomalyDescription { get; set; }
        
        /// <summary>
        /// 异常值
        /// </summary>
        public decimal AnomalyValue { get; set; }
    }
    
    /// <summary>
    /// 工资异常检查结果
    /// </summary>
    public class PayrollAnomalyCheckResult
    {
        /// <summary>
        /// 是否有异常
        /// </summary>
        public bool HasAnomalies { get; set; }
        
        /// <summary>
        /// 异常总数
        /// </summary>
        public int TotalAnomalies { get; set; }
        
        /// <summary>
        /// 异常详情
        /// </summary>
        public List<PayrollAnomalyDto> Anomalies { get; set; } = new List<PayrollAnomalyDto>();
    }
    
    /// <summary>
    /// 工资异常
    /// </summary>
    public class PayrollAnomalyDto
    {
        /// <summary>
        /// 异常ID
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
        /// 异常类型
        /// </summary>
        public string AnomalyType { get; set; }
        
        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 严重级别
        /// </summary>
        public AnomalySeverity Severity { get; set; }
        
        /// <summary>
        /// 发现时间
        /// </summary>
        public DateTime DetectedAt { get; set; }
        
        /// <summary>
        /// 是否已修复
        /// </summary>
        public bool IsFixed { get; set; }
    }
    
    /// <summary>
    /// 异常严重级别
    /// </summary>
    public enum AnomalySeverity
    {
        /// <summary>
        /// 低
        /// </summary>
        Low,
        
        /// <summary>
        /// 中
        /// </summary>
        Medium,
        
        /// <summary>
        /// 高
        /// </summary>
        High,
        
        /// <summary>
        /// 严重
        /// </summary>
        Critical
    }
}