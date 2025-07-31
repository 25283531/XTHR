using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Requests;
using XTHR.Common.Entities;
using XTHR.Core.Entities;
using XTHR.Common.Models;
using XTHR.Core.Interfaces.Services;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 工资管理仓储接口
    /// </summary>
    public interface IPayrollRepository /* : IBaseRepository<PayrollResult, int> */
    {
        #region 工资结果查询
        
        /// <summary>
        /// 根据员工ID和工资期间获取工资结果
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果</returns>
        Task<XTHR.Common.Models.PayrollResult> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 根据工资期间获取工资结果列表
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<XTHR.Common.Models.PayrollResult>> GetByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 根据部门和工资期间获取工资结果
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<XTHR.Common.Models.PayrollResult>> GetByDepartmentAndPeriodAsync(int departmentId, int year, int month);
        
        /// <summary>
        /// 根据审核状态获取工资结果
        /// </summary>
        /// <param name="approvalStatus">审核状态</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<XTHR.Common.Models.PayrollResult>> GetByApprovalStatusAsync(string approvalStatus, int year, int month);
        
        /// <summary>
        /// 根据员工ID获取工资结果列表
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资结果列表</returns>
        Task<IEnumerable<XTHR.Common.Models.PayrollResult>> GetByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据ID获取工资结果详情
        /// </summary>
        /// <param name="id">工资结果ID</param>
        /// <returns>工资结果详情</returns>
        Task<XTHR.Common.Models.PayrollResult> GetByIdAsync(int id);
        
        #endregion
        
        #region 工资基础数据
        
        /// <summary>
        /// 根据员工ID获取当前工资基础数据
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础数据</returns>
        Task<XTHR.Common.Models.SalaryBase> GetCurrentSalaryBaseAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID获取工资基础数据历史记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础数据历史记录</returns>
        Task<IEnumerable<XTHR.Common.Models.SalaryBase>> GetSalaryBaseHistoryAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID和日期获取工资基础数据
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>工资基础数据</returns>
        Task<XTHR.Common.Models.SalaryBase> GetSalaryBaseByDateAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 添加工资基础数据
        /// </summary>
        /// <param name="salaryBase">工资基础数据</param>
        /// <returns>任务</returns>
        Task AddSalaryBaseAsync(XTHR.Common.Models.SalaryBase salaryBase);
        
        /// <summary>
        /// 更新工资基础数据
        /// </summary>
        /// <param name="salaryBase">工资基础数据</param>
        /// <returns>任务</returns>
        Task UpdateSalaryBaseAsync(XTHR.Common.Models.SalaryBase salaryBase);
        
        /// <summary>
        /// 删除工资基础数据
        /// </summary>
        /// <param name="id">工资基础数据ID</param>
        /// <returns>任务</returns>
        Task DeleteSalaryBaseAsync(int id);
        
        #endregion
        
        #region 工资计算
        
        /// <summary>
        /// 添加工资计算结果
        /// </summary>
        /// <param name="payrollResult">工资计算结果</param>
        /// <returns>任务</returns>
        Task AddPayrollResultAsync(XTHR.Common.Models.PayrollResult payrollResult);
        
        /// <summary>
        /// 更新工资计算结果
        /// </summary>
        /// <param name="payrollResult">工资计算结果</param>
        /// <returns>任务</returns>
        Task UpdatePayrollResultAsync(XTHR.Common.Models.PayrollResult payrollResult);
        
        /// <summary>
        /// 删除工资计算结果
        /// </summary>
        /// <param name="id">工资计算结果ID</param>
        /// <returns>任务</returns>
        Task DeletePayrollResultAsync(int id);
        
        /// <summary>
        /// 批量添加工资计算结果
        /// </summary>
        /// <param name="payrollResults">工资计算结果列表</param>
        /// <returns>任务</returns>
        Task BatchAddPayrollResultsAsync(IEnumerable<XTHR.Common.Models.PayrollResult> payrollResults);
        
        #endregion
        
        #region 工资审核
        
        /// <summary>
        /// 提交工资审核
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="submittedBy">提交人</param>
        /// <returns>任务</returns>
        Task SubmitForApprovalAsync(int year, int month, int submittedBy);
        
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="approvedBy">审核人</param>
        /// <returns>任务</returns>
        Task ApproveAsync(int year, int month, int approvedBy);
        
        /// <summary>
        /// 审核拒绝
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="rejectedBy">拒绝人</param>
        /// <param name="reason">拒绝原因</param>
        /// <returns>任务</returns>
        Task RejectAsync(int year, int month, int rejectedBy, string reason);
        
        #endregion
        
        #region 数据导入
        
        /// <summary>
        /// 导入工资基础数据
        /// </summary>
        /// <param name="importData">导入数据</param>
        /// <param name="importedBy">导入人</param>
        /// <returns>导入结果</returns>
        Task<XTHR.Core.Interfaces.Services.ImportResult<XTHR.Common.Models.SalaryBase>> ImportSalaryBaseDataAsync(IEnumerable<XTHR.Common.Models.SalaryBase> importData, int importedBy);
        
        /// <summary>
        /// 验证工资计算数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Common.Models.ValidationResult> ValidatePayrollDataAsync(int year, int month);
        
        #endregion
    }
}