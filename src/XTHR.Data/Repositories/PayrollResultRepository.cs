using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 工资核算结果Repository接口
    /// </summary>
    public interface IPayrollResultRepository : IRepository<PayrollResult>
    {
        /// <summary>
        /// 根据员工ID获取工资记录
        /// </summary>
        Task<IEnumerable<PayrollResult>> GetByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID和工资期间获取工资记录
        /// </summary>
        Task<PayrollResult> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取指定期间的所有工资记录
        /// </summary>
        Task<IEnumerable<PayrollResult>> GetByPeriodAsync(int year, int month);
        
        /// <summary>
        /// 获取指定期间范围的工资记录
        /// </summary>
        Task<IEnumerable<PayrollResult>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取部门工资统计
        /// </summary>
        Task<IEnumerable<DepartmentPayrollStatistics>> GetDepartmentStatisticsAsync(int year, int month);
        
        /// <summary>
        /// 获取员工工资趋势
        /// </summary>
        Task<IEnumerable<PayrollResult>> GetEmployeePayrollTrendAsync(int employeeId, int months = 12);
        
        /// <summary>
        /// 获取待审批的工资记录
        /// </summary>
        Task<IEnumerable<PayrollResult>> GetPendingApprovalAsync();
        
        /// <summary>
        /// 批量更新审批状态
        /// </summary>
        Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> payrollIds, string approvalStatus, string approvedBy);
        
        /// <summary>
        /// 检查员工在指定期间是否已有工资记录
        /// </summary>
        Task<bool> ExistsForEmployeeAndPeriodAsync(int employeeId, int year, int month);
        
        /// <summary>
        /// 获取工资分布统计
        /// </summary>
        Task<IEnumerable<PayrollDistribution>> GetPayrollDistributionAsync(int year, int month);
        
        /// <summary>
        /// 获取工资汇总统计
        /// </summary>
        Task<PayrollSummaryStatistics> GetPayrollSummaryAsync(int year, int month);
        
        /// <summary>
        /// 获取员工年度工资汇总
        /// </summary>
        Task<IEnumerable<EmployeeAnnualPayroll>> GetEmployeeAnnualPayrollAsync(int employeeId, int year);
        
        /// <summary>
        /// 批量删除指定期间的工资记录
        /// </summary>
        Task<int> BatchDeleteByPeriodAsync(int year, int month);
    }
    
    /// <summary>
    /// 工资核算结果Repository实现
    /// </summary>
    public class PayrollResultRepository : BaseRepository<PayrollResult>, IPayrollResultRepository
    {
        public PayrollResultRepository(IDatabaseService databaseService)
            : base(databaseService, "PayrollResults", "PayrollId")
        {
        }
        
        protected override object EntityToInsertParameters(PayrollResult entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.PayrollYear,
                entity.PayrollMonth,
                entity.PayrollPeriod,
                entity.BaseSalary,
                entity.PositionSalary,
                entity.SkillSalary,
                entity.SeniorityPay,
                entity.PerformanceSalary,
                entity.Allowances,
                entity.OvertimePay,
                entity.NonCompetePay,
                entity.Bonus,
                entity.GrossPay,
                entity.PersonalIncomeTax,
                entity.PersonalSocialSecurity,
                entity.PersonalHousingFund,
                entity.OtherDeductions,
                entity.TotalDeductions,
                entity.NetPay,
                entity.CompanySocialSecurity,
                entity.CompanyHousingFund,
                entity.TotalCompanyCost,
                entity.WorkDays,
                entity.ActualWorkDays,
                entity.OvertimeHours,
                entity.LeaveHours,
                entity.LateMinutes,
                entity.EarlyLeaveMinutes,
                entity.AbsentDays,
                entity.PerformanceCoefficient,
                entity.TaxableIncome,
                entity.TaxExemption,
                entity.SpecialDeductions,
                entity.PayrollStatus,
                entity.ApprovalStatus,
                entity.ApprovedBy,
                entity.ApprovedAt,
                entity.PaymentDate,
                entity.PaymentMethod,
                entity.BankAccount,
                entity.CalculatedBy,
                entity.CalculatedAt,
                entity.Remarks,
                entity.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override object EntityToUpdateParameters(PayrollResult entity)
        {
            return new
            {
                entity.PayrollId,
                entity.EmployeeId,
                entity.PayrollYear,
                entity.PayrollMonth,
                entity.PayrollPeriod,
                entity.BaseSalary,
                entity.PositionSalary,
                entity.SkillSalary,
                entity.SeniorityPay,
                entity.PerformanceSalary,
                entity.Allowances,
                entity.OvertimePay,
                entity.NonCompetePay,
                entity.Bonus,
                entity.GrossPay,
                entity.PersonalIncomeTax,
                entity.PersonalSocialSecurity,
                entity.PersonalHousingFund,
                entity.OtherDeductions,
                entity.TotalDeductions,
                entity.NetPay,
                entity.CompanySocialSecurity,
                entity.CompanyHousingFund,
                entity.TotalCompanyCost,
                entity.WorkDays,
                entity.ActualWorkDays,
                entity.OvertimeHours,
                entity.LeaveHours,
                entity.LateMinutes,
                entity.EarlyLeaveMinutes,
                entity.AbsentDays,
                entity.PerformanceCoefficient,
                entity.TaxableIncome,
                entity.TaxExemption,
                entity.SpecialDeductions,
                entity.PayrollStatus,
                entity.ApprovalStatus,
                entity.ApprovedBy,
                entity.ApprovedAt,
                entity.PaymentDate,
                entity.PaymentMethod,
                entity.BankAccount,
                entity.CalculatedBy,
                entity.CalculatedAt,
                entity.Remarks,
                entity.IsActive,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO PayrollResults (
                    EmployeeId, PayrollYear, PayrollMonth, PayrollPeriod, BaseSalary, PositionSalary,
                    SkillSalary, SeniorityPay, PerformanceSalary, Allowances, OvertimePay, NonCompetePay,
                    Bonus, GrossPay, PersonalIncomeTax, PersonalSocialSecurity, PersonalHousingFund,
                    OtherDeductions, TotalDeductions, NetPay, CompanySocialSecurity, CompanyHousingFund,
                    TotalCompanyCost, WorkDays, ActualWorkDays, OvertimeHours, LeaveHours, LateMinutes,
                    EarlyLeaveMinutes, AbsentDays, PerformanceCoefficient, TaxableIncome, TaxExemption,
                    SpecialDeductions, PayrollStatus, ApprovalStatus, ApprovedBy, ApprovedAt, PaymentDate,
                    PaymentMethod, BankAccount, CalculatedBy, CalculatedAt, Remarks, IsActive,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeId, @PayrollYear, @PayrollMonth, @PayrollPeriod, @BaseSalary, @PositionSalary,
                    @SkillSalary, @SeniorityPay, @PerformanceSalary, @Allowances, @OvertimePay, @NonCompetePay,
                    @Bonus, @GrossPay, @PersonalIncomeTax, @PersonalSocialSecurity, @PersonalHousingFund,
                    @OtherDeductions, @TotalDeductions, @NetPay, @CompanySocialSecurity, @CompanyHousingFund,
                    @TotalCompanyCost, @WorkDays, @ActualWorkDays, @OvertimeHours, @LeaveHours, @LateMinutes,
                    @EarlyLeaveMinutes, @AbsentDays, @PerformanceCoefficient, @TaxableIncome, @TaxExemption,
                    @SpecialDeductions, @PayrollStatus, @ApprovalStatus, @ApprovedBy, @ApprovedAt, @PaymentDate,
                    @PaymentMethod, @BankAccount, @CalculatedBy, @CalculatedAt, @Remarks, @IsActive,
                    @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE PayrollResults SET
                    EmployeeId = @EmployeeId, PayrollYear = @PayrollYear, PayrollMonth = @PayrollMonth,
                    PayrollPeriod = @PayrollPeriod, BaseSalary = @BaseSalary, PositionSalary = @PositionSalary,
                    SkillSalary = @SkillSalary, SeniorityPay = @SeniorityPay, PerformanceSalary = @PerformanceSalary,
                    Allowances = @Allowances, OvertimePay = @OvertimePay, NonCompetePay = @NonCompetePay,
                    Bonus = @Bonus, GrossPay = @GrossPay, PersonalIncomeTax = @PersonalIncomeTax,
                    PersonalSocialSecurity = @PersonalSocialSecurity, PersonalHousingFund = @PersonalHousingFund,
                    OtherDeductions = @OtherDeductions, TotalDeductions = @TotalDeductions, NetPay = @NetPay,
                    CompanySocialSecurity = @CompanySocialSecurity, CompanyHousingFund = @CompanyHousingFund,
                    TotalCompanyCost = @TotalCompanyCost, WorkDays = @WorkDays, ActualWorkDays = @ActualWorkDays,
                    OvertimeHours = @OvertimeHours, LeaveHours = @LeaveHours, LateMinutes = @LateMinutes,
                    EarlyLeaveMinutes = @EarlyLeaveMinutes, AbsentDays = @AbsentDays, PerformanceCoefficient = @PerformanceCoefficient,
                    TaxableIncome = @TaxableIncome, TaxExemption = @TaxExemption, SpecialDeductions = @SpecialDeductions,
                    PayrollStatus = @PayrollStatus, ApprovalStatus = @ApprovalStatus, ApprovedBy = @ApprovedBy,
                    ApprovedAt = @ApprovedAt, PaymentDate = @PaymentDate, PaymentMethod = @PaymentMethod,
                    BankAccount = @BankAccount, CalculatedBy = @CalculatedBy, CalculatedAt = @CalculatedAt,
                    Remarks = @Remarks, IsActive = @IsActive, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                WHERE PayrollId = @PayrollId";
        }
        
        public async Task<IEnumerable<PayrollResult>> GetByEmployeeIdAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM PayrollResults 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY PayrollYear DESC, PayrollMonth DESC";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PayrollResult>(sql, new { EmployeeId = employeeId });
        }
        
        public async Task<PayrollResult> GetByEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT * FROM PayrollResults 
                WHERE EmployeeId = @EmployeeId AND PayrollYear = @Year AND PayrollMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<PayrollResult>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
        }
        
        public async Task<IEnumerable<PayrollResult>> GetByPeriodAsync(int year, int month)
        {
            const string sql = @"
                SELECT * FROM PayrollResults 
                WHERE PayrollYear = @Year AND PayrollMonth = @Month AND IsActive = 1
                ORDER BY EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PayrollResult>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<PayrollResult>> GetByPeriodRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM PayrollResults 
                WHERE CalculatedAt >= @StartDate AND CalculatedAt <= @EndDate AND IsActive = 1
                ORDER BY CalculatedAt DESC, EmployeeId";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PayrollResult>(sql, new { StartDate = startDate, EndDate = endDate });
        }
        
        public async Task<IEnumerable<DepartmentPayrollStatistics>> GetDepartmentStatisticsAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    e.Department,
                    COUNT(*) as EmployeeCount,
                    SUM(p.GrossPay) as TotalGrossPay,
                    AVG(p.GrossPay) as AverageGrossPay,
                    SUM(p.NetPay) as TotalNetPay,
                    AVG(p.NetPay) as AverageNetPay,
                    SUM(p.TotalCompanyCost) as TotalCompanyCost,
                    AVG(p.TotalCompanyCost) as AverageCompanyCost,
                    SUM(p.PersonalIncomeTax) as TotalTax,
                    SUM(p.OvertimePay) as TotalOvertimePay
                FROM PayrollResults p
                INNER JOIN Employees e ON p.EmployeeId = e.EmployeeId
                WHERE p.PayrollYear = @Year AND p.PayrollMonth = @Month AND p.IsActive = 1
                GROUP BY e.Department
                ORDER BY e.Department";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<DepartmentPayrollStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<PayrollResult>> GetEmployeePayrollTrendAsync(int employeeId, int months = 12)
        {
            const string sql = @"
                SELECT * FROM PayrollResults 
                WHERE EmployeeId = @EmployeeId AND IsActive = 1
                ORDER BY PayrollYear DESC, PayrollMonth DESC
                LIMIT @Months";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PayrollResult>(sql, new { EmployeeId = employeeId, Months = months });
        }
        
        public async Task<IEnumerable<PayrollResult>> GetPendingApprovalAsync()
        {
            const string sql = @"
                SELECT p.*, e.EmployeeName, e.Department, e.Position
                FROM PayrollResults p
                INNER JOIN Employees e ON p.EmployeeId = e.EmployeeId
                WHERE p.ApprovalStatus = '待审批' AND p.IsActive = 1
                ORDER BY p.CalculatedAt";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PayrollResult>(sql);
        }
        
        public async Task<int> BatchUpdateApprovalStatusAsync(IEnumerable<int> payrollIds, string approvalStatus, string approvedBy)
        {
            const string sql = @"
                UPDATE PayrollResults 
                SET ApprovalStatus = @ApprovalStatus, ApprovedBy = @ApprovedBy, ApprovedAt = @ApprovedAt, UpdatedAt = @UpdatedAt
                WHERE PayrollId IN @PayrollIds";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, new 
            { 
                ApprovalStatus = approvalStatus, 
                ApprovedBy = approvedBy, 
                ApprovedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PayrollIds = payrollIds 
            });
        }
        
        public async Task<bool> ExistsForEmployeeAndPeriodAsync(int employeeId, int year, int month)
        {
            const string sql = @"
                SELECT COUNT(*) FROM PayrollResults 
                WHERE EmployeeId = @EmployeeId AND PayrollYear = @Year AND PayrollMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, new { EmployeeId = employeeId, Year = year, Month = month });
            return count > 0;
        }
        
        public async Task<IEnumerable<PayrollDistribution>> GetPayrollDistributionAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    CASE 
                        WHEN NetPay < 5000 THEN '5000以下'
                        WHEN NetPay < 10000 THEN '5000-10000'
                        WHEN NetPay < 15000 THEN '10000-15000'
                        WHEN NetPay < 20000 THEN '15000-20000'
                        WHEN NetPay < 30000 THEN '20000-30000'
                        ELSE '30000以上'
                    END as SalaryRange,
                    COUNT(*) as Count,
                    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM PayrollResults WHERE PayrollYear = @Year AND PayrollMonth = @Month AND IsActive = 1), 2) as Percentage
                FROM PayrollResults 
                WHERE PayrollYear = @Year AND PayrollMonth = @Month AND IsActive = 1
                GROUP BY 
                    CASE 
                        WHEN NetPay < 5000 THEN '5000以下'
                        WHEN NetPay < 10000 THEN '5000-10000'
                        WHEN NetPay < 15000 THEN '10000-15000'
                        WHEN NetPay < 20000 THEN '15000-20000'
                        WHEN NetPay < 30000 THEN '20000-30000'
                        ELSE '30000以上'
                    END
                ORDER BY 
                    CASE 
                        WHEN NetPay < 5000 THEN 1
                        WHEN NetPay < 10000 THEN 2
                        WHEN NetPay < 15000 THEN 3
                        WHEN NetPay < 20000 THEN 4
                        WHEN NetPay < 30000 THEN 5
                        ELSE 6
                    END";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<PayrollDistribution>(sql, new { Year = year, Month = month });
        }
        
        public async Task<PayrollSummaryStatistics> GetPayrollSummaryAsync(int year, int month)
        {
            const string sql = @"
                SELECT 
                    COUNT(*) as TotalEmployees,
                    SUM(GrossPay) as TotalGrossPay,
                    AVG(GrossPay) as AverageGrossPay,
                    SUM(NetPay) as TotalNetPay,
                    AVG(NetPay) as AverageNetPay,
                    SUM(TotalCompanyCost) as TotalCompanyCost,
                    AVG(TotalCompanyCost) as AverageCompanyCost,
                    SUM(PersonalIncomeTax) as TotalTax,
                    SUM(PersonalSocialSecurity + PersonalHousingFund) as TotalPersonalDeductions,
                    SUM(CompanySocialSecurity + CompanyHousingFund) as TotalCompanyContributions,
                    SUM(OvertimePay) as TotalOvertimePay,
                    MIN(NetPay) as MinNetPay,
                    MAX(NetPay) as MaxNetPay
                FROM PayrollResults 
                WHERE PayrollYear = @Year AND PayrollMonth = @Month AND IsActive = 1";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<PayrollSummaryStatistics>(sql, new { Year = year, Month = month });
        }
        
        public async Task<IEnumerable<EmployeeAnnualPayroll>> GetEmployeeAnnualPayrollAsync(int employeeId, int year)
        {
            const string sql = @"
                SELECT 
                    PayrollMonth,
                    GrossPay,
                    NetPay,
                    PersonalIncomeTax,
                    TotalCompanyCost,
                    OvertimePay,
                    Bonus
                FROM PayrollResults 
                WHERE EmployeeId = @EmployeeId AND PayrollYear = @Year AND IsActive = 1
                ORDER BY PayrollMonth";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<EmployeeAnnualPayroll>(sql, new { EmployeeId = employeeId, Year = year });
        }
        
        public async Task<int> BatchDeleteByPeriodAsync(int year, int month)
        {
            const string sql = @"
                UPDATE PayrollResults 
                SET IsActive = 0, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy
                WHERE PayrollYear = @Year AND PayrollMonth = @Month";
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, new 
            { 
                Year = year, 
                Month = month,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            });
        }
    }
    
    /// <summary>
    /// 部门工资统计
    /// </summary>
    public class DepartmentPayrollStatistics
    {
        public string Department { get; set; }
        public int EmployeeCount { get; set; }
        public decimal TotalGrossPay { get; set; }
        public decimal AverageGrossPay { get; set; }
        public decimal TotalNetPay { get; set; }
        public decimal AverageNetPay { get; set; }
        public decimal TotalCompanyCost { get; set; }
        public decimal AverageCompanyCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalOvertimePay { get; set; }
    }
    
    /// <summary>
    /// 工资分布统计
    /// </summary>
    public class PayrollDistribution
    {
        public string SalaryRange { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
    }
    
    /// <summary>
    /// 工资汇总统计
    /// </summary>
    public class PayrollSummaryStatistics
    {
        public int TotalEmployees { get; set; }
        public decimal TotalGrossPay { get; set; }
        public decimal AverageGrossPay { get; set; }
        public decimal TotalNetPay { get; set; }
        public decimal AverageNetPay { get; set; }
        public decimal TotalCompanyCost { get; set; }
        public decimal AverageCompanyCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalPersonalDeductions { get; set; }
        public decimal TotalCompanyContributions { get; set; }
        public decimal TotalOvertimePay { get; set; }
        public decimal MinNetPay { get; set; }
        public decimal MaxNetPay { get; set; }
    }
    
    /// <summary>
    /// 员工年度工资
    /// </summary>
    public class EmployeeAnnualPayroll
    {
        public int PayrollMonth { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal PersonalIncomeTax { get; set; }
        public decimal TotalCompanyCost { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Bonus { get; set; }
    }
}