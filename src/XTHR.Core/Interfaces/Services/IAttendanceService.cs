using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Attendance;
using XTHR.Core.DTOs.Employee;
using XTHR.Core.DTOs.Requests;
using XTHR.Core.Entities;

namespace XTHR.Core.Interfaces.Services
{
    /// <summary>
    /// 考勤管理服务接口
    /// </summary>
    public interface IAttendanceService : IBaseService<AttendanceRecord, int, AttendanceRecordDetailDto, AttendanceRecordListDto, CreateAttendanceRecordRequest, UpdateAttendanceRecordRequest>
    {
        #region 考勤记录管理
        
        /// <summary>
        /// 员工打卡
        /// </summary>
        /// <param name="request">打卡请求</param>
        /// <returns>打卡结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendancePunchResult>> PunchAsync(AttendancePunchRequest request);
        
        /// <summary>
        /// 获取员工考勤记录
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>考勤记录</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<AttendanceRecordListDto>>> GetAttendanceRecordsAsync(AttendanceRecordQueryRequest request);
        
        /// <summary>
        /// 获取员工当日考勤状态
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>考勤状态</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeDailyAttendanceDto>> GetEmployeeDailyAttendanceAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 批量创建考勤记录
        /// </summary>
        /// <param name="request">批量创建请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchCreateAttendanceResult>> BatchCreateAttendanceRecordsAsync(BatchCreateAttendanceRecordRequest request);
        
        /// <summary>
        /// 批量更新考勤状态
        /// </summary>
        /// <param name="request">批量更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchUpdateResult>> BatchUpdateAttendanceStatusAsync(BatchUpdateAttendanceStatusRequest request);
        
        #endregion
        
        #region 员工考勤查询
        
        /// <summary>
        /// 获取员工考勤查询
        /// </summary>
        /// <param name="request">查询请求</param>
        /// <returns>考勤查询结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<EmployeeAttendanceDto>>> GetEmployeeAttendanceAsync(EmployeeAttendanceQueryRequest request);
        
        /// <summary>
        /// 获取员工月度考勤汇总
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>月度考勤汇总</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeMonthlyAttendanceSummaryDto>> GetEmployeeMonthlyAttendanceAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取员工考勤统计
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeAttendanceStatisticsDto>> GetEmployeeAttendanceStatisticsAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 检查员工是否全勤
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>是否全勤</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsEmployeePerfectAttendanceAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取员工异常考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>异常考勤记录</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<AttendanceAnomalyDto>>> GetEmployeeAttendanceAnomaliesAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region 考勤统计分析
        
        /// <summary>
        /// 获取考勤统计
        /// </summary>
        /// <param name="request">统计请求</param>
        /// <returns>考勤统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceStatisticsDto>> GetAttendanceStatisticsAsync(AttendanceStatisticsRequest request);
        
        /// <summary>
        /// 获取部门考勤统计
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>部门考勤统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<DepartmentAttendanceStatisticsDto>> GetDepartmentAttendanceStatisticsAsync(int departmentId, int year, int month);
        
        /// <summary>
        /// 获取公司考勤统计
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>公司考勤统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<CompanyAttendanceStatisticsDto>> GetCompanyAttendanceStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取考勤异常统计
        /// </summary>
        /// <param name="request">统计请求</param>
        /// <returns>异常统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceAnomalyStatisticsDto>> GetAttendanceAnomalyStatisticsAsync(AttendanceStatisticsRequest request);
        
        /// <summary>
        /// 获取迟到早退统计
        /// </summary>
        /// <param name="request">统计请求</param>
        /// <returns>迟到早退统计</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<LateEarlyLeaveStatisticsDto>> GetLateEarlyLeaveStatisticsAsync(AttendanceStatisticsRequest request);
        
        /// <summary>
        /// 获取考勤趋势分析
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="groupBy">分组方式</param>
        /// <returns>趋势分析</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<AttendanceTrendDto>>> GetAttendanceTrendAnalysisAsync(DateTime startDate, DateTime endDate, TrendGroupBy groupBy = TrendGroupBy.Month);
        
        #endregion
        
        #region 考勤计算
        
        /// <summary>
        /// 批量计算考勤
        /// </summary>
        /// <param name="request">计算请求</param>
        /// <returns>计算结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BatchAttendanceCalculationResult>> BatchCalculateAttendanceAsync(BatchAttendanceCalculationRequest request);
        
        /// <summary>
        /// 计算员工工作时长
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>工作时长（分钟）</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> CalculateWorkingHoursAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 计算员工加班时长
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>加班时长（分钟）</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> CalculateOvertimeHoursAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 判断员工是否迟到
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="punchTime">打卡时间</param>
        /// <returns>是否迟到及迟到分钟数</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<LateCheckResult>> CheckLateAsync(int employeeId, DateTime punchTime);
        
