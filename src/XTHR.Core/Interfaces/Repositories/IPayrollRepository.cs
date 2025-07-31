using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Requests;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 工资管理仓储接口
    /// </summary>
    public interface IPayrollRepository : IBaseRepository<PayrollResult, int>
    {
        #region 工资结果查询
        
        /// <summary>
        /// 根据员工ID和工资期间获取工资结果
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果</returns>
        Task<PayrollResult> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 根据工资期间获取工资结果列表
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<PayrollResult>> GetByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 根据部门和工资期间获取工资结果
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<PayrollResult>> GetByDepartmentAndPeriodAsync(int departmentId, int year, int month);
        
        /// <summary>
        /// 根据审核状态获取工资结果
        /// </summary>
        /// <param name="approvalStatus">审核状态</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<PayrollResult>> GetByApprovalStatusAsync(string approvalStatus, int year, int month);
        
        /// <summary>
        /// 分页查询工资结果
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<PayrollResult>> GetPayrollResultsPagedAsync(PayrollQueryRequest request);
        
        #endregion
        
        #region 工资基础信息管理
        
        /// <summary>
        /// 根据员工ID获取工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息</returns>
        Task<SalaryBase> GetSalaryBaseByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 获取所有工资基础信息
        /// </summary>
        /// <returns>工资基础信息列表</returns>
        Task<IEnumerable<SalaryBase>> GetAllSalaryBasesAsync();
        
        /// <summary>
        /// 根据部门获取工资基础信息
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>工资基础信息列表</returns>
        Task<IEnumerable<SalaryBase>> GetSalaryBasesByDepartmentAsync(int departmentId);
        
        /// <summary>
        /// 创建工资基础信息
        /// </summary>
        /// <param name="salaryBase">工资基础信息</param>
        /// <returns>创建的工资基础信息</returns>
        Task<SalaryBase> CreateSalaryBaseAsync(SalaryBase salaryBase);
        
        /// <summary>
        /// 更新工资基础信息
        /// </summary>
        /// <param name="salaryBase">工资基础信息</param>
        /// <returns>更新的工资基础信息</returns>
        Task<SalaryBase> UpdateSalaryBaseAsync(SalaryBase salaryBase);
        
        /// <summary>
        /// 获取工资基础信息历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息历史列表</returns>
        Task<IEnumerable<SalaryBaseHistory>> GetSalaryBaseHistoryAsync(int employeeId);
        
        #endregion
        
        #region 工资计算
        
        /// <summary>
        /// 计算单个员工工资
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>工资计算结果</returns>
        Task<PayrollCalculationResult> CalculatePayrollAsync(int employeeId, int year, int month, string operatedBy);
        
        /// <summary>
        /// 批量计算工资
        /// </summary>
        /// <param name="employeeIds">员工ID列表</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>批量工资计算结果</returns>
        Task<BatchPayrollCalculationResult> BatchCalculatePayrollAsync(IEnumerable<int> employeeIds, int year, int month, string operatedBy);
        
        /// <summary>
        /// 重新计算工资
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>工资计算结果</returns>
        Task<PayrollCalculationResult> RecalculatePayrollAsync(int payrollResultId, string operatedBy);
        
        /// <summary>
        /// 保存工资计算结果
        /// </summary>
        /// <param name="payrollResult">工资结果</param>
        /// <returns>保存的工资结果</returns>
        Task<PayrollResult> SavePayrollResultAsync(PayrollResult payrollResult);
        
        /// <summary>
        /// 批量保存工资计算结果
        /// </summary>
        /// <param name="payrollResults">工资结果列表</param>
        /// <returns>保存的数量</returns>
        Task<int> BatchSavePayrollResultsAsync(IEnumerable<PayrollResult> payrollResults);
        
        #endregion
        
        #region 工资统计分析
        
        /// <summary>
        /// 获取工资汇总统计
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资汇总统计</returns>
        Task<PayrollSummaryStatistics> GetPayrollSummaryAsync(int year, int month);
        
        /// <summary>
        /// 获取部门工资统计
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>部门工资统计列表</returns>
        Task<IEnumerable<DepartmentPayrollStatisticsDto>> GetDepartmentPayrollStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取工资成本分析
        /// </summary>
        /// <param name="startYear">开始年份</param>
        /// <param name="startMonth">开始月份</param>
        /// <param name="endYear">结束年份</param>
        /// <param name="endMonth">结束月份</param>
        /// <returns>工资成本分析数据</returns>
        Task<IEnumerable<PayrollCostAnalysisDto>> GetPayrollCostAnalysisAsync(int startYear, int startMonth, int endYear, int endMonth);
        
        /// <summary>
        /// 获取工资趋势分析
        /// </summary>
        /// <param name="startYear">开始年份</param>
        /// <param name="startMonth">开始月份</param>
        /// <param name="endYear">结束年份</param>
        /// <param name="endMonth">结束月份</param>
        /// <returns>工资趋势分析数据</returns>
        Task<IEnumerable<PayrollTrendAnalysisDto>> GetPayrollTrendAnalysisAsync(int startYear, int startMonth, int endYear, int endMonth);
        
        #endregion
        
        #region 工资审核
        
        /// <summary>
        /// 提交工资审核
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <param name="submittedBy">提交人</param>
        /// <returns>提交的数量</returns>
        Task<int> SubmitForApprovalAsync(IEnumerable<int> payrollResultIds, string submittedBy);
        
        /// <summary>
        /// 审批工资
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <param name="approvalStatus">审批状态</param>
        /// <param name="approvalComments">审批意见</param>
        /// <param name="approvedBy">审批人</param>
        /// <returns>审批的数量</returns>
        Task<int> ApprovePayrollAsync(IEnumerable<int> payrollResultIds, string approvalStatus, string approvalComments, string approvedBy);
        
        /// <summary>
        /// 获取待审核工资列表
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>待审核工资列表</returns>
        Task<IEnumerable<PayrollResult>> GetPendingApprovalPayrollsAsync(int year, int month);
        
        #endregion
        
        #region 工资发放
        
        /// <summary>
        /// 标记工资已发放
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <param name="paymentDate">发放日期</param>
        /// <param name="paymentMethod">发放方式</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>发放的数量</returns>
        Task<int> MarkAsPayedAsync(IEnumerable<int> payrollResultIds, DateTime paymentDate, string paymentMethod, string operatedBy);
        
        /// <summary>
        /// 生成工资条
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <returns>工资条信息</returns>
        Task<PayslipDto> GeneratePayslipAsync(int payrollResultId);
        
        /// <summary>
        /// 批量生成工资条
        /// </summary>
        /// <param name="payrollResultIds">工资结果ID列表</param>
        /// <returns>工资条信息列表</returns>
        Task<IEnumerable<PayslipDto>> BatchGeneratePayslipsAsync(IEnumerable<int> payrollResultIds);
        
        #endregion
        
        #region 工资数据验证
        
        /// <summary>
        /// 验证工资数据
        /// </summary>
        /// <param name="payrollResult">工资结果</param>
        /// <returns>验证结果</returns>
        Task<ValidationResult> ValidatePayrollDataAsync(PayrollResult payrollResult);
        
        /// <summary>
        /// 验证工资计算规则
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>验证结果</returns>
        Task<ValidationResult> ValidateCalculationRulesAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 检查工资数据完整性
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>完整性检查结果</returns>
        Task<PayrollIntegrityCheckResult> CheckPayrollIntegrityAsync(int year, int month);
        
        #endregion
        
        #region 工资历史和变更
        
        /// <summary>
        /// 获取工资变更历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>工资变更历史列表</returns>
        Task<IEnumerable<PayrollChangeHistory>> GetPayrollChangeHistoryAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);
        
        /// <summary>
        /// 记录工资变更
        /// </summary>
        /// <param name="payrollResultId">工资结果ID</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="oldValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="changeReason">变更原因</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>变更记录ID</returns>
        Task<int> RecordPayrollChangeAsync(int payrollResultId, string changeType, decimal oldValue, decimal newValue, string changeReason, string operatedBy);
        
        #endregion
    }
    
    /// <summary>
    /// 工资成本分析DTO
    /// </summary>
    public class PayrollCostAnalysisDto
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal TotalCost { get; set; }
        
        /// <summary>
        /// 基本工资成本
        /// </summary>
        public decimal BaseSalaryCost { get; set; }
        
        /// <summary>
        /// 津贴补贴成本
        /// </summary>
        public decimal AllowanceCost { get; set; }
        
        /// <summary>
        /// 奖金成本
        /// </summary>
        public decimal BonusCost { get; set; }
        
        /// <summary>
        /// 加班费成本
        /// </summary>
        public decimal OvertimeCost { get; set; }
        
        /// <summary>
        /// 社保成本
        /// </summary>
        public decimal SocialInsuranceCost { get; set; }
        
        /// <summary>
        /// 公积金成本
        /// </summary>
        public decimal HousingFundCost { get; set; }
        
        /// <summary>
        /// 个税成本
        /// </summary>
        public decimal IncomeTaxCost { get; set; }
        
        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }
        
        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }
    }
    
    /// <summary>
    /// 工资趋势分析DTO
    /// </summary>
    public class PayrollTrendAnalysisDto
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        
        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }
        
        /// <summary>
        /// 总工资
        /// </summary>
        public decimal TotalSalary { get; set; }
        
        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }
        
        /// <summary>
        /// 工资增长率
        /// </summary>
        public decimal GrowthRate { get; set; }
        
        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }
        
        /// <summary>
        /// 环比增长率
        /// </summary>
        public decimal MonthOverMonthGrowth { get; set; }
        
        /// <summary>
        /// 同比增长率
        /// </summary>
        public decimal YearOverYearGrowth { get; set; }
    }
    
    /// <summary>
    /// 工资完整性检查结果
    /// </summary>
    public class PayrollIntegrityCheckResult
    {
        /// <summary>
        /// 检查是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// 已计算工资员工数
        /// </summary>
        public int CalculatedEmployees { get; set; }
        
        /// <summary>
        /// 缺失工资数据的员工
        /// </summary>
        public List<int> MissingPayrollEmployees { get; set; } = new List<int>();
        
        /// <summary>
        /// 工资数据异常的员工
        /// </summary>
        public List<int> AbnormalPayrollEmployees { get; set; } = new List<int>();
        
        /// <summary>
        /// 检查详情
        /// </summary>
        public List<string> CheckDetails { get; set; } = new List<string>();
    }
}