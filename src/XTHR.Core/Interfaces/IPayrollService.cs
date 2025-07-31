using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Models;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Payroll;
using XTHR.Core.DTOs.Requests;
using XTHR.Core.DTOs.Responses;

namespace XTHR.Core.Interfaces
{
    /// <summary>
    /// 工资管理服务接口
    /// 提供工资计算、核算和管理的业务逻辑
    /// </summary>
    public interface IPayrollService
    {
        #region 工资计算
        
        /// <summary>
        /// 计算单个员工工资
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="payrollPeriod">工资期间</param>
        /// <returns>工资计算结果</returns>
        Task<PayrollCalculationResult> CalculateEmployeePayrollAsync(int employeeId, XTHR.Core.DTOs.Payroll.PayrollPeriod payrollPeriod);
        
        /// <summary>
        /// 批量计算工资
        /// </summary>
        /// <param name="request">批量计算请求</param>
        /// <returns>批量计算结果</returns>
        Task<BatchPayrollCalculationResult> BatchCalculatePayrollAsync(BatchPayrollCalculationRequest request);
        
        /// <summary>
        /// 重新计算工资
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <returns>重新计算结果</returns>
        Task<PayrollCalculationResult> RecalculatePayrollAsync(int payrollResultId);
        
        #endregion
        
        #region 工资结果管理
        
        /// <summary>
        /// 获取工资结果详情
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <returns>工资结果详情</returns>
        Task<PayrollResultDetailDto> GetPayrollResultDetailAsync(int payrollResultId);
        
        /// <summary>
        /// 获取员工工资历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>工资历史列表</returns>
        Task<List<PayrollHistoryDto>> GetEmployeePayrollHistoryAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);
        
        /// <summary>
        /// 获取工资结果列表（分页）
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页工资结果</returns>
        Task<PagedResult<PayrollResultListDto>> GetPayrollResultListAsync(PayrollQueryRequest request);
        
        /// <summary>
        /// 更新工资结果
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<ServiceResult> UpdatePayrollResultAsync(int payrollResultId, UpdatePayrollBaseRequest request);
        
        /// <summary>
        /// 删除工资结果
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <returns>删除结果</returns>
        Task<ServiceResult> DeletePayrollResultAsync(int payrollResultId);
        
        #endregion
        
        #region 工资基础管理
        
        /// <summary>
        /// 获取员工工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="effectiveDate">生效日期</param>
        /// <returns>工资基础信息</returns>
        Task<SalaryBaseDetailDto> GetEmployeeSalaryBaseAsync(int employeeId, DateTime? effectiveDate = null);
        
        /// <summary>
        /// 创建工资基础信息
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<ServiceResult<int>> CreateSalaryBaseAsync(CreateSalaryBaseRequest request);
        
        /// <summary>
        /// 更新工资基础信息
        /// </summary>
        /// <param name="salaryBaseId">工资基础ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<ServiceResult> UpdateSalaryBaseAsync(int salaryBaseId, XTHR.Core.DTOs.Payroll.UpdateSalaryBaseRequest request);
        
        /// <summary>
        /// 获取工资基础变更历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>变更历史</returns>
        Task<List<SalaryBaseHistoryDto>> GetSalaryBaseHistoryAsync(int employeeId);
        
        #endregion
        
        #region 工资统计分析
        
        /// <summary>
        /// 获取工资统计信息
        /// </summary>
        /// <param name="period">统计期间</param>
        /// <returns>工资统计</returns>
        Task<XTHR.Core.DTOs.Payroll.PayrollStatisticsDto> GetPayrollStatisticsAsync(XTHR.Core.DTOs.Payroll.PayrollPeriod period);
        
        /// <summary>
        /// 获取部门工资统计
        /// </summary>
        /// <param name="period">统计期间</param>
        /// <returns>部门工资统计</returns>
        Task<List<XTHR.Core.DTOs.Payroll.DepartmentPayrollStatisticsDto>> GetDepartmentPayrollStatisticsAsync(XTHR.Core.DTOs.Payroll.PayrollPeriod period);
        
        /// <summary>
        /// 获取工资成本分析
        /// </summary>
        /// <param name="request">分析请求</param>
        /// <returns>成本分析结果</returns>
        Task<PayrollCostAnalysisDto> GetPayrollCostAnalysisAsync(PayrollCostAnalysisRequest request);
        
        /// <summary>
        /// 获取工资趋势分析
        /// </summary>
        /// <param name="employeeId">员工ID（可选）</param>
        /// <param name="months">分析月数</param>
        /// <returns>趋势分析</returns>
        Task<PayrollTrendAnalysisDto> GetPayrollTrendAnalysisAsync(int? employeeId = null, int months = 12);
        
        #endregion
        
        #region 工资审核
        
        /// <summary>
        /// 提交工资审核
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <returns>提交结果</returns>
        Task<ServiceResult> SubmitPayrollForApprovalAsync(List<int> payrollResultIds);
        
        /// <summary>
        /// 审核工资
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <param name="approved">是否通过</param>
        /// <param name="comments">审核意见</param>
        /// <returns>审核结果</returns>
        Task<ServiceResult> ApprovePayrollAsync(List<int> payrollResultIds, bool approved, string? comments = null);
        
        /// <summary>
        /// 获取待审核工资列表
        /// </summary>
        /// <returns>待审核工资列表</returns>
        Task<List<PayrollApprovalDto>> GetPendingApprovalPayrollsAsync();
        
        #endregion
        
        #region 工资发放
        
        /// <summary>
        /// 标记工资已发放
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <param name="paymentDate">发放日期</param>
        /// <param name="paymentMethod">发放方式</param>
        /// <returns>标记结果</returns>
        Task<ServiceResult> MarkPayrollAsPaidAsync(List<int> payrollResultIds, DateTime paymentDate, string paymentMethod);
        
        /// <summary>
        /// 生成工资条
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <returns>工资条数据</returns>
        Task<PayslipDto> GeneratePayslipAsync(int payrollResultId);
        
        /// <summary>
        /// 批量生成工资条
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <returns>工资条列表</returns>
        Task<List<PayslipDto>> BatchGeneratePayslipsAsync(List<int> payrollResultIds);
        
        #endregion
        
        #region 数据验证
        
        /// <summary>
        /// 验证工资计算数据
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="period">工资期间</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Payroll.PayrollValidationResult> ValidatePayrollDataAsync(int employeeId, XTHR.Core.DTOs.Payroll.PayrollPeriod period);
        
        /// <summary>
        /// 检查工资计算规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <returns>检查结果</returns>
        Task<RuleValidationResult> ValidatePayrollRuleAsync(int ruleId);
        
        #endregion
    }
}