        /// <summary>
        /// 判断员工是否早退
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="punchTime">打卡时间</param>
        /// <returns>是否早退及早退分钟数</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EarlyLeaveCheckResult>> CheckEarlyLeaveAsync(int employeeId, DateTime punchTime);
        
        /// <summary>
        /// 计算考勤状态
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>考勤状态</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceStatus>> CalculateAttendanceStatusAsync(int employeeId, DateTime date);
        
        #endregion
        
        #region 考勤导入
        
        /// <summary>
        /// 导入考勤数据
        /// </summary>
        /// <param name="request">导入请求</param>
        /// <returns>导入结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceImportResultDto>> ImportAttendanceDataAsync(AttendanceImportRequest request);
        
        /// <summary>
        /// 验证导入的考勤数据
        /// </summary>
        /// <param name="data">导入数据</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceImportValidationResult>> ValidateImportDataAsync(IEnumerable<AttendanceImportData> data);
        
        /// <summary>
        /// 生成考勤导入模板
        /// </summary>
        /// <param name="format">文件格式</param>
        /// <returns>模板文件</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<object>>> GenerateAttendanceImportTemplateAsync(ExportFormat format = ExportFormat.Excel);
        
        #endregion
        
        #region 考勤审核
        
        /// <summary>
        /// 提交考勤审核
        /// </summary>
        /// <param name="request">审核请求</param>
        /// <returns>提交结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SubmitAttendanceForApprovalAsync(AttendanceApprovalRequest request);
        
        /// <summary>
        /// 审批考勤
        /// </summary>
        /// <param name="request">审批请求</param>
        /// <returns>审批结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ApproveAttendanceAsync(AttendanceApprovalRequest request);
        
        /// <summary>
        /// 拒绝考勤审批
        /// </summary>
        /// <param name="attendanceId">考勤记录ID</param>
        /// <param name="reason">拒绝原因</param>
        /// <returns>拒绝结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> RejectAttendanceAsync(int attendanceId, string reason);
        
        /// <summary>
        /// 获取待审核考勤列表
        /// </summary>
        /// <param name="approverId">审批人ID（可选）</param>
        /// <returns>待审核列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<AttendanceApprovalListDto>>> GetPendingAttendanceApprovalsAsync(int? approverId = null);
        
        #endregion
        
        #region 考勤报表
        
        /// <summary>
        /// 生成考勤报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>考勤报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceReportDto>> GenerateAttendanceReportAsync(AttendanceReportRequest request);
        
        /// <summary>
        /// 生成员工考勤报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>员工考勤报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<EmployeeAttendanceReportDto>> GenerateEmployeeAttendanceReportAsync(AttendanceReportRequest request);
        
        /// <summary>
        /// 生成部门考勤报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>部门考勤报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<DepartmentAttendanceReportDto>> GenerateDepartmentAttendanceReportAsync(AttendanceReportRequest request);
        
        /// <summary>
        /// 生成考勤异常报表
        /// </summary>
        /// <param name="request">报表请求</param>
        /// <returns>异常报表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceAnomalyReportDto>> GenerateAttendanceAnomalyReportAsync(AttendanceReportRequest request);
        
        /// <summary>
        /// 导出考勤数据
        /// </summary>
        /// <param name="request">导出请求</param>
        /// <returns>导出结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<AttendanceRecordListDto>>> ExportAttendanceDataAsync(AttendanceExportRequest request);
        
        #endregion
        
        #region 考勤数据验证
        
        /// <summary>
        /// 验证考勤记录
        /// </summary>
        /// <param name="request">验证请求</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceValidationResult>> ValidateAttendanceRecordAsync(AttendanceValidationRequest request);
        
        /// <summary>
        /// 检查考勤数据完整性
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="departmentId">部门ID（可选）</param>
        /// <returns>完整性检查结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceIntegrityCheckResult>> CheckAttendanceDataIntegrityAsync(int year, int month, int? departmentId = null);
        
        /// <summary>
        /// 检查重复打卡
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="punchTime">打卡时间</param>
        /// <param name="punchType">打卡类型</param>
        /// <returns>是否重复打卡</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> CheckDuplicatePunchAsync(int employeeId, DateTime punchTime, AttendancePunchType punchType);
        
        #endregion
        
        #region 考勤规则配置
        
        /// <summary>
        /// 获取员工考勤规则
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>考勤规则</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceRuleDto>> GetEmployeeAttendanceRuleAsync(int employeeId);
        
        /// <summary>
        /// 获取部门考勤规则
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns>考勤规则</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceRuleDto>> GetDepartmentAttendanceRuleAsync(int departmentId);
        
