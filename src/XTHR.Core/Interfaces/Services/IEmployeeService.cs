using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Requests;
using XTHR.Core.DTOs.Responses;
using XTHR.Core.DTOs.Employee;
using XTHR.Common.Entities;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Services
{
    /// <summary>
    /// 员工服务接口
    /// </summary>
    public interface IEmployeeService : IBaseService<Employee, int, EmployeeDetailDto, EmployeeListDto, CreateEmployeeRequest, UpdateEmployeeRequest>
    {
        #region 员工基础查询
        
        /// <summary>
        /// 根据员工编号获取员工信息
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns>员工详情</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeDetailDto>> GetByEmployeeNumberAsync(string employeeNumber);
        
        /// <summary>
        /// 根据身份证号获取员工信息
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns>员工详情</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeDetailDto>> GetByIdCardAsync(string idCard);
        
        /// <summary>
        /// 根据手机号获取员工信息
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <returns>员工详情</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeDetailDto>> GetByPhoneNumberAsync(string phoneNumber);
        
        /// <summary>
        /// 根据邮箱获取员工信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>员工详情</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeDetailDto>> GetByEmailAsync(string email);
        
        /// <summary>
        /// 根据部门ID获取员工列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="includeSubDepartments">是否包含子部门</param>
        /// <returns>员工列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeListDto>>> GetByDepartmentAsync(int departmentId, bool includeSubDepartments = false);
        
        /// <summary>
        /// 根据职位ID获取员工列表
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <returns>员工列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeListDto>>> GetByPositionAsync(int positionId);
        
        /// <summary>
        /// 根据在职状态获取员工列表
        /// </summary>
        /// <param name="status">在职状态</param>
        /// <returns>员工列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeListDto>>> GetByStatusAsync(EmployeeStatus status);
        
        /// <summary>
        /// 员工高级查询
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<EmployeeListDto>>> SearchEmployeesAsync(EmployeeQueryRequest request);
        
        #endregion
        
        #region 员工统计查询
        
        /// <summary>
        /// 获取员工总数统计
        /// </summary>
        /// <returns>统计结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeStatisticsDto>> GetEmployeeStatisticsAsync();
        
        /// <summary>
        /// 获取部门员工统计
        /// </summary>
        /// <param name="departmentId">部门ID（可选）</param>
        /// <returns>部门统计列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<DepartmentEmployeeStatisticsDto>>> GetDepartmentStatisticsAsync(int departmentId = 0);
        
        /// <summary>
        /// 获取员工年龄分布统计
        /// </summary>
        /// <returns>年龄分布统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeAgeDistributionDto>> GetAgeDistributionAsync();
        
        /// <summary>
        /// 获取员工入职趋势统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="groupBy">分组方式（月/季度/年）</param>
        /// <returns>入职趋势</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeHireTrendDto>>> GetHireTrendAsync(DateTime startDate, DateTime endDate, TrendGroupBy groupBy = TrendGroupBy.Month);
        
        /// <summary>
        /// 获取员工离职趋势统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="groupBy">分组方式（月/季度/年）</param>
        /// <returns>离职趋势</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeResignationTrendDto>>> GetResignationTrendAsync(DateTime startDate, DateTime endDate, TrendGroupBy groupBy = TrendGroupBy.Month);
        
        #endregion
        
        #region 员工信息验证
        
        /// <summary>
        /// 验证员工编号是否唯一
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsEmployeeNumberUniqueAsync(string employeeNumber, int excludeId = 0);
        
        /// <summary>
        /// 验证身份证号是否唯一
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsIdCardUniqueAsync(string idCard, int excludeId = 0);
        
        /// <summary>
        /// 验证手机号是否唯一
        /// </summary>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsPhoneNumberUniqueAsync(string phoneNumber, int excludeId = 0);
        
        /// <summary>
        /// 验证邮箱是否唯一
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsEmailUniqueAsync(string email, int excludeId = 0);
        
        /// <summary>
        /// 验证员工信息完整性
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeValidationResult>> ValidateEmployeeDataAsync(int employeeId);
        
        #endregion
        
        #region 员工状态管理
        
        /// <summary>
        /// 更新员工状态
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="status">新状态</param>
        /// <param name="reason">变更原因</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> UpdateEmployeeStatusAsync(int employeeId, EmployeeStatus status, string reason = "");
        
        /// <summary>
        /// 批量更新员工状态
        /// </summary>
        /// <param name="request">批量更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchUpdateResult>> BatchUpdateStatusAsync(BatchUpdateEmployeeStatusRequest request);
        
        /// <summary>
        /// 员工入职处理
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="hireDate">入职日期</param>
        /// <returns>处理结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ProcessEmployeeHireAsync(int employeeId, DateTime hireDate);
        
        /// <summary>
        /// 员工离职处理
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="resignationDate">离职日期</param>
        /// <param name="reason">离职原因</param>
        /// <returns>处理结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ProcessEmployeeResignationAsync(int employeeId, DateTime resignationDate, string reason);
        
        #endregion
        
        #region 员工部门管理
        
        /// <summary>
        /// 更新员工部门
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="newDepartmentId">新部门ID</param>
        /// <param name="effectiveDate">生效日期</param>
        /// <param name="reason">变更原因</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> UpdateEmployeeDepartmentAsync(int employeeId, int newDepartmentId, DateTime effectiveDate, string reason = "");
        
        /// <summary>
        /// 批量更新员工部门
        /// </summary>
        /// <param name="request">批量更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchUpdateResult>> BatchUpdateDepartmentAsync(BatchUpdateEmployeeDepartmentRequest request);
        
        /// <summary>
        /// 获取员工部门变更历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>变更历史</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeDepartmentChangeHistoryDto>>> GetDepartmentChangeHistoryAsync(int employeeId);
        
        #endregion
        
        #region 员工导入导出
        
        /// <summary>
        /// 导入员工数据
        /// </summary>
        /// <param name="request">导入请求</param>
        /// <returns>导入结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeImportResultDto>> ImportEmployeesAsync(EmployeeImportRequest request);
        
        /// <summary>
        /// 验证导入数据
        /// </summary>
        /// <param name="data">导入数据</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeImportValidationResult>> ValidateImportDataAsync(IEnumerable<EmployeeImportData> data);
        
        /// <summary>
        /// 导出员工数据
        /// </summary>
        /// <param name="request">导出请求</param>
        /// <returns>导出结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<EmployeeListDto>>> ExportEmployeesAsync(EmployeeExportRequest request);
        
        /// <summary>
        /// 生成员工导入模板
        /// </summary>
        /// <param name="format">文件格式</param>
        /// <returns>模板文件</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<object>>> GenerateImportTemplateAsync(ExportFormat format = ExportFormat.Excel);
        
        #endregion
        
        #region 员工关联数据
        
        /// <summary>
        /// 获取员工工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeePayrollBaseDto>> GetEmployeePayrollBaseAsync(int employeeId);
        
        /// <summary>
        /// 获取员工考勤汇总
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>考勤汇总</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeAttendanceSummaryDto>> GetEmployeeAttendanceSummaryAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取员工工资记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>工资记录列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeePayrollHistoryDto>>> GetEmployeePayrollHistoryAsync(int employeeId, DateTime startDate = default, DateTime endDate = default);
        
        #endregion
        
        #region 员工档案管理
        
        /// <summary>
        /// 获取员工变更历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="changeType">变更类型</param>
        /// <returns>变更历史</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeChangeHistoryDto>>> GetEmployeeChangeHistoryAsync(int employeeId, EmployeeChangeType changeType = EmployeeChangeType.BasicInfo);
        
        /// <summary>
        /// 记录员工变更
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="oldValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="reason">变更原因</param>
        /// <returns>记录结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> RecordEmployeeChangeAsync(int employeeId, EmployeeChangeType changeType, string oldValue, string newValue, string reason = "");
        
        /// <summary>
        /// 获取员工合同信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>合同信息列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeContractDto>>> GetEmployeeContractsAsync(int employeeId);
        
        /// <summary>
        /// 获取员工培训记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>培训记录列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeTrainingRecordDto>>> GetEmployeeTrainingRecordsAsync(int employeeId);
        
        #endregion
        
        #region 员工报表
        
        /// <summary>
        /// 生成员工花名册
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>花名册数据</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeRosterReportDto>> GenerateEmployeeRosterAsync(EmployeeReportRequest request);
        
        /// <summary>
        /// 生成员工统计报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>统计报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeStatisticsReportDto>> GenerateStatisticsReportAsync(EmployeeReportRequest request);
        
        /// <summary>
        /// 生成员工异动报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>异动报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeChangeReportDto>> GenerateChangeReportAsync(EmployeeReportRequest request);
        
        #endregion
        
        #region 员工搜索
        
        /// <summary>
        /// 快速搜索员工
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="maxResults">最大结果数</param>
        /// <returns>搜索结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<EmployeeSearchResultDto>>> QuickSearchAsync(string keyword, int maxResults = 10);
        
        /// <summary>
        /// 高级搜索员工
        /// </summary>
        /// <param name="request">搜索请求</param>
        /// <returns>搜索结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<EmployeeListDto>>> AdvancedSearchAsync(EmployeeAdvancedSearchRequest request);
        
        #endregion
    }
    
    /// <summary>
    /// 趋势分组方式
    /// </summary>
    public enum TrendGroupBy
    {
        /// <summary>
        /// 按月
        /// </summary>
        Month,
        
        /// <summary>
        /// 按季度
        /// </summary>
        Quarter,
        
        /// <summary>
        /// 按年
        /// </summary>
        Year
    }
    
    /// <summary>
    /// 员工变更类型
    /// </summary>
    public enum EmployeeChangeType
    {
        /// <summary>
        /// 基本信息变更
        /// </summary>
        BasicInfo,
        
        /// <summary>
        /// 部门变更
        /// </summary>
        Department,
        
        /// <summary>
        /// 职位变更
        /// </summary>
        Position,
        
        /// <summary>
        /// 状态变更
        /// </summary>
        Status,
        
        /// <summary>
        /// 工资变更
        /// </summary>
        Salary,
        
        /// <summary>
        /// 联系方式变更
        /// </summary>
        Contact,
        
        /// <summary>
        /// 其他变更
        /// </summary>
        Other
    }
    
    /// <summary>
    /// 批量更新结果
    /// </summary>
    public class BatchUpdateResult
    {
        /// <summary>
        /// 更新是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功更新数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败更新数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<BatchUpdateFailure> Failures { get; set; } = new List<BatchUpdateFailure>();
    }
    
    /// <summary>
    /// 批量更新失败记录
    /// </summary>
    public class BatchUpdateFailure
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 原始数据
        /// </summary>
        public object OriginalData { get; set; }
    }
    
    /// <summary>
    /// 员工验证结果
    /// </summary>
    public class EmployeeValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 完整性得分（0-100）
        /// </summary>
        public int CompletenessScore { get; set; }
        
        /// <summary>
        /// 缺失字段
        /// </summary>
        public List<string> MissingFields { get; set; } = new List<string>();
        
        /// <summary>
        /// 无效字段
        /// </summary>
        public List<ValidationError> InvalidFields { get; set; } = new List<ValidationError>();
        
        /// <summary>
        /// 建议改进项
        /// </summary>
        public List<string> Suggestions { get; set; } = new List<string>();
    }
}