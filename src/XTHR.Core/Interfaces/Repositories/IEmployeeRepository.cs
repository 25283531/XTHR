using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Employee;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 员工仓储接口
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee, int>
    {
        #region 员工基础查询
        
        /// <summary>
        /// 根据员工编号获取员工
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <returns>员工信息</returns>
        Task<Employee> GetByEmployeeNumberAsync(string employeeNumber);

        /// <summary>
        /// 根据员工姓名查找员工
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <returns>员工实体列表</returns>
        Task<IEnumerable<Employee>> FindByNameAsync(string name);
        
        /// <summary>
        /// 根据身份证号获取员工
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns>员工信息</returns>
        Task<Employee> GetByIdCardAsync(string idCard);
        
        /// <summary>
        /// 根据手机号获取员工
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns>员工信息</returns>
        Task<Employee> GetByPhoneAsync(string phone);
        
        /// <summary>
        /// 根据邮箱获取员工
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns>员工信息</returns>
        Task<Employee> GetByEmailAsync(string email);
        
        /// <summary>
        /// 根据部门ID获取员工列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="includeSubDepartments">是否包含子部门</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByDepartmentAsync(int departmentId, bool includeSubDepartments = false);
        
        /// <summary>
        /// 根据职位ID获取员工列表
        /// </summary>
        /// <param name="positionId">职位ID</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByPositionAsync(int positionId);
        
        /// <summary>
        /// 根据在职状态获取员工列表
        /// </summary>
        /// <param name="isActive">是否在职</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByStatusAsync(bool isActive);
        
        #endregion
        
        #region 员工分页查询
        
        /// <summary>
        /// 分页查询员工
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<Employee>> GetEmployeesPagedAsync(EmployeeQueryRequest request);
        
        /// <summary>
        /// 搜索员工
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="searchField">搜索字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<Employee>> SearchEmployeesAsync(string keyword, EmployeeSearchField searchField, int pageIndex, int pageSize);
        
        #endregion
        
        #region 员工统计查询
        
        /// <summary>
        /// 获取员工总数统计
        /// </summary>
        /// <returns>员工统计信息</returns>
        Task<EmployeeStatisticsDto> GetEmployeeStatisticsAsync();
        
        /// <summary>
        /// 获取部门员工统计
        /// </summary>
        /// <returns>部门统计列表</returns>
        Task<IEnumerable<DepartmentStatisticsDto>> GetDepartmentStatisticsAsync();
        
        /// <summary>
        /// 获取年龄分布统计
        /// </summary>
        /// <returns>年龄分布列表</returns>
        Task<IEnumerable<AgeDistributionDto>> GetAgeDistributionAsync();
        
        /// <summary>
        /// 获取入职趋势统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>入职趋势数据</returns>
        Task<IEnumerable<HireTrendDto>> GetHireTrendAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取离职趋势统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>离职趋势数据</returns>
        Task<IEnumerable<ResignationTrendDto>> GetResignationTrendAsync(DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region 员工验证
        
        /// <summary>
        /// 验证员工编号是否唯一
        /// </summary>
        /// <param name="employeeNumber">员工编号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<bool> IsEmployeeNumberUniqueAsync(string employeeNumber, int? excludeId = null);
        
        /// <summary>
        /// 验证身份证号是否唯一
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<bool> IsIdCardUniqueAsync(string idCard, int? excludeId = null);
        
        /// <summary>
        /// 验证手机号是否唯一
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<bool> IsPhoneUniqueAsync(string phone, int? excludeId = null);
        
        /// <summary>
        /// 验证邮箱是否唯一
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否唯一</returns>
        Task<bool> IsEmailUniqueAsync(string email, int? excludeId = null);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量更新员工状态
        /// </summary>
        /// <param name="employeeIds">员工ID列表</param>
        /// <param name="isActive">新状态</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>更新的数量</returns>
        Task<int> BatchUpdateStatusAsync(IEnumerable<int> employeeIds, bool isActive, string operatedBy);
        
        /// <summary>
        /// 批量更新员工部门
        /// </summary>
        /// <param name="employeeIds">员工ID列表</param>
        /// <param name="departmentId">新部门ID</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>更新的数量</returns>
        Task<int> BatchUpdateDepartmentAsync(IEnumerable<int> employeeIds, int departmentId, string operatedBy);
        
        /// <summary>
        /// 批量导入员工
        /// </summary>
        /// <param name="employees">员工列表</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>导入结果</returns>
        Task<BatchImportResult<Employee>> BatchImportAsync(IEnumerable<Employee> employees, string operatedBy);
        
        #endregion
        
        #region 员工关联数据
        
        /// <summary>
        /// 获取员工的工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息</returns>
        Task<SalaryBase> GetEmployeeSalaryBaseAsync(int employeeId);
        
        /// <summary>
        /// 获取员工的考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetEmployeeAttendanceAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取员工的工资记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>工资记录列表</returns>
        Task<IEnumerable<PayrollResult>> GetEmployeePayrollAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region 员工档案管理
        
        /// <summary>
        /// 获取员工变更历史
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>变更历史列表</returns>
        Task<IEnumerable<EmployeeChangeHistory>> GetEmployeeChangeHistoryAsync(int employeeId);
        
        /// <summary>
        /// 记录员工变更
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="oldValue">原值</param>
        /// <param name="newValue">新值</param>
        /// <param name="changeReason">变更原因</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>变更记录ID</returns>
        Task<int> RecordEmployeeChangeAsync(int employeeId, string changeType, string oldValue, string newValue, string changeReason, string operatedBy);
        
        /// <summary>
        /// 获取员工合同信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>合同信息列表</returns>
        Task<IEnumerable<EmployeeContract>> GetEmployeeContractsAsync(int employeeId);
        
        /// <summary>
        /// 获取员工培训记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>培训记录列表</returns>
        Task<IEnumerable<EmployeeTraining>> GetEmployeeTrainingAsync(int employeeId);
        
        #endregion
    }
    
    /// <summary>
    /// 入职趋势DTO
    /// </summary>
    public class HireTrendDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// 入职人数
        /// </summary>
        public int HireCount { get; set; }
        
        /// <summary>
        /// 累计入职人数
        /// </summary>
        public int CumulativeHireCount { get; set; }
    }
    
    /// <summary>
    /// 离职趋势DTO
    /// </summary>
    public class ResignationTrendDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// 离职人数
        /// </summary>
        public int ResignationCount { get; set; }
        
        /// <summary>
        /// 累计离职人数
        /// </summary>
        public int CumulativeResignationCount { get; set; }
        
        /// <summary>
        /// 离职率
        /// </summary>
        public decimal ResignationRate { get; set; }
    }
}