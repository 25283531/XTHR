using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XTHR.Core.DTOs.Attendance;

namespace XTHR.Core.DTOs.Requests
{
    /// <summary>
    /// 考勤记录查询请求
    /// </summary>
    public class AttendanceQueryRequest : BaseQueryRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int? EmployeeId { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        
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
        
        /// <summary>
        /// 是否只显示异常记录
        /// </summary>
        public bool? OnlyExceptions { get; set; }
        
        /// <summary>
        /// 是否只显示全勤记录
        /// </summary>
        public bool? OnlyFullAttendance { get; set; }
        
        /// <summary>
        /// 最小工作时长
        /// </summary>
        [Range(0, 24, ErrorMessage = "最小工作时长必须在0-24小时之间")]
        public decimal? MinWorkHours { get; set; }
        
        /// <summary>
        /// 最大工作时长
        /// </summary>
        [Range(0, 24, ErrorMessage = "最大工作时长必须在0-24小时之间")]
        public decimal? MaxWorkHours { get; set; }
        
        /// <summary>
        /// 最小加班时长
        /// </summary>
        [Range(0, 24, ErrorMessage = "最小加班时长必须在0-24小时之间")]
        public decimal? MinOvertimeHours { get; set; }
        
        /// <summary>
        /// 最大加班时长
        /// </summary>
        [Range(0, 24, ErrorMessage = "最大加班时长必须在0-24小时之间")]
        public decimal? MaxOvertimeHours { get; set; }
    }
    
