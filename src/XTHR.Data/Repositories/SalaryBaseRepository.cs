using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 工资基础信息仓储接口
    /// </summary>
    public interface ISalaryBaseRepository : IRepository<SalaryBase>
    {
        /// <summary>
        /// 根据员工ID获取当前有效的工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息</returns>
        Task<SalaryBase> GetCurrentByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID获取所有工资基础信息（按生效日期倒序）
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns>工资基础信息列表</returns>
        Task<IEnumerable<SalaryBase>> GetByEmployeeIdAsync(int employeeId);
        
        /// <summary>
        /// 根据员工ID和日期获取有效的工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">查询日期</param>
        /// <returns>工资基础信息</returns>
        Task<SalaryBase> GetByEmployeeIdAndDateAsync(int employeeId, DateTime date);
        
        /// <summary>
        /// 获取指定日期范围内生效的工资基础信息
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>工资基础信息列表</returns>
        Task<IEnumerable<SalaryBase>> GetByEffectiveDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 获取即将过期的工资基础信息（指定天数内）
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns>工资基础信息列表</returns>
        Task<IEnumerable<SalaryBase>> GetExpiringAsync(int days = 30);
        
        /// <summary>
        /// 批量更新工资基础信息的失效日期
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="endDate">失效日期</param>
        /// <returns>受影响的行数</returns>
        Task<int> BatchUpdateEndDateAsync(int employeeId, DateTime endDate);
        
        /// <summary>
        /// 获取工资统计信息
        /// </summary>
        /// <param name="department">部门（可选）</param>
        /// <param name="position">职位（可选）</param>
        /// <returns>工资统计</returns>
        Task<SalaryStatistics> GetSalaryStatisticsAsync(string? department = null, string? position = null);
        
        /// <summary>
        /// 检查员工在指定日期是否有有效的工资基础信息
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <param name="date">日期</param>
        /// <returns>是否有效</returns>
        Task<bool> HasValidSalaryAsync(int employeeId, DateTime date);
    }
    
    /// <summary>
    /// 工资统计信息
    /// </summary>
    public class SalaryStatistics
    {
        public int TotalCount { get; set; }
        public decimal AverageBaseSalary { get; set; }
        public decimal AveragePositionSalary { get; set; }
        public decimal AverageSkillSalary { get; set; }
        public decimal AverageSeniorityPay { get; set; }
        public decimal AverageTotalAllowance { get; set; }
        public decimal AverageTotalFixedSalary { get; set; }
        public decimal MinTotalFixedSalary { get; set; }
        public decimal MaxTotalFixedSalary { get; set; }
    }
    
    /// <summary>
    /// 工资基础信息仓储实现类
    /// </summary>
    public class SalaryBaseRepository : BaseRepository<SalaryBase>, ISalaryBaseRepository
    {
        public SalaryBaseRepository(IDatabaseService databaseService) 
            : base(databaseService, "SalaryBases", "SalaryBaseId")
        {
        }
        
        #region 重写基类方法
        
        protected override object EntityToInsertParameters(SalaryBase entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.BaseSalary,
                entity.PositionSalary,
                entity.SkillSalary,
                entity.SeniorityPay,
                entity.Allowance,
                entity.TransportAllowance,
                entity.MealAllowance,
                entity.CommunicationAllowance,
                entity.HousingAllowance,
                entity.OtherAllowance,
                entity.NonCompeteBase,
                entity.HasNonCompete,
                entity.EffectiveDate,
                entity.EndDate,
                entity.IsActive,
                entity.Remarks,
                CreatedAt = DateTime.Now,
                CreatedBy = entity.CreatedBy ?? "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = entity.UpdatedBy ?? "System"
            };
        }

        public override (IEnumerable<SalaryBase> Items, int TotalCount) GetPaged<TKey>(
            int pageIndex, 
            int pageSize, 
            Expression<Func<SalaryBase, bool>> predicate = null,
            Expression<Func<SalaryBase, TKey>> orderBy = null,
            bool ascending = true)
        {
            var query = GetAll();
            
            if (predicate != null)
            {
                query = query.Where(predicate.Compile());
            }
            
            var totalCount = query.Count();
            
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy.Compile()) : query.OrderByDescending(orderBy.Compile());
            }
            
            var items = query.Skip(pageIndex * pageSize).Take(pageSize);
            
            return (items, totalCount);
        }

        public override async Task<(IEnumerable<SalaryBase> Items, int TotalCount)> GetPagedAsync<TKey>(
            int pageIndex, 
            int pageSize, 
            Expression<Func<SalaryBase, bool>> predicate = null,
            Expression<Func<SalaryBase, TKey>> orderBy = null,
            bool ascending = true)
        {
            var query = await GetAllAsync();
            
            if (predicate != null)
            {
                query = query.Where(predicate.Compile());
            }
            
            var totalCount = query.Count();
            
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy.Compile()) : query.OrderByDescending(orderBy.Compile());
            }
            
            var items = query.Skip(pageIndex * pageSize).Take(pageSize);
            
            return (items, totalCount);
        }
        
        protected override object EntityToUpdateParameters(SalaryBase entity)
        {
            return new
            {
                entity.SalaryBaseId,
                entity.EmployeeId,
                entity.BaseSalary,
                entity.PositionSalary,
                entity.SkillSalary,
                entity.SeniorityPay,
                entity.Allowance,
                entity.TransportAllowance,
                entity.MealAllowance,
                entity.CommunicationAllowance,
                entity.HousingAllowance,
                entity.OtherAllowance,
                entity.NonCompeteBase,
                entity.HasNonCompete,
                entity.EffectiveDate,
                entity.EndDate,
                entity.IsActive,
                entity.Remarks,
                UpdatedAt = DateTime.Now,
                UpdatedBy = entity.UpdatedBy ?? "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO SalaryBases (
                    EmployeeId, BaseSalary, PositionSalary, SkillSalary, SeniorityPay,
                    Allowance, TransportAllowance, MealAllowance, CommunicationAllowance,
                    HousingAllowance, OtherAllowance, NonCompeteBase, HasNonCompete,
                    EffectiveDate, EndDate, IsActive, Remarks,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeId, @BaseSalary, @PositionSalary, @SkillSalary, @SeniorityPay,
                    @Allowance, @TransportAllowance, @MealAllowance, @CommunicationAllowance,
                    @HousingAllowance, @OtherAllowance, @NonCompeteBase, @HasNonCompete,
                    @EffectiveDate, @EndDate, @IsActive, @Remarks,
                    @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE SalaryBases SET 
                    EmployeeId = @EmployeeId,
                    BaseSalary = @BaseSalary,
                    PositionSalary = @PositionSalary,
                    SkillSalary = @SkillSalary,
                    SeniorityPay = @SeniorityPay,
                    Allowance = @Allowance,
                    TransportAllowance = @TransportAllowance,
                    MealAllowance = @MealAllowance,
                    CommunicationAllowance = @CommunicationAllowance,
                    HousingAllowance = @HousingAllowance,
                    OtherAllowance = @OtherAllowance,
                    NonCompeteBase = @NonCompeteBase,
                    HasNonCompete = @HasNonCompete,
                    EffectiveDate = @EffectiveDate,
                    EndDate = @EndDate,
                    IsActive = @IsActive,
                    Remarks = @Remarks,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE SalaryBaseId = @SalaryBaseId";
        }
        
        protected override string GetSelectByIdSql()
        {
            return "SELECT * FROM SalaryBases WHERE SalaryBaseId = @Id";
        }
        
        protected override string GetDeleteSql()
        {
            return "DELETE FROM SalaryBases WHERE SalaryBaseId = @Id";
        }
        
        #endregion
        
        #region 接口实现
        
        public async Task<SalaryBase> GetCurrentByEmployeeIdAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM SalaryBases 
                WHERE EmployeeId = @EmployeeId 
                    AND IsActive = 1 
                    AND EffectiveDate <= @CurrentDate 
                    AND (EndDate IS NULL OR EndDate >= @CurrentDate)
                ORDER BY EffectiveDate DESC 
                LIMIT 1";
            
            var result = await ExecuteQueryAsync(sql, new { EmployeeId = employeeId, CurrentDate = DateTime.Today });
            return result.FirstOrDefault()!;
        }
        
        public async Task<IEnumerable<SalaryBase>> GetByEmployeeIdAsync(int employeeId)
        {
            const string sql = @"
                SELECT * FROM SalaryBases 
                WHERE EmployeeId = @EmployeeId 
                ORDER BY EffectiveDate DESC";
            
            return await ExecuteQueryAsync(sql, new { EmployeeId = employeeId });
        }
        
        public async Task<SalaryBase> GetByEmployeeIdAndDateAsync(int employeeId, DateTime date)
        {
            const string sql = @"
                SELECT * FROM SalaryBases 
                WHERE EmployeeId = @EmployeeId 
                    AND EffectiveDate <= @Date 
                    AND (EndDate IS NULL OR EndDate >= @Date)
                ORDER BY EffectiveDate DESC 
                LIMIT 1";
            
            var result = await ExecuteQueryAsync(sql, new { EmployeeId = employeeId, Date = date });
            return result.FirstOrDefault(); // 允许返回null，调用方应处理
        }
        
        public async Task<IEnumerable<SalaryBase>> GetByEffectiveDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM SalaryBases 
                WHERE EffectiveDate >= @StartDate AND EffectiveDate <= @EndDate 
                ORDER BY EffectiveDate DESC";
            
            return await ExecuteQueryAsync(sql, new { StartDate = startDate, EndDate = endDate });
        }
        
        public async Task<IEnumerable<SalaryBase>> GetExpiringAsync(int days = 30)
        {
            var targetDate = DateTime.Today.AddDays(days);
            const string sql = @"
                SELECT sb.*, e.Name as EmployeeName, e.EmployeeNumber, e.Department 
                FROM SalaryBases sb
                INNER JOIN Employees e ON sb.EmployeeId = e.EmployeeId
                WHERE sb.EndDate IS NOT NULL 
                    AND sb.EndDate <= @TargetDate 
                    AND sb.EndDate >= @CurrentDate
                    AND sb.IsActive = 1
                ORDER BY sb.EndDate";
            
            return await ExecuteQueryAsync(sql, new { TargetDate = targetDate, CurrentDate = DateTime.Today });
        }
        
        public async Task<int> BatchUpdateEndDateAsync(int employeeId, DateTime endDate)
        {
            const string sql = @"
                UPDATE SalaryBases 
                SET EndDate = @EndDate, 
                    IsActive = 0,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE EmployeeId = @EmployeeId 
                    AND IsActive = 1 
                    AND (EndDate IS NULL OR EndDate > @EndDate)";
            
            return await ExecuteNonQueryAsync(sql, new 
            { 
                EmployeeId = employeeId, 
                EndDate = endDate,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            });
        }
        
        public async Task<SalaryStatistics> GetSalaryStatisticsAsync(string? department = null, string? position = null)
        {
            var sql = @"
                SELECT 
                    COUNT(*) as TotalCount,
                    AVG(sb.BaseSalary) as AverageBaseSalary,
                    AVG(sb.PositionSalary) as AveragePositionSalary,
                    AVG(sb.SkillSalary) as AverageSkillSalary,
                    AVG(sb.SeniorityPay) as AverageSeniorityPay,
                    AVG(sb.Allowance + sb.TransportAllowance + sb.MealAllowance + 
                        sb.CommunicationAllowance + sb.HousingAllowance + sb.OtherAllowance) as AverageTotalAllowance,
                    AVG(sb.BaseSalary + sb.PositionSalary + sb.SkillSalary + sb.SeniorityPay + 
                        sb.Allowance + sb.TransportAllowance + sb.MealAllowance + 
                        sb.CommunicationAllowance + sb.HousingAllowance + sb.OtherAllowance) as AverageTotalFixedSalary,
                    MIN(sb.BaseSalary + sb.PositionSalary + sb.SkillSalary + sb.SeniorityPay + 
                        sb.Allowance + sb.TransportAllowance + sb.MealAllowance + 
                        sb.CommunicationAllowance + sb.HousingAllowance + sb.OtherAllowance) as MinTotalFixedSalary,
                    MAX(sb.BaseSalary + sb.PositionSalary + sb.SkillSalary + sb.SeniorityPay + 
                        sb.Allowance + sb.TransportAllowance + sb.MealAllowance + 
                        sb.CommunicationAllowance + sb.HousingAllowance + sb.OtherAllowance) as MaxTotalFixedSalary
                FROM SalaryBases sb
                INNER JOIN Employees e ON sb.EmployeeId = e.EmployeeId
                WHERE sb.IsActive = 1 
                    AND e.IsActive = 1
                    AND sb.EffectiveDate <= @CurrentDate 
                    AND (sb.EndDate IS NULL OR sb.EndDate >= @CurrentDate)";
            
            var parameters = new DynamicParameters();
            parameters.Add("@CurrentDate", DateTime.Today);
            
            if (!string.IsNullOrEmpty(department))
            {
                sql += " AND e.Department = @Department";
                parameters.Add("@Department", department);
            }
            
            if (!string.IsNullOrEmpty(position))
            {
                sql += " AND e.Position = @Position";
                parameters.Add("@Position", position);
            }

            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleAsync<SalaryStatistics>(sql, parameters);
        }
        
        public async Task<bool> HasValidSalaryAsync(int employeeId, DateTime date)
        {
            const string sql = @"
                SELECT COUNT(*) FROM SalaryBases 
                WHERE EmployeeId = @EmployeeId 
                    AND EffectiveDate <= @Date 
                    AND (EndDate IS NULL OR EndDate >= @Date)";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, new { EmployeeId = employeeId, Date = date });
            return count > 0;
        }
        
        #endregion
    }
}