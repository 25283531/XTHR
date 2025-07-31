using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Attendance;
using XTHR.Core.DTOs.Requests;
using XTHR.Common.Entities;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 考勤管理仓储接口
    /// </summary>
    public interface IAttendanceRepository : IBaseRepository<AttendanceRecord, int>
    {
        #region 考勤记录查询
        
        /// <summary>
        /// 根据员工ID和日期获取考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <returns>考勤记录</returns>
        Task<AttendanceRecord> GetByEmployeeAndDateAsync(int employeeId, DateTime attendanceDate);
        
        /// <summary>
        /// 根据员工ID和日期范围获取考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 根据部门和日期范围获取考勤记录
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetByDepartmentAndDateRangeAsync(int departmentId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 根据考勤状态获取考勤记录
        /// </summary>
        /// <param name="attendanceStatus">考勤状态</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetByStatusAndDateRangeAsync(string attendanceStatus, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 分页查询考勤记录
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<AttendanceRecord>> GetAttendanceRecordsPagedAsync(AttendanceQueryRequest request);
        
        #endregion
        
        #region 员工考勤查询
        
        /// <summary>
        /// 获取员工月度考勤汇总
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>月度考勤汇总</returns>
        Task<EmployeeMonthlyAttendanceSummaryDto> GetEmployeeMonthlyAttendanceAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取员工考勤统计
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤统计</returns>
        Task<EmployeeAttendanceStatisticsDto> GetEmployeeAttendanceStatisticsAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 检查员工是否全勤
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>是否全勤</returns>
        Task<bool> IsEmployeePerfectAttendanceAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取员工考勤异常记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>异常记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetEmployeeAbnormalAttendanceAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region 考勤统计分析
        
        /// <summary>
        /// 获取部门考勤统计
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>部门考勤统计</returns>
        Task<DepartmentAttendanceStatisticsDto> GetDepartmentAttendanceStatisticsAsync(int departmentId, int year, int month);
        
        /// <summary>
        /// 获取公司考勤统计
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>公司考勤统计</returns>
        Task<CompanyAttendanceStatisticsDto> GetCompanyAttendanceStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取考勤异常统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>异常统计</returns>
        Task<AttendanceAbnormalStatisticsDto> GetAbnormalAttendanceStatisticsAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取迟到早退统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>迟到早退统计</returns>
        Task<LateEarlyLeaveStatisticsDto> GetLateEarlyLeaveStatisticsAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取考勤趋势分析
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤趋势数据</returns>
        Task<IEnumerable<AttendanceTrendDto>> GetAttendanceTrendAsync(DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region 考勤规则计算
        
        /// <summary>
        /// 计算工作时长
        /// </summary>
        /// <param name="checkInTime">签到时间</param>
        /// <param name="checkOutTime">签退时间</param>
        /// <param name="breakDuration">休息时长（分钟）</param>
        /// <returns>工作时长（小时）</returns>
        Task<decimal> CalculateWorkHoursAsync(DateTime? checkInTime, DateTime? checkOutTime, int breakDuration = 60);
        
        /// <summary>
        /// 计算加班时长
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="checkInTime">签到时间</param>
        /// <param name="checkOutTime">签退时间</param>
        /// <returns>加班时长（小时）</returns>
        Task<decimal> CalculateOvertimeHoursAsync(int employeeId, DateTime attendanceDate, DateTime? checkInTime, DateTime? checkOutTime);
        
        /// <summary>
        /// 判断是否迟到
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="checkInTime">签到时间</param>
        /// <returns>是否迟到及迟到分钟数</returns>
        Task<(bool IsLate, int LateMinutes)> CheckLateAsync(int employeeId, DateTime attendanceDate, DateTime? checkInTime);
        
        /// <summary>
        /// 判断是否早退
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="checkOutTime">签退时间</param>
        /// <returns>是否早退及早退分钟数</returns>
        Task<(bool IsEarlyLeave, int EarlyLeaveMinutes)> CheckEarlyLeaveAsync(int employeeId, DateTime attendanceDate, DateTime? checkOutTime);
        
        /// <summary>
        /// 计算考勤状态
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="checkInTime">签到时间</param>
        /// <param name="checkOutTime">签退时间</param>
        /// <returns>考勤状态</returns>
        Task<string> CalculateAttendanceStatusAsync(int employeeId, DateTime attendanceDate, DateTime? checkInTime, DateTime? checkOutTime);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量导入考勤记录
        /// </summary>
        /// <param name="attendanceRecords">考勤记录列表</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>导入结果</returns>
        Task<AttendanceImportResultDto> BatchImportAttendanceAsync(IEnumerable<AttendanceRecord> attendanceRecords, string operatedBy);
        
        /// <summary>
        /// 批量计算考勤统计
        /// </summary>
        /// <param name="employeeIds">员工ID列表</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>计算结果</returns>
        Task<AttendanceCalculationResultDto> BatchCalculateAttendanceAsync(IEnumerable<int> employeeIds, int year, int month, string operatedBy);
        
        /// <summary>
        /// 批量更新考勤状态
        /// </summary>
        /// <param name="attendanceIds">考勤记录ID列表</param>
        /// <param name="newStatus">新状态</param>
        /// <param name="operatedBy">操作人</param>
        /// <returns>更新的数量</returns>
        Task<int> BatchUpdateAttendanceStatusAsync(IEnumerable<int> attendanceIds, string newStatus, string operatedBy);
        
        #endregion
        
        #region 考勤报表
        
        /// <summary>
        /// 生成员工考勤报表
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>员工考勤报表</returns>
        Task<EmployeeAttendanceReportDto> GenerateEmployeeAttendanceReportAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 生成部门考勤报表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>部门考勤报表</returns>
        Task<DepartmentAttendanceReportDto> GenerateDepartmentAttendanceReportAsync(int departmentId, int year, int month);
        
        /// <summary>
        /// 生成考勤异常报表
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="abnormalTypes">异常类型列表</param>
        /// <returns>异常报表</returns>
        Task<AttendanceAbnormalReportDto> GenerateAbnormalAttendanceReportAsync(DateTime startDate, DateTime endDate, IEnumerable<string>? abnormalTypes = null);
        
        #endregion
        
        #region 考勤数据验证
        
        /// <summary>
        /// 验证考勤记录
        /// </summary>
        /// <param name="attendanceRecord">考勤记录</param>
        /// <returns>验证结果</returns>
        Task<ValidationResult> ValidateAttendanceRecordAsync(AttendanceRecord attendanceRecord);
        
        /// <summary>
        /// 检查考勤数据完整性
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>完整性检查结果</returns>
        Task<AttendanceIntegrityCheckResult> CheckAttendanceIntegrityAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 检查重复打卡
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="checkTime">打卡时间</param>
        /// <param name="checkType">打卡类型</param>
        /// <returns>是否重复</returns>
        Task<bool> CheckDuplicateCheckAsync(int employeeId, DateTime attendanceDate, DateTime checkTime, string checkType);
        
        #endregion
        
        #region 考勤规则配置
        
        /// <summary>
        /// 获取员工考勤规则
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>考勤规则</returns>
        Task<AttendanceRule> GetEmployeeAttendanceRuleAsync(int employeeId);
        
        /// <summary>
        /// 获取部门考勤规则
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>考勤规则</returns>
        Task<AttendanceRule> GetDepartmentAttendanceRuleAsync(int departmentId);
        
        /// <summary>
        /// 获取默认考勤规则
        /// </summary>
        /// <returns>默认考勤规则</returns>
        Task<AttendanceRule> GetDefaultAttendanceRuleAsync();
        
        #endregion
        
        #region 考勤假期管理
        
        /// <summary>
        /// 获取员工请假记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>请假记录列表</returns>
        Task<IEnumerable<LeaveRecord>> GetEmployeeLeaveRecordsAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 检查是否为工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否为工作日</returns>
        Task<bool> IsWorkingDayAsync(DateTime date);
        
        /// <summary>
        /// 检查是否为节假日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否为节假日</returns>
        Task<bool> IsHolidayAsync(DateTime date);
        
        #endregion
    }
    
    /// <summary>
    /// 考勤趋势DTO
    /// </summary>
    public class AttendanceTrendDto
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }
        
        /// <summary>
        /// 迟到率
        /// </summary>
        public decimal LateRate { get; set; }
        
        /// <summary>
        /// 早退率
        /// </summary>
        public decimal EarlyLeaveRate { get; set; }
        
        /// <summary>
        /// 缺勤率
        /// </summary>
        public decimal AbsenceRate { get; set; }
        
        /// <summary>
        /// 加班率
        /// </summary>
        public decimal OvertimeRate { get; set; }
    }
    
    /// <summary>
    /// 迟到早退统计DTO
    /// </summary>
    public class LateEarlyLeaveStatisticsDto
    {
        /// <summary>
        /// 迟到总次数
        /// </summary>
        public int TotalLateCount { get; set; }
        
        /// <summary>
        /// 早退总次数
        /// </summary>
        public int TotalEarlyLeaveCount { get; set; }
        
        /// <summary>
        /// 迟到总时长（分钟）
        /// </summary>
        public int TotalLateMinutes { get; set; }
        
        /// <summary>
        /// 早退总时长（分钟）
        /// </summary>
        public int TotalEarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 平均迟到时长（分钟）
        /// </summary>
        public decimal AverageLateMinutes { get; set; }
        
        /// <summary>
        /// 平均早退时长（分钟）
        /// </summary>
        public decimal AverageEarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 迟到率
        /// </summary>
        public decimal LateRate { get; set; }
        
        /// <summary>
        /// 早退率
        /// </summary>
        public decimal EarlyLeaveRate { get; set; }
    }
    
    /// <summary>
    /// 考勤完整性检查结果
    /// </summary>
    public class AttendanceIntegrityCheckResult
    {
        /// <summary>
        /// 检查是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 应出勤天数
        /// </summary>
        public int ExpectedWorkDays { get; set; }
        
        /// <summary>
        /// 实际考勤记录天数
        /// </summary>
        public int ActualAttendanceDays { get; set; }
        
        /// <summary>
        /// 缺失考勤记录的日期
        /// </summary>
        public List<DateTime> MissingAttendanceDates { get; set; } = new List<DateTime>();
        
        /// <summary>
        /// 异常考勤记录的日期
        /// </summary>
        public List<DateTime> AbnormalAttendanceDates { get; set; } = new List<DateTime>();
        
        /// <summary>
        /// 检查详情
        /// </summary>
        public List<string> CheckDetails { get; set; } = new List<string>();
    }
}