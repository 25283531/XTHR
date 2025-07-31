using System;
using System.Collections.Generic;
using XTHR.Core.DTOs.Attendance;
using XTHR.Core.DTOs.Employee;
using XTHR.Core.DTOs.Requests;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 考勤记录详情DTO
    /// </summary>
    public class AttendanceRecordDetailDto : BaseDto
    {
        /// <summary>
        /// 员工信息
        /// </summary>
        public EmployeeListDto Employee { get; set; }
        
        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }
        
        /// <summary>
        /// 上班时间
        /// </summary>
        public TimeSpan? CheckInTime { get; set; }
        
        /// <summary>
        /// 下班时间
        /// </summary>
        public TimeSpan? CheckOutTime { get; set; }
        
        /// <summary>
        /// 实际工作时长（小时）
        /// </summary>
        public decimal ActualWorkHours { get; set; }
        
        /// <summary>
        /// 标准工作时长（小时）
        /// </summary>
        public decimal StandardWorkHours { get; set; }
        
        /// <summary>
        /// 加班时长（小时）
        /// </summary>
        public decimal OvertimeHours { get; set; }
        
        /// <summary>
        /// 迟到时长（分钟）
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// 早退时长（分钟）
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 考勤状态
        /// </summary>
        public string AttendanceStatus { get; set; }
        
        /// <summary>
        /// 是否全勤
        /// </summary>
        public bool IsFullAttendance { get; set; }
        
        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }
        
        /// <summary>
        /// 异常说明
        /// </summary>
        public string ExceptionDescription { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; }
        
        /// <summary>
        /// 审核人
        /// </summary>
        public string ApprovedBy { get; set; }
        
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovedAt { get; set; }
    }
    
    /// <summary>
    /// 考勤记录列表DTO
    /// </summary>
    public class AttendanceRecordListDto
    {
        /// <summary>
        /// ID
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
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }
        
        /// <summary>
        /// 上班时间
        /// </summary>
        public TimeSpan? CheckInTime { get; set; }
        
        /// <summary>
        /// 下班时间
        /// </summary>
        public TimeSpan? CheckOutTime { get; set; }
        
        /// <summary>
        /// 实际工作时长
        /// </summary>
        public decimal ActualWorkHours { get; set; }
        
        /// <summary>
        /// 加班时长
        /// </summary>
        public decimal OvertimeHours { get; set; }
        
        /// <summary>
        /// 迟到时长（分钟）
        /// </summary>
        public int LateMinutes { get; set; }
        
        /// <summary>
        /// 早退时长（分钟）
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 考勤状态
        /// </summary>
        public string AttendanceStatus { get; set; }
        
        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }
        
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; }
    }
    
    /// <summary>
    /// 员工月度考勤汇总DTO
    /// </summary>
    public class EmployeeMonthlyAttendanceDto
    {
        /// <summary>
        /// 员工信息
        /// </summary>
        public EmployeeListDto Employee { get; set; }
        
        /// <summary>
        /// 考勤期间
        /// </summary>
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 应出勤天数
        /// </summary>
        public int ExpectedWorkDays { get; set; }
        
        /// <summary>
        /// 实际出勤天数
        /// </summary>
        public int ActualWorkDays { get; set; }
        
        /// <summary>
        /// 缺勤天数
        /// </summary>
        public int AbsentDays { get; set; }
        
        /// <summary>
        /// 迟到次数
        /// </summary>
        public int LateCount { get; set; }
        
        /// <summary>
        /// 早退次数
        /// </summary>
        public int EarlyLeaveCount { get; set; }
        
        /// <summary>
        /// 总迟到时长（分钟）
        /// </summary>
        public int TotalLateMinutes { get; set; }
        
        /// <summary>
        /// 总早退时长（分钟）
        /// </summary>
        public int TotalEarlyLeaveMinutes { get; set; }
        
        /// <summary>
        /// 总工作时长（小时）
        /// </summary>
        public decimal TotalWorkHours { get; set; }
        
        /// <summary>
        /// 总加班时长（小时）
        /// </summary>
        public decimal TotalOvertimeHours { get; set; }
        
        /// <summary>
        /// 是否全勤
        /// </summary>
        public bool IsFullAttendance { get; set; }
        
        /// <summary>
        /// 出勤率
        /// </summary>
        public decimal AttendanceRate { get; set; }
        
        /// <summary>
        /// 异常考勤次数
        /// </summary>
        public int ExceptionCount { get; set; }
        
        /// <summary>
        /// 考勤明细
        /// </summary>
        public List<AttendanceRecordListDto> AttendanceDetails { get; set; } = new List<AttendanceRecordListDto>();
    }
    
    /// <summary>
    /// 考勤统计DTO
    /// </summary>
    public class AttendanceStatisticsDto
    {
        /// <summary>
        /// 统计期间
        /// </summary>
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 员工总数
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// 正常出勤员工数
        /// </summary>
        public int NormalAttendanceEmployees { get; set; }
        
        /// <summary>
        /// 异常考勤员工数
        /// </summary>
        public int ExceptionAttendanceEmployees { get; set; }
        
        /// <summary>
        /// 全勤员工数
        /// </summary>
        public int FullAttendanceEmployees { get; set; }
        
        /// <summary>
        /// 总出勤率
        /// </summary>
        public decimal OverallAttendanceRate { get; set; }
        
        /// <summary>
        /// 总迟到次数
        /// </summary>
        public int TotalLateCount { get; set; }
        
        /// <summary>
        /// 总早退次数
        /// </summary>
        public int TotalEarlyLeaveCount { get; set; }
        
        /// <summary>
        /// 总缺勤天数
        /// </summary>
        public int TotalAbsentDays { get; set; }
        
        /// <summary>
        /// 总工作时长
        /// </summary>
        public decimal TotalWorkHours { get; set; }
        
        /// <summary>
        /// 总加班时长
        /// </summary>
        public decimal TotalOvertimeHours { get; set; }
        
        /// <summary>
        /// 平均工作时长
        /// </summary>
        public decimal AverageWorkHours { get; set; }
        
        /// <summary>
        /// 平均加班时长
        /// </summary>
        public decimal AverageOvertimeHours { get; set; }
        
        /// <summary>
        /// 部门考勤统计
        /// </summary>
        public List<DepartmentAttendanceStatisticsDto> DepartmentStatistics { get; set; } = new List<DepartmentAttendanceStatisticsDto>();
        
        /// <summary>
        /// 异常考勤统计
        /// </summary>
        public List<AttendanceExceptionStatisticsDto> ExceptionStatistics { get; set; } = new List<AttendanceExceptionStatisticsDto>();
    }
    

    
    /// <summary>
    /// 考勤异常统计DTO
    /// </summary>
    public class AttendanceExceptionStatisticsDto
    {
        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }
        
        /// <summary>
        /// 异常次数
        /// </summary>
        public int ExceptionCount { get; set; }
        
        /// <summary>
        /// 涉及员工数
        /// </summary>
        public int AffectedEmployees { get; set; }
        
        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percentage { get; set; }
        
        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; }
    }
    
    /// <summary>
    /// 考勤报表DTO
    /// </summary>
    public class AttendanceReportDto
    {
        /// <summary>
        /// 报表类型
        /// </summary>
        public string ReportType { get; set; }
        
        /// <summary>
        /// 报表期间
        /// </summary>
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime GeneratedAt { get; set; }
        
        /// <summary>
        /// 生成人
        /// </summary>
        public string GeneratedBy { get; set; }
        
        /// <summary>
        /// 报表数据
        /// </summary>
        public object ReportData { get; set; }
        
        /// <summary>
        /// 汇总信息
        /// </summary>
        public AttendanceStatisticsDto Summary { get; set; }
    }
    
    /// <summary>
    /// 员工考勤报表DTO
    /// </summary>
    public class EmployeeAttendanceReportDto : AttendanceReportDto
    {
        /// <summary>
        /// 员工考勤数据
        /// </summary>
        public new List<EmployeeMonthlyAttendanceDto> ReportData { get; set; } = new List<EmployeeMonthlyAttendanceDto>();
    }
    
    /// <summary>
    /// 部门考勤报表DTO
    /// </summary>
    public class DepartmentAttendanceReportDto : AttendanceReportDto
    {
        /// <summary>
        /// 部门考勤数据
        /// </summary>
        public new List<DepartmentAttendanceStatisticsDto> ReportData { get; set; } = new List<DepartmentAttendanceStatisticsDto>();
    }
    
    /// <summary>
    /// 考勤异常报表DTO
    /// </summary>
    public class AttendanceExceptionReportDto : AttendanceReportDto
    {
        /// <summary>
        /// 异常考勤数据
        /// </summary>
        public new List<AttendanceRecordListDto> ReportData { get; set; } = new List<AttendanceRecordListDto>();
        
        /// <summary>
        /// 异常统计
        /// </summary>
        public List<AttendanceExceptionStatisticsDto> ExceptionStatistics { get; set; } = new List<AttendanceExceptionStatisticsDto>();
    }
    
    /// <summary>
    /// 考勤导入结果DTO
    /// </summary>
    public class AttendanceImportResultDto
    {
        /// <summary>
        /// 导入总数
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// 成功导入数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败导入数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 重复记录数
        /// </summary>
        public int DuplicateCount { get; set; }
        
        /// <summary>
        /// 成功导入的记录
        /// </summary>
        public List<AttendanceRecordListDto> SuccessRecords { get; set; } = new List<AttendanceRecordListDto>();
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<AttendanceImportFailure> FailureRecords { get; set; } = new List<AttendanceImportFailure>();
        
        /// <summary>
        /// 导入时间
        /// </summary>
        public DateTime ImportTime { get; set; }
        
        /// <summary>
        /// 导入人
        /// </summary>
        public string ImportedBy { get; set; }
    }
    
    /// <summary>
    /// 考勤导入失败记录
    /// </summary>
    public class AttendanceImportFailure
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 考勤日期
        /// </summary>
        public string AttendanceDate { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> OriginalData { get; set; } = new Dictionary<string, string>();
    }
    
    /// <summary>
    /// 考勤计算结果DTO
    /// </summary>
    public class AttendanceCalculationResultDto
    {
        /// <summary>
        /// 计算期间
        /// </summary>
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 计算员工数
        /// </summary>
        public int CalculatedEmployees { get; set; }
        
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
        public List<EmployeeMonthlyAttendanceDto> CalculationResults { get; set; } = new List<EmployeeMonthlyAttendanceDto>();
        
        /// <summary>
        /// 失败记录
        /// </summary>
        public List<AttendanceCalculationFailure> FailureRecords { get; set; } = new List<AttendanceCalculationFailure>();
        
        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime CalculationTime { get; set; }
        
        /// <summary>
        /// 计算人
        /// </summary>
        public string CalculatedBy { get; set; }
    }
    
    /// <summary>
    /// 考勤计算失败记录
    /// </summary>
    public class AttendanceCalculationFailure
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
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误详情
        /// </summary>
        public List<string> ErrorDetails { get; set; } = new List<string>();
    }
}