    /// <summary>
    /// 创建考勤记录请求
    /// </summary>
    public class CreateAttendanceRecordRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "员工ID必须大于0")]
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 考勤日期
        /// </summary>
        [Required(ErrorMessage = "考勤日期不能为空")]
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
        /// 考勤状态
        /// </summary>
        [Required(ErrorMessage = "考勤状态不能为空")]
        [StringLength(20, ErrorMessage = "考勤状态长度不能超过20字符")]
        public string AttendanceStatus { get; set; }
        
        /// <summary>
        /// 异常类型
        /// </summary>
        [StringLength(50, ErrorMessage = "异常类型长度不能超过50字符")]
        public string ExceptionType { get; set; }
        
        /// <summary>
        /// 异常说明
        /// </summary>
        [StringLength(500, ErrorMessage = "异常说明长度不能超过500字符")]
        public string ExceptionDescription { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 更新考勤记录请求
    /// </summary>
    public class UpdateAttendanceRecordRequest
    {
        /// <summary>
        /// 考勤记录ID
        /// </summary>
        [Required(ErrorMessage = "考勤记录ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "考勤记录ID必须大于0")]
        public int Id { get; set; }
        
        /// <summary>
        /// 上班时间
        /// </summary>
        public TimeSpan? CheckInTime { get; set; }
        
        /// <summary>
        /// 下班时间
        /// </summary>
        public TimeSpan? CheckOutTime { get; set; }
        
        /// <summary>
        /// 考勤状态
        /// </summary>
        [StringLength(20, ErrorMessage = "考勤状态长度不能超过20字符")]
        public string AttendanceStatus { get; set; }
        
        /// <summary>
        /// 异常类型
        /// </summary>
        [StringLength(50, ErrorMessage = "异常类型长度不能超过50字符")]
        public string ExceptionType { get; set; }
        
        /// <summary>
        /// 异常说明
        /// </summary>
        [StringLength(500, ErrorMessage = "异常说明长度不能超过500字符")]
        public string ExceptionDescription { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 员工考勤查询请求
    /// </summary>
    public class EmployeeAttendanceQueryRequest : DateRangeRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "员工ID必须大于0")]
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 是否包含详细记录
        /// </summary>
        public bool IncludeDetails { get; set; } = true;
        
        /// <summary>
        /// 是否只显示异常记录
        /// </summary>
        public bool OnlyExceptions { get; set; } = false;
    }
    
    /// <summary>
    /// 考勤统计请求
    /// </summary>
    public class AttendanceStatisticsRequest
    {
        /// <summary>
        /// 统计期间
        /// </summary>
        [Required(ErrorMessage = "统计期间不能为空")]
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 部门列表（为空则统计所有部门）
        /// </summary>
        public List<string> Departments { get; set; } = new List<string>();
        
        /// <summary>
        /// 员工ID列表（为空则统计所有员工）
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 统计类型
        /// </summary>
        [Required(ErrorMessage = "统计类型不能为空")]
        [RegularExpression("^(总体|部门|员工|异常)$", ErrorMessage = "统计类型只能是'总体'、'部门'、'员工'或'异常'")]
        public string StatisticsType { get; set; }
        
        /// <summary>
        /// 是否包含部门明细
        /// </summary>
        public bool IncludeDepartmentDetails { get; set; } = true;
        
        /// <summary>
        /// 是否包含异常统计
        /// </summary>
        public bool IncludeExceptionStatistics { get; set; } = true;
    }
    
    /// <summary>
    /// 批量考勤计算请求
    /// </summary>
    public class BatchAttendanceCalculationRequest
    {
        /// <summary>
        /// 计算期间
        /// </summary>
        [Required(ErrorMessage = "计算期间不能为空")]
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 员工ID列表（为空则计算所有员工）
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 部门列表（为空则计算所有部门）
        /// </summary>
        public List<string> Departments { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否重新计算（覆盖已有结果）
        /// </summary>
        public bool ForceRecalculate { get; set; } = false;
        
        /// <summary>
        /// 计算类型
        /// </summary>
        [Required(ErrorMessage = "计算类型不能为空")]
        [RegularExpression("^(工作时长|加班时长|迟到早退|全勤|全部)$", ErrorMessage = "计算类型只能是'工作时长'、'加班时长'、'迟到早退'、'全勤'或'全部'")]
        public string CalculationType { get; set; }
    }
    
    /// <summary>
    /// 考勤导入请求
    /// </summary>
    public class AttendanceImportRequest : BaseImportRequest
    {
        /// <summary>
        /// 导入数据
        /// </summary>
        [Required(ErrorMessage = "导入数据不能为空")]
        [MinLength(1, ErrorMessage = "至少导入一条记录")]
        public List<AttendanceImportData> ImportData { get; set; } = new List<AttendanceImportData>();
        
        /// <summary>
        /// 是否跳过重复记录
        /// </summary>
        public bool SkipDuplicates { get; set; } = true;
        
        /// <summary>
        /// 是否自动计算工作时长
        /// </summary>
        public bool AutoCalculateWorkHours { get; set; } = true;
        
        /// <summary>
        /// 是否自动判断异常
        /// </summary>
        public bool AutoDetectExceptions { get; set; } = true;
    }
    
    /// <summary>
    /// 考勤导入数据
    /// </summary>
    public class AttendanceImportData
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        [Required(ErrorMessage = "员工编号不能为空")]
        [StringLength(20, ErrorMessage = "员工编号长度不能超过20字符")]
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 考勤日期
        /// </summary>
        [Required(ErrorMessage = "考勤日期不能为空")]
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
        /// 考勤状态
        /// </summary>
        [StringLength(20, ErrorMessage = "考勤状态长度不能超过20字符")]
        public string AttendanceStatus { get; set; }
        
        /// <summary>
        /// 异常类型
        /// </summary>
        [StringLength(50, ErrorMessage = "异常类型长度不能超过50字符")]
        public string ExceptionType { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注长度不能超过500字符")]
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 考勤审核请求
    /// </summary>
    public class AttendanceApprovalRequest
    {
        /// <summary>
        /// 考勤记录ID列表
        /// </summary>
        [Required(ErrorMessage = "考勤记录ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一条考勤记录")]
        public List<int> AttendanceRecordIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 审核状态
        /// </summary>
        [Required(ErrorMessage = "审核状态不能为空")]
        [RegularExpression("^(通过|拒绝)$", ErrorMessage = "审核状态只能是'通过'或'拒绝'")]
        public string ApprovalStatus { get; set; }
        
        /// <summary>
        /// 审核意见
        /// </summary>
        [StringLength(1000, ErrorMessage = "审核意见长度不能超过1000字符")]
        public string ApprovalComments { get; set; }
    }
    
    /// <summary>
    /// 考勤报表生成请求
    /// </summary>
    public class AttendanceReportRequest
    {
        /// <summary>
        /// 报表类型
        /// </summary>
        [Required(ErrorMessage = "报表类型不能为空")]
        [RegularExpression("^(员工考勤|部门考勤|异常考勤|考勤汇总)$", ErrorMessage = "报表类型只能是'员工考勤'、'部门考勤'、'异常考勤'或'考勤汇总'")]
        public string ReportType { get; set; }
        
        /// <summary>
        /// 报表期间
        /// </summary>
        [Required(ErrorMessage = "报表期间不能为空")]
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 部门列表（为空则包含所有部门）
        /// </summary>
        public List<string> Departments { get; set; } = new List<string>();
        
        /// <summary>
        /// 员工ID列表（为空则包含所有员工）
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 报表格式
        /// </summary>
        [Required(ErrorMessage = "报表格式不能为空")]
        [RegularExpression("^(Excel|PDF|HTML)$", ErrorMessage = "报表格式只能是'Excel'、'PDF'或'HTML'")]
        public string ReportFormat { get; set; } = "Excel";
        
        /// <summary>
        /// 是否包含详细数据
        /// </summary>
        public bool IncludeDetails { get; set; } = true;
        
        /// <summary>
        /// 是否包含统计图表
        /// </summary>
        public bool IncludeCharts { get; set; } = false;
        
        /// <summary>
        /// 筛选条件
        /// </summary>
        public AttendanceReportFilter Filter { get; set; }
    }
    
    /// <summary>
    /// 考勤报表筛选条件
    /// </summary>
    public class AttendanceReportFilter
    {
        /// <summary>
        /// 考勤状态
        /// </summary>
        public List<string> AttendanceStatuses { get; set; } = new List<string>();
        
        /// <summary>
        /// 异常类型
        /// </summary>
        public List<string> ExceptionTypes { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否只显示异常记录
        /// </summary>
        public bool OnlyExceptions { get; set; } = false;
        
        /// <summary>
        /// 是否只显示全勤记录
        /// </summary>
        public bool OnlyFullAttendance { get; set; } = false;
        
        /// <summary>
        /// 最小工作时长
        /// </summary>
        [Range(0, 24, ErrorMessage = "最小工作时长必须在0-24小时之间")]
        public decimal? MinWorkHours { get; set; }
        
        /// <summary>
        /// 最大工作时长
        /// </summary>
        [Range(0, 24, ErrorMessage = "最大工作时长必须在0-24小时之间")]
        public decimal? MaxWorkHours { get; set; }
        
        /// <summary>
        /// 最小出勤率
        /// </summary>
        [Range(0, 100, ErrorMessage = "最小出勤率必须在0-100之间")]
        public decimal? MinAttendanceRate { get; set; }
        
        /// <summary>
        /// 最大出勤率
        /// </summary>
        [Range(0, 100, ErrorMessage = "最大出勤率必须在0-100之间")]
        public decimal? MaxAttendanceRate { get; set; }
    }
    
    /// <summary>
    /// 考勤数据验证请求
    /// </summary>
    public class AttendanceValidationRequest
    {
        /// <summary>
        /// 验证期间
        /// </summary>
        [Required(ErrorMessage = "验证期间不能为空")]
        public AttendancePeriod Period { get; set; }
        
        /// <summary>
        /// 员工ID列表（为空则验证所有员工）
        /// </summary>
        public List<int> EmployeeIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 验证类型
        /// </summary>
        [Required(ErrorMessage = "验证类型不能为空")]
        [RegularExpression("^(数据完整性|时间逻辑|业务规则|全面验证)$", ErrorMessage = "验证类型只能是'数据完整性'、'时间逻辑'、'业务规则'或'全面验证'")]
        public string ValidationType { get; set; }
        
        /// <summary>
        /// 验证规则
        /// </summary>
        public List<string> ValidationRules { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否自动修复
        /// </summary>
        public bool AutoFix { get; set; } = false;
    }
    
    /// <summary>
    /// 批量更新考勤状态请求
    /// </summary>
    public class BatchUpdateAttendanceStatusRequest
    {
        /// <summary>
        /// 考勤记录ID列表
        /// </summary>
        [Required(ErrorMessage = "考勤记录ID列表不能为空")]
        [MinLength(1, ErrorMessage = "至少选择一条考勤记录")]
        public List<int> AttendanceRecordIds { get; set; } = new List<int>();
        
        /// <summary>
        /// 新的考勤状态
        /// </summary>
        [Required(ErrorMessage = "考勤状态不能为空")]
        [StringLength(20, ErrorMessage = "考勤状态长度不能超过20字符")]
        public string NewStatus { get; set; }
        
        /// <summary>
        /// 更新原因
        /// </summary>
        [Required(ErrorMessage = "更新原因不能为空")]
        [StringLength(500, ErrorMessage = "更新原因长度不能超过500字符")]
        public string UpdateReason { get; set; }
    }
}