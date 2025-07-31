using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 考勤记录仓储接口
    /// </summary>
    public interface IAttendanceRepository : IRepository<AttendanceRecord>
    {
        /// <summary>
        /// 根据员工ID和日期获取考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>考勤记录</returns>
        Task<AttendanceRecord> GetByEmployeeAndDateAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 根据员工ID和日期范围获取考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 根据日期范围获取所有考勤记录
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取员工月度考勤统计
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>考勤统计</returns>
        Task<AttendanceStatistics> GetMonthlyStatisticsAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取部门月度考勤统计
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>考勤统计列表</returns>
        Task<IEnumerable<AttendanceStatistics>> GetDepartmentMonthlyStatisticsAsync(string department, int year, int month);
        
        /// <summary>
        /// 获取异常考勤记录
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="status">考勤状态（可选）</param>
        /// <returns>异常考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetAbnormalRecordsAsync(DateTime startDate, DateTime endDate, string status = null);
        
        /// <summary>
        /// 获取待审批的考勤记录
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>待审批考勤记录列表</returns>
        Task<IEnumerable<AttendanceRecord>> GetPendingApprovalRecordsAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 批量更新考勤记录的审批状态
        /// </summary>
        /// <param name="attendanceIds">考勤记录ID列表</param>
        /// <param name="approvalStatus">审批状态</param>
        /// <param name="approvedBy">审批人</param>
        /// <returns>受影响的行数</returns>
        Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> attendanceIds, string approvalStatus, string approvedBy);
        
        /// <summary>
        /// 检查员工在指定日期是否已有考勤记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>是否存在</returns>
        Task<bool> HasAttendanceRecordAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 获取员工加班统计
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>加班统计</returns>
        Task<OvertimeStatistics> GetOvertimeStatisticsAsync(int employeeId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取迟到早退统计
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>迟到早退统计</returns>
        Task<LateEarlyStatistics> GetLateEarlyStatisticsAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
    
    /// <summary>
    /// 考勤统计信息
    /// </summary>
    public class AttendanceStatistics
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Department { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int WorkDays { get; set; }
        public int ActualWorkDays { get; set; }
        public decimal TotalWorkHours { get; set; }
        public decimal TotalOvertimeHours { get; set; }
        public int LateCount { get; set; }
        public int EarlyLeaveCount { get; set; }
        public int AbsentCount { get; set; }
        public int LeaveCount { get; set; }
        public int BusinessTripCount { get; set; }
        public decimal AttendanceRate { get; set; }
    }
    
    /// <summary>
    /// 加班统计信息
    /// </summary>
    public class OvertimeStatistics
    {
        public int EmployeeId { get; set; }
        public decimal TotalOvertimeHours { get; set; }
        public decimal WeekdayOvertimeHours { get; set; }
        public decimal WeekendOvertimeHours { get; set; }
        public decimal HolidayOvertimeHours { get; set; }
        public int OvertimeDays { get; set; }
    }
    
    /// <summary>
    /// 迟到早退统计信息
    /// </summary>
    public class LateEarlyStatistics
    {
        public int EmployeeId { get; set; }
        public int LateCount { get; set; }
        public int EarlyLeaveCount { get; set; }
        public decimal TotalLateMinutes { get; set; }
        public decimal TotalEarlyLeaveMinutes { get; set; }
        public decimal AverageLateMinutes { get; set; }
        public decimal AverageEarlyLeaveMinutes { get; set; }
    }
    
    /// <summary>
    /// 考勤记录仓储实现类
    /// </summary>
    public class AttendanceRepository : BaseRepository<AttendanceRecord>, IAttendanceRepository
    {
        public AttendanceRepository(IDatabaseService databaseService) 
            : base(databaseService, "AttendanceRecords", "AttendanceId")
        {
        }
        
        #region 重写基类方法
        
        protected override object EntityToInsertParameters(AttendanceRecord entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.AttendanceDate,
                entity.DayOfWeek,
                entity.CheckInTime,
                entity.CheckOutTime,
                entity.WorkHours,
                entity.LateEarlyMinutes,
                entity.OvertimeHours,
                entity.Status,
                entity.LeaveType,
                entity.BusinessTrip,
                entity.FieldWork,
                entity.IsHoliday,
                entity.ExceptionReason,
                entity.ApprovalStatus,
                entity.DataSource,
                entity.RawData,
                entity.Remarks,
                CreatedAt = DateTime.Now,
                CreatedBy = entity.CreatedBy ?? "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = entity.UpdatedBy ?? "System"
            };
        }
        
        protected override object EntityToUpdateParameters(AttendanceRecord entity)
        {
            return new
            {
                entity.AttendanceId,
                entity.EmployeeId,
                entity.AttendanceDate,
                entity.DayOfWeek,
                entity.CheckInTime,
                entity.CheckOutTime,
                entity.WorkHours,
                entity.LateEarlyMinutes,
                entity.OvertimeHours,
                entity.Status,
                entity.LeaveType,
                entity.BusinessTrip,
                entity.FieldWork,
                entity.IsHoliday,
                entity.ExceptionReason,
                entity.ApprovalStatus,
                entity.DataSource,
                entity.RawData,
                entity.Remarks,
                UpdatedAt = DateTime.Now,
                UpdatedBy = entity.UpdatedBy ?? "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO AttendanceRecords (
                    EmployeeId, AttendanceDate, DayOfWeek, CheckInTime, CheckOutTime,
                    WorkHours, LateEarlyMinutes, OvertimeHours, Status, LeaveType,
                    BusinessTrip, FieldWork, IsHoliday, ExceptionReason, ApprovalStatus,
                    DataSource, RawData, Remarks,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeId, @AttendanceDate, @DayOfWeek, @CheckInTime, @CheckOutTime,
                    @WorkHours, @LateEarlyMinutes, @OvertimeHours, @Status, @LeaveType,
                    @BusinessTrip, @FieldWork, @IsHoliday, @ExceptionReason, @ApprovalStatus,
                    @DataSource, @RawData, @Remarks,
                    @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE AttendanceRecords SET 
                    EmployeeId = @EmployeeId,
                    AttendanceDate = @AttendanceDate,
                    DayOfWeek = @DayOfWeek,
                    CheckInTime = @CheckInTime,
                    CheckOutTime = @CheckOutTime,
                    WorkHours = @WorkHours,
                    LateEarlyMinutes = @LateEarlyMinutes,
                    OvertimeHours = @OvertimeHours,
                    Status = @Status,
                    LeaveType = @LeaveType,
                    BusinessTrip = @BusinessTrip,
                    FieldWork = @FieldWork,
                    IsHoliday = @IsHoliday,
                    ExceptionReason = @ExceptionReason,
                    ApprovalStatus = @ApprovalStatus,
                    DataSource = @DataSource,
                    RawData = @RawData,
                    Remarks = @Remarks,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE AttendanceId = @AttendanceId";
        }
        
        protected override string GetSelectByIdSql()
        {
            return "SELECT * FROM AttendanceRecords WHERE AttendanceId = @Id";
        }
        
        protected override string GetDeleteSql()
        {
            return "DELETE FROM AttendanceRecords WHERE AttendanceId = @Id";
        }
        
        #endregion
        
        #region 接口实现
        
        public async Task<AttendanceRecord> GetByEmployeeAndDateAsync(int employeeId, DateTime date)
        {
            const string sql = @"
                SELECT * FROM AttendanceRecords 
                WHERE EmployeeId = @EmployeeId AND AttendanceDate = @Date";
            
            var result = await ExecuteQueryAsync(sql, new { EmployeeId = employeeId, Date = date.Date });
            return result.FirstOrDefault();
        }
        
        public async Task<IEnumerable<AttendanceRecord>> GetByEmployeeAndDateRangeAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM AttendanceRecords 
                WHERE EmployeeId = @EmployeeId 
                    AND AttendanceDate >= @StartDate 
                    AND AttendanceDate <= @EndDate
                ORDER BY AttendanceDate";
            
            return await ExecuteQueryAsync(sql, new { EmployeeId = employeeId, StartDate = startDate.Date, EndDate = endDate.Date });
        }
        
        public async Task<IEnumerable<AttendanceRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT ar.*, e.Name as EmployeeName, e.EmployeeNumber, e.Department 
                FROM AttendanceRecords ar
                INNER JOIN Employees e ON ar.EmployeeId = e.EmployeeId
                WHERE ar.AttendanceDate >= @StartDate 
                    AND ar.AttendanceDate <= @EndDate
                ORDER BY ar.AttendanceDate, e.EmployeeNumber";
            
            return await ExecuteQueryAsync(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });
        }
        
        public async Task<AttendanceStatistics> GetMonthlyStatisticsAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT 
                    ar.EmployeeId,
                    e.Name as EmployeeName,
                    e.EmployeeNumber,
                    e.Department,
                    @Year as Year,
                    @Month as Month,
                    COUNT(*) as WorkDays,
                    COUNT(CASE WHEN ar.Status IN ('正常', '迟到', '早退', '迟到早退') THEN 1 END) as ActualWorkDays,
                    COALESCE(SUM(ar.WorkHours), 0) as TotalWorkHours,
                    COALESCE(SUM(ar.OvertimeHours), 0) as TotalOvertimeHours,
                    COUNT(CASE WHEN ar.Status IN ('迟到', '迟到早退') THEN 1 END) as LateCount,
                    COUNT(CASE WHEN ar.Status IN ('早退', '迟到早退') THEN 1 END) as EarlyLeaveCount,
                    COUNT(CASE WHEN ar.Status = '缺勤' THEN 1 END) as AbsentCount,
                    COUNT(CASE WHEN ar.LeaveType IS NOT NULL THEN 1 END) as LeaveCount,
                    COUNT(CASE WHEN ar.BusinessTrip = 1 THEN 1 END) as BusinessTripCount,
                    CASE WHEN COUNT(*) > 0 THEN 
                        CAST(COUNT(CASE WHEN ar.Status IN ('正常', '迟到', '早退', '迟到早退') THEN 1 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
                    ELSE 0 END as AttendanceRate
                FROM AttendanceRecords ar
                INNER JOIN Employees e ON ar.EmployeeId = e.EmployeeId
                WHERE ar.EmployeeId = @EmployeeId 
                    AND strftime('%Y', ar.AttendanceDate) = @Year
                    AND strftime('%m', ar.AttendanceDate) = @Month
                GROUP BY ar.EmployeeId, e.Name, e.EmployeeNumber, e.Department";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<AttendanceStatistics>(sql, 
                new { EmployeeId = employeeId, Year = year.ToString(), Month = month.ToString("D2") });
        }
        
        public async Task<IEnumerable<AttendanceStatistics>> GetDepartmentMonthlyStatisticsAsync(string department, int year, int month)
        {
            const string sql = @"
                SELECT 
                    ar.EmployeeId,
                    e.Name as EmployeeName,
                    e.EmployeeNumber,
                    e.Department,
                    @Year as Year,
                    @Month as Month,
                    COUNT(*) as WorkDays,
                    COUNT(CASE WHEN ar.Status IN ('正常', '迟到', '早退', '迟到早退') THEN 1 END) as ActualWorkDays,
                    COALESCE(SUM(ar.WorkHours), 0) as TotalWorkHours,
                    COALESCE(SUM(ar.OvertimeHours), 0) as TotalOvertimeHours,
                    COUNT(CASE WHEN ar.Status IN ('迟到', '迟到早退') THEN 1 END) as LateCount,
                    COUNT(CASE WHEN ar.Status IN ('早退', '迟到早退') THEN 1 END) as EarlyLeaveCount,
                    COUNT(CASE WHEN ar.Status = '缺勤' THEN 1 END) as AbsentCount,
                    COUNT(CASE WHEN ar.LeaveType IS NOT NULL THEN 1 END) as LeaveCount,
                    COUNT(CASE WHEN ar.BusinessTrip = 1 THEN 1 END) as BusinessTripCount,
                    CASE WHEN COUNT(*) > 0 THEN 
                        CAST(COUNT(CASE WHEN ar.Status IN ('正常', '迟到', '早退', '迟到早退') THEN 1 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
                    ELSE 0 END as AttendanceRate
                FROM AttendanceRecords ar
                INNER JOIN Employees e ON ar.EmployeeId = e.EmployeeId
                WHERE e.Department = @Department 
                    AND e.IsActive = 1
                    AND strftime('%Y', ar.AttendanceDate) = @Year
                    AND strftime('%m', ar.AttendanceDate) = @Month
                GROUP BY ar.EmployeeId, e.Name, e.EmployeeNumber, e.Department
                ORDER BY e.EmployeeNumber";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<AttendanceStatistics>(sql, 
                new { Department = department, Year = year.ToString(), Month = month.ToString("D2") });
        }
        
        public async Task<IEnumerable<AttendanceRecord>> GetAbnormalRecordsAsync(DateTime startDate, DateTime endDate, string status = null)
        {
            var sql = @"
                SELECT ar.*, e.Name as EmployeeName, e.EmployeeNumber, e.Department 
                FROM AttendanceRecords ar
                INNER JOIN Employees e ON ar.EmployeeId = e.EmployeeId
                WHERE ar.AttendanceDate >= @StartDate 
                    AND ar.AttendanceDate <= @EndDate
                    AND ar.Status NOT IN ('正常')";
            
            var parameters = new { StartDate = startDate.Date, EndDate = endDate.Date };
            
            if (!string.IsNullOrEmpty(status))
            {
                sql += " AND ar.Status = @Status";
                parameters = new { StartDate = startDate.Date, EndDate = endDate.Date, Status = status };
            }
            
            sql += " ORDER BY ar.AttendanceDate DESC, e.EmployeeNumber";
            
            return await ExecuteQueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<AttendanceRecord>> GetPendingApprovalRecordsAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT ar.*, e.Name as EmployeeName, e.EmployeeNumber, e.Department 
                FROM AttendanceRecords ar
                INNER JOIN Employees e ON ar.EmployeeId = e.EmployeeId
                WHERE ar.AttendanceDate >= @StartDate 
                    AND ar.AttendanceDate <= @EndDate
                    AND ar.ApprovalStatus = '待审批'
                ORDER BY ar.AttendanceDate DESC, e.EmployeeNumber";
            
            return await ExecuteQueryAsync(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });
        }
        
        public async Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> attendanceIds, string approvalStatus, string approvedBy)
        {
            const string sql = @"
                UPDATE AttendanceRecords 
                SET ApprovalStatus = @ApprovalStatus,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE AttendanceId IN @AttendanceIds";
            
            return await ExecuteNonQueryAsync(sql, new 
            { 
                AttendanceIds = attendanceIds.ToArray(),
                ApprovalStatus = approvalStatus,
                UpdatedAt = DateTime.Now,
                UpdatedBy = approvedBy
            });
        }
        
        public async Task<bool> HasAttendanceRecordAsync(int employeeId, DateTime date)
        {
            const string sql = @"
                SELECT COUNT(*) FROM AttendanceRecords 
                WHERE EmployeeId = @EmployeeId AND AttendanceDate = @Date";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, new { EmployeeId = employeeId, Date = date.Date });
            return count > 0;
        }
        
        public async Task<OvertimeStatistics> GetOvertimeStatisticsAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT 
                    EmployeeId,
                    COALESCE(SUM(OvertimeHours), 0) as TotalOvertimeHours,
                    COALESCE(SUM(CASE WHEN DayOfWeek NOT IN ('Saturday', 'Sunday') AND IsHoliday = 0 THEN OvertimeHours ELSE 0 END), 0) as WeekdayOvertimeHours,
                    COALESCE(SUM(CASE WHEN DayOfWeek IN ('Saturday', 'Sunday') AND IsHoliday = 0 THEN OvertimeHours ELSE 0 END), 0) as WeekendOvertimeHours,
                    COALESCE(SUM(CASE WHEN IsHoliday = 1 THEN OvertimeHours ELSE 0 END), 0) as HolidayOvertimeHours,
                    COUNT(CASE WHEN OvertimeHours > 0 THEN 1 END) as OvertimeDays
                FROM AttendanceRecords 
                WHERE EmployeeId = @EmployeeId 
                    AND AttendanceDate >= @StartDate 
                    AND AttendanceDate <= @EndDate
                GROUP BY EmployeeId";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<OvertimeStatistics>(sql, 
                new { EmployeeId = employeeId, StartDate = startDate.Date, EndDate = endDate.Date }) ?? new OvertimeStatistics { EmployeeId = employeeId };
        }
        
        public async Task<LateEarlyStatistics> GetLateEarlyStatisticsAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT 
                    EmployeeId,
                    COUNT(CASE WHEN Status IN ('迟到', '迟到早退') THEN 1 END) as LateCount,
                    COUNT(CASE WHEN Status IN ('早退', '迟到早退') THEN 1 END) as EarlyLeaveCount,
                    COALESCE(SUM(CASE WHEN Status IN ('迟到', '迟到早退') AND LateEarlyMinutes > 0 THEN LateEarlyMinutes ELSE 0 END), 0) as TotalLateMinutes,
                    COALESCE(SUM(CASE WHEN Status IN ('早退', '迟到早退') AND LateEarlyMinutes < 0 THEN ABS(LateEarlyMinutes) ELSE 0 END), 0) as TotalEarlyLeaveMinutes,
                    CASE WHEN COUNT(CASE WHEN Status IN ('迟到', '迟到早退') THEN 1 END) > 0 THEN
                        COALESCE(SUM(CASE WHEN Status IN ('迟到', '迟到早退') AND LateEarlyMinutes > 0 THEN LateEarlyMinutes ELSE 0 END), 0) / 
                        COUNT(CASE WHEN Status IN ('迟到', '迟到早退') THEN 1 END)
                    ELSE 0 END as AverageLateMinutes,
                    CASE WHEN COUNT(CASE WHEN Status IN ('早退', '迟到早退') THEN 1 END) > 0 THEN
                        COALESCE(SUM(CASE WHEN Status IN ('早退', '迟到早退') AND LateEarlyMinutes < 0 THEN ABS(LateEarlyMinutes) ELSE 0 END), 0) / 
                        COUNT(CASE WHEN Status IN ('早退', '迟到早退') THEN 1 END)
                    ELSE 0 END as AverageEarlyLeaveMinutes
                FROM AttendanceRecords 
                WHERE EmployeeId = @EmployeeId 
                    AND AttendanceDate >= @StartDate 
                    AND AttendanceDate <= @EndDate
                GROUP BY EmployeeId";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<LateEarlyStatistics>(sql, 
                new { EmployeeId = employeeId, StartDate = startDate.Date, EndDate = endDate.Date }) ?? new LateEarlyStatistics { EmployeeId = employeeId };
        }
        
        #endregion
    }
}