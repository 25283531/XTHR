using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Models;
using XTHR.Common.Services;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Employee;
using XTHR.Core.DTOs.Common;

namespace XTHR.Core.Interfaces
{
    /// <summary>
    /// 员工服务接口
    /// 提供员工信息管理的业务逻辑
    /// </summary>
    public interface IEmployeeService
    {
        #region 基础CRUD操作
        
        /// <summary>
        /// 获取员工详细信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>员工详细信息</returns>
        Task<XTHR.Core.DTOs.Employee.EmployeeDetailDto> GetEmployeeDetailAsync(int employeeId);
        
        /// <summary>
        /// 获取员工列表（分页）
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页员工列表</returns>
        Task<XTHR.Core.DTOs.Common.PagedResult<XTHR.Core.DTOs.Employee.EmployeeListDto>> GetEmployeeListAsync(XTHR.Core.DTOs.Employee.EmployeeQueryRequest request);
        
        /// <summary>
        /// 创建员工
        /// </summary>
        /// <param name="request">创建员工请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.ServiceResult<int>> CreateEmployeeAsync(XTHR.Core.DTOs.Employee.CreateEmployeeRequest request);
        
        /// <summary>
        /// 更新员工信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.ServiceResult> UpdateEmployeeAsync(int employeeId, XTHR.Core.DTOs.Employee.UpdateEmployeeRequest request);
        
        /// <summary>
        /// 删除员工（软删除）
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.ServiceResult> DeleteEmployeeAsync(int employeeId);
        
        #endregion
        
        #region 业务查询
        
        /// <summary>
        /// 根据员工编号获取员工信息
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns>员工信息</returns>
        Task<XTHR.Core.DTOs.Employee.EmployeeDetailDto> GetEmployeeByNumberAsync(string employeeNumber);
        
        /// <summary>
        /// 获取部门员工列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="includeInactive">是否包含离职员工</param>
        /// <returns>员工列表</returns>
        Task<List<XTHR.Core.DTOs.Employee.EmployeeListDto>> GetEmployeesByDepartmentAsync(string department, bool includeInactive = false);
        
        /// <summary>
        /// 获取在职员工列表
        /// </summary>
        /// <returns>在职员工列表</returns>
        Task<List<XTHR.Core.DTOs.Employee.EmployeeListDto>> GetActiveEmployeesAsync();
        
        /// <summary>
        /// 搜索员工
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="searchFields">搜索字段</param>
        /// <returns>搜索结果</returns>
        Task<List<XTHR.Core.DTOs.Employee.EmployeeListDto>> SearchEmployeesAsync(string keyword, EmployeeSearchFields searchFields = EmployeeSearchFields.All);
        
        #endregion
        
        #region 统计分析
        
        /// <summary>
        /// 获取员工统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<XTHR.Core.DTOs.Employee.EmployeeStatisticsDto> GetEmployeeStatisticsAsync();
        
        /// <summary>
        /// 获取部门统计信息
        /// </summary>
        /// <returns>部门统计</returns>
        Task<List<XTHR.Core.DTOs.Employee.DepartmentStatisticsDto>> GetDepartmentStatisticsAsync();
        
        /// <summary>
        /// 获取员工年龄分布
        /// </summary>
        /// <returns>年龄分布</returns>
        Task<List<XTHR.Core.DTOs.Employee.AgeDistributionDto>> GetAgeDistributionAsync();
        
        #endregion
        
        #region 数据验证
        
        /// <summary>
        /// 验证员工编号是否唯一
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <param name="excludeEmployeeId">排除的员工ID（用于更新时验证）</param>
        /// <returns>是否唯一</returns>
        Task<bool> IsEmployeeNumberUniqueAsync(string employeeNumber, int? excludeEmployeeId = null);
        
        /// <summary>
        /// 验证身份证号是否唯一
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <param name="excludeEmployeeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<bool> IsIdCardUniqueAsync(string idCard, int? excludeEmployeeId = null);
        
        /// <summary>
        /// 验证员工数据
        /// </summary>
        /// <param name="employee">员工数据</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Common.Models.ValidationResult> ValidateEmployeeAsync(Employee employee);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量导入员工
        /// </summary>
        /// <param name="employees">员工列表</param>
        /// <returns>导入结果</returns>
        Task<BatchImportResult<EmployeeImportDto>> BatchImportEmployeesAsync(List<EmployeeImportDto> employees);
        
        /// <summary>
        /// 批量更新员工状态
        /// </summary>
        /// <param name="employeeIds">员工ID列表</param>
        /// <param name="status">新状态</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.ServiceResult> BatchUpdateEmployeeStatusAsync(List<int> employeeIds, string status);
        
        #endregion
    }
    
    /// <summary>
    /// 员工搜索字段枚举
    /// </summary>
    [Flags]
    public enum EmployeeSearchFields
    {
        Name = 1,
        EmployeeNumber = 2,
        Department = 4,
        Position = 8,
        Phone = 16,
        Email = 32,
        All = Name | EmployeeNumber | Department | Position | Phone | Email
    }
}