        /// <summary>
        /// 获取默认考勤规则
        /// </summary>
        /// <returns>默认考勤规则</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceRuleDto>> GetDefaultAttendanceRuleAsync();
        
        /// <summary>
        /// 创建考勤规则
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceRuleDto>> CreateAttendanceRuleAsync(CreateAttendanceRuleRequest request);
        
        /// <summary>
        /// 更新考勤规则
        /// </summary>
        /// <param name="ruleId">规则ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceRuleDto>> UpdateAttendanceRuleAsync(int ruleId, UpdateAttendanceRuleRequest request);
        
        #endregion
        
        #region 考勤假期管理
        
        /// <summary>
        /// 获取员工请假记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>请假记录</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<LeaveRecordDto>>> GetEmployeeLeaveRecordsAsync(int employeeId, DateTime? startDate = null, DateTime? endDate = null);
        
        /// <summary>
        /// 判断是否为工作日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否为工作日</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsWorkingDayAsync(DateTime date);
        
        /// <summary>
        /// 判断是否为节假日
        /// </summary>
        /// <param name="date">日期</param>
        /// <returns>是否为节假日</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> IsHolidayAsync(DateTime date);
        
        /// <summary>
        /// 获取工作日历
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>工作日历</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<WorkingCalendarDto>> GetWorkingCalendarAsync(int year, int month);
        
        #endregion
        
        #region 考勤设备管理
        
        /// <summary>
        /// 获取考勤设备列表
        /// </summary>
        /// <returns>设备列表</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<AttendanceDeviceDto>>> GetAttendanceDevicesAsync();
        
        /// <summary>
        /// 同步考勤设备数据
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>同步结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceDeviceSyncResult>> SyncAttendanceDeviceDataAsync(int deviceId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取设备同步状态
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <returns>同步状态</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<AttendanceDeviceSyncStatus>> GetDeviceSyncStatusAsync(int deviceId);
        
        #endregion
    }
    
    /// <summary>
    /// 打卡结果
    /// </summary>
    public class AttendancePunchResult
    {
        /// <summary>
        /// 打卡是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 打卡时间
        /// </summary>
        public DateTime PunchTime { get; set; }
        
        /// <summary>
        /// 打卡类型
        /// </summary>
        public AttendancePunchType PunchType { get; set; }
        
        /// <summary>
        /// 考勤状态
        /// </summary>
        public AttendanceStatus Status { get; set; }
        
        /// <summary>
        /// 是否迟到
        /// </summary>
        public bool IsLate { get; set; }
        
        /// <summary>
        /// 迟到分钟数
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// 是否早退
        /// </summary>
        public bool IsEarlyLeave { get; set; }
        
        /// <summary>
        /// 早退分钟数
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
    
    /// <summary>
    /// 批量创建考勤结果
    /// </summary>
    public class BatchCreateAttendanceResult
    {
        /// <summary>
        /// 创建是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功创建数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败创建数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 创建的考勤记录
        /// </summary>
        public List<AttendanceRecordDetailDto> CreatedRecords { get; set; } = new List<AttendanceRecordDetailDto>();
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<BatchCreateAttendanceFailure> Failures { get; set; } = new List<BatchCreateAttendanceFailure>();
    }
    
    /// <summary>
    /// 批量创建考勤失败记录
    /// </summary>
    public class BatchCreateAttendanceFailure
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
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }
        
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
    /// 批量考勤计算结果
    /// </summary>
    public class BatchAttendanceCalculationResult
    {
        /// <summary>
        /// 计算是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功计算数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败计算数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 计算结果
        /// </summary>
        public List<AttendanceCalculationResult> Results { get; set; } = new List<AttendanceCalculationResult>();
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<BatchAttendanceCalculationFailure> Failures { get; set; } = new List<BatchAttendanceCalculationFailure>();
    }
    
    /// <summary>
    /// 考勤计算结果
    /// </summary>
    public class AttendanceCalculationResult
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }
        
        /// <summary>
        /// 工作时长（分钟）
        /// </summary>
        public int WorkingMinutes { get; set; }
        
        /// <summary>
        /// 加班时长（分钟）
        /// </summary>
        public int OvertimeMinutes { get; set; }
        
        /// <summary>
        /// 考勤状态
        /// </summary>
        public AttendanceStatus Status { get; set; }
        
        /// <summary>
        /// 是否迟到
        /// </summary>
        public bool IsLate { get; set; }
        
        /// <summary>
        /// 迟到分钟数
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// 是否早退
        /// </summary>
        public bool IsEarlyLeave { get; set; }
        
        /// <summary>
        /// 早退分钟数
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }
    }
    
    /// <summary>
    /// 批量考勤计算失败记录
    /// </summary>
    public class BatchAttendanceCalculationFailure
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
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 迟到检查结果
    /// </summary>
    public class LateCheckResult
    {
        /// <summary>
        /// 是否迟到
        /// </summary>
        public bool IsLate { get; set; }
        
        /// <summary>
        /// 迟到分钟数
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// 应到时间
        /// </summary>
        public DateTime ExpectedTime { get; set; }
        
        /// <summary>
        /// 实际到达时间
        /// </summary>
        public DateTime ActualTime { get; set; }
    }
    
    /// <summary>
    /// 早退检查结果
    /// </summary>
    public class EarlyLeaveCheckResult
    {
        /// <summary>
        /// 是否早退
        /// </summary>
        public bool IsEarlyLeave { get; set; }
        
        /// <summary>
        /// 早退分钟数
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 应离时间
        /// </summary>
        public DateTime ExpectedTime { get; set; }
        
        /// <summary>
        /// 实际离开时间
        /// </summary>
        public DateTime ActualTime { get; set; }
    }
    
    /// <summary>
    /// 考勤导入验证结果
    /// </summary>
    public class AttendanceImportValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 有效记录数
        /// </summary>
        public int ValidRecords { get; set; }
        
        /// <summary>
        /// 无效记录数
        /// </summary>
        public int InvalidRecords { get; set; }
        
        /// <summary>
        /// 验证错误
        /// </summary>
        public List<AttendanceImportValidationError> Errors { get; set; } = new List<AttendanceImportValidationError>();
        
        /// <summary>
        /// 验证警告
        /// </summary>
        public List<AttendanceImportValidationWarning> Warnings { get; set; } = new List<AttendanceImportValidationWarning>();
    }
    
    /// <summary>
    /// 考勤导入验证错误
    /// </summary>
    public class AttendanceImportValidationError
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
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
        
        /// <summary>
        /// 原始数据
        /// </summary>
        public object OriginalData { get; set; }
    }
    
    /// <summary>
    /// 考勤导入验证警告
    /// </summary>
    public class AttendanceImportValidationWarning
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
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
    /// 考勤验证结果
    /// </summary>
    public class AttendanceValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 验证错误
        /// </summary>
        public List<AttendanceValidationError> Errors { get; set; } = new List<AttendanceValidationError>();
        
        /// <summary>
        /// 验证警告
        /// </summary>
        public List<AttendanceValidationWarning> Warnings { get; set; } = new List<AttendanceValidationWarning>();
    }
    
    /// <summary>
    /// 考勤验证错误
    /// </summary>
    public class AttendanceValidationError
    {
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
        
        /// <summary>
        /// 错误值
        /// </summary>
        public object ErrorValue { get; set; }
    }
    
    /// <summary>
    /// 考勤验证警告
    /// </summary>
    public class AttendanceValidationWarning
    {
        /// <summary>
        /// 警告类型
        /// </summary>
        public string WarningType { get; set; }
        
        /// <summary>
        /// 警告信息
        /// </summary>
        public string WarningMessage { get; set; }
        
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
    }
    
    /// <summary>
    /// 考勤设备同步结果
    /// </summary>
    public class AttendanceDeviceSyncResult
    {
        /// <summary>
        /// 同步是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 同步记录数
        /// </summary>
        public int SyncedRecords { get; set; }
        
        /// <summary>
        /// 新增记录数
        /// </summary>
        public int NewRecords { get; set; }
        
        /// <summary>
        /// 更新记录数
        /// </summary>
        public int UpdatedRecords { get; set; }
        
        /// <summary>
        /// 同步开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 同步结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 同步耗时
        /// </summary>
        public TimeSpan Duration => EndTime - StartTime;
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 考勤设备同步状态
    /// </summary>
    public class AttendanceDeviceSyncStatus
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceId { get; set; }
        
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        
        /// <summary>
        /// 同步状态
        /// </summary>
        public DeviceSyncStatus Status { get; set; }
        
        /// <summary>
        /// 最后同步时间
        /// </summary>
        public DateTime? LastSyncTime { get; set; }
        
        /// <summary>
        /// 下次同步时间
        /// </summary>
        public DateTime? NextSyncTime { get; set; }
        
        /// <summary>
        /// 同步进度（百分比）
        /// </summary>
        public int Progress { get; set; }
        
        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusDescription { get; set; }
    }
    
    /// <summary>
    /// 设备同步状态
    /// </summary>
    public enum DeviceSyncStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle,
        
        /// <summary>
        /// 同步中
        /// </summary>
        Syncing,
        
        /// <summary>
        /// 同步完成
        /// </summary>
        Completed,
        
        /// <summary>
        /// 同步失败
        /// </summary>
        Failed,
        
        /// <summary>
        /// 设备离线
        /// </summary>
        Offline
    }
}