using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Services;
using XTHR.Common.Entities;
using XTHR.Common.Models;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Attendance;
using XTHR.Core.DTOs.Common;
using XTHR.Core.DTOs.Requests;

namespace XTHR.Core.Interfaces
{
    /// <summary>
    /// 考勤管理服务接口
    /// 提供考勤记录管理和统计分析的业务逻辑
    /// </summary>
    public interface IAttendanceService
    {
        #region 考勤记录管理
        
        /// <summary>
        /// 获取指定员工和期间的考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<ApiResult<IEnumerable<XTHR.Common.Models.AttendanceRecord>>> GetAttendanceRecordsAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 添加考勤记录
        /// </summary>
        /// <param name="request">添加考勤记录请求</param>
        /// <returns>添加结果</returns>
        Task<ApiResult<bool>> AddAttendanceRecordAsync(CreateAttendanceRecordRequest request);
        
        /// <summary>
        /// 更新考勤记录
        /// </summary>
        /// <param name="request">更新考勤记录请求</param>
        /// <returns>更新结果</returns>
        Task<ApiResult<bool>> UpdateAttendanceRecordAsync(UpdateAttendanceRecordRequest request);
        
        /// <summary>
        /// 删除考勤记录
        /// </summary>
        /// <param name="recordId">记录ID</param>
        /// <returns>删除结果</returns>
        Task<ApiResult<bool>> DeleteAttendanceRecordAsync(int recordId);
        
        #endregion
        
        #region 员工考勤查询
        
        /// <summary>
        /// 获取员工考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<List<AttendanceRecordListDto>> GetEmployeeAttendanceRecordsAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取员工月度考勤汇总
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>月度考勤汇总</returns>
        Task<MonthlyAttendanceSummaryDto> GetEmployeeMonthlyAttendanceSummaryAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取员工考勤统计
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="period">统计期间</param>
        /// <returns>考勤统计</returns>
        Task<EmployeeAttendanceStatisticsDto> GetEmployeeAttendanceStatisticsAsync(int employeeId, AttendancePeriod period);
        
        #endregion
        
        #region 考勤统计分析
        
        /// <summary>
        /// 获取部门考勤统计
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="period">统计期间</param>
        /// <returns>部门考勤统计</returns>
        Task<DepartmentAttendanceStatisticsDto> GetDepartmentAttendanceStatisticsAsync(string? department, AttendancePeriod period);
        
        /// <summary>
        /// 获取全公司考勤统计
        /// </summary>
        /// <param name="period">统计期间</param>
        /// <returns>公司考勤统计</returns>
        Task<CompanyAttendanceStatisticsDto> GetCompanyAttendanceStatisticsAsync(AttendancePeriod period);
        
        /// <summary>
        /// 获取考勤异常统计
        /// </summary>
        /// <param name="period">统计期间</param>
        /// <returns>异常统计</returns>
        Task<AttendanceAnomalyStatisticsDto> GetAttendanceAnomalyStatisticsAsync(AttendancePeriod period);
        
        /// <summary>
        /// 获取迟到早退统计
        /// </summary>
        /// <param name="period">统计期间</param>
        /// <param name="department">部门（可选）</param>
        /// <returns>迟到早退统计</returns>
        Task<LateEarlyStatisticsDto> GetLateEarlyStatisticsAsync(AttendancePeriod period, string? department = null);
        
        #endregion
        
        #region 考勤规则计算
        
        /// <summary>
        /// 计算工作时长
        /// </summary>
        /// <param name="checkInTime">签到时间</param>
        /// <param name="checkOutTime">签退时间</param>
        /// <param name="breakDuration">休息时长（分钟）</param>
        /// <returns>工作时长（小时）</returns>
        Task<decimal> CalculateWorkHoursAsync(DateTime checkInTime, DateTime checkOutTime, int breakDuration = 60);
        
        /// <summary>
        /// 计算加班时长
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="attendanceDate">考勤日期</param>
        /// <param name="actualWorkHours">实际工作时长</param>
        /// <returns>加班时长</returns>
        Task<OvertimeCalculationResult> CalculateOvertimeHoursAsync(int employeeId, DateTime attendanceDate, decimal actualWorkHours);
        
        /// <summary>
        /// 判断是否迟到
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="checkInTime">签到时间</param>
        /// <returns>迟到信息</returns>
        Task<LateCheckResult> CheckLateAsync(int employeeId, DateTime checkInTime);
        
        /// <summary>
        /// 判断是否早退
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="checkOutTime">签退时间</param>
        /// <returns>早退信息</returns>
        Task<EarlyLeaveResult> CheckEarlyLeaveAsync(int employeeId, DateTime checkOutTime);
        
        /// <summary>
        /// 判断是否全勤
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>全勤判断结果</returns>
        Task<PerfectAttendanceResult> CheckPerfectAttendanceAsync(int employeeId, int year, int month);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量导入考勤记录
        /// </summary>
        /// <param name="request">批量导入请求</param>
        /// <returns>导入结果</returns>
        Task<ApiResult<AttendanceImportDto>> BatchImportAttendanceAsync(AttendanceImportRequest request);
        
        /// <summary>
        /// 批量计算考勤统计
        /// </summary>
        /// <param name="employeeIds">员工ID列表</param>
        /// <param name="period">统计期间</param>
        /// <returns>批量计算结果</returns>
        Task<BatchCalculationResult<AttendanceStatisticsDto>> BatchCalculateAttendanceStatisticsAsync(List<int> employeeIds, AttendancePeriod period);
        
        /// <summary>
        /// 批量更新考勤状态
        /// </summary>
        /// <param name="attendanceIds">考勤记录ID列表</param>
        /// <param name="status">新状态</param>
        /// <returns>更新结果</returns>
        Task<ServiceResult> BatchUpdateAttendanceStatusAsync(List<int> attendanceIds, string status);
        
        #endregion
        
        #region 考勤报表
        
        /// <summary>
        /// 生成员工考勤报表
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="period">报表期间</param>
        /// <returns>考勤报表</returns>
        Task<EmployeeAttendanceReportDto> GenerateEmployeeAttendanceReportAsync(int employeeId, AttendancePeriod period);
        
        /// <summary>
        /// 生成部门考勤报表
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="period">报表期间</param>
        /// <returns>部门考勤报表</returns>
        Task<DepartmentAttendanceReportDto> GenerateDepartmentAttendanceReportAsync(string department, AttendancePeriod period);
        
        /// <summary>
        /// 生成考勤异常报表
        /// </summary>
        /// <param name="period">报表期间</param>
        /// <param name="anomalyTypes">异常类型</param>
        /// <returns>异常报表</returns>
        Task<AttendanceAnomalyReportDto> GenerateAttendanceAnomalyReportAsync(AttendancePeriod period, List<string>? anomalyTypes = null);
        
        #endregion
        
        #region 数据验证
        
        /// <summary>
        /// 验证考勤记录
        /// </summary>
        /// <param name="record">考勤记录</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.SystemConfig.SystemConfigValidationResult> ValidateAttendanceRecordAsync(AttendanceRecord record);
        
        /// <summary>
        /// 检查考勤数据完整性
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="period">检查期间</param>
        /// <returns>完整性检查结果</returns>
        Task<AttendanceDataIntegrityResult> CheckAttendanceDataIntegrityAsync(int employeeId, AttendancePeriod period);
        
        #endregion
    }
    
    /// <summary>
    /// 考勤期间
    /// </summary>
    public class AttendancePeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PeriodType { get; set; } // Daily, Weekly, Monthly, Yearly
        
        public static AttendancePeriod CreateMonthly(int year, int month)
        {
            return new AttendancePeriod
            {
                StartDate = new DateTime(year, month, 1),
                EndDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)),
                PeriodType = "Monthly"
            };
        }
        
        public static AttendancePeriod CreateYearly(int year)
        {
            return new AttendancePeriod
            {
                StartDate = new DateTime(year, 1, 1),
                EndDate = new DateTime(year, 12, 31),
                PeriodType = "Yearly"
            };
        }
    }
}