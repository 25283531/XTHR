using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XTHR.Common.Models;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 员工信息仓储接口
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// 根据工号查找员工
        /// </summary>
        /// <param name="employeeNumber">工号</param>
        /// <returns>员工信息</returns>
        Task<Employee> GetByEmployeeNumberAsync(string employeeNumber);
        
        /// <summary>
        /// 根据身份证号查找员工
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <returns>员工信息</returns>
        Task<Employee> GetByIdCardAsync(string idCard);
        
        /// <summary>
        /// 根据部门获取员工列表
        /// </summary>
        /// <param name="department">部门</param>
        /// <param name="isActive">是否在职</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByDepartmentAsync(string department, bool? isActive = null);
        
        /// <summary>
        /// 根据职位获取员工列表
        /// </summary>
        /// <param name="position">职位</param>
        /// <param name="isActive">是否在职</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByPositionAsync(string position, bool? isActive = null);
        
        /// <summary>
        /// 根据职级获取员工列表
        /// </summary>
        /// <param name="level">职级</param>
        /// <param name="isActive">是否在职</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByLevelAsync(string level, bool? isActive = null);
        
        /// <summary>
        /// 获取在职员工列表
        /// </summary>
        /// <returns>在职员工列表</returns>
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
        
        /// <summary>
        /// 获取离职员工列表
        /// </summary>
        /// <returns>离职员工列表</returns>
        Task<IEnumerable<Employee>> GetInactiveEmployeesAsync();
        
        /// <summary>
        /// 根据入职日期范围获取员工列表
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// 搜索员工（支持姓名、工号、部门、职位模糊查询）
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="isActive">是否在职</param>
        /// <returns>员工列表</returns>
        Task<IEnumerable<Employee>> SearchEmployeesAsync(string keyword, bool? isActive = null);
        
        /// <summary>
        /// 检查工号是否已存在
        /// </summary>
        /// <param name="employeeNumber">工号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否存在</returns>
        Task<bool> IsEmployeeNumberExistsAsync(string employeeNumber, int? excludeId = null);
        
        /// <summary>
        /// 检查身份证号是否已存在
        /// </summary>
        /// <param name="idCard">身份证号</param>
        /// <param name="excludeId">排除的员工ID</param>
        /// <returns>是否存在</returns>
        Task<bool> IsIdCardExistsAsync(string idCard, int? excludeId = null);
        
        /// <summary>
        /// 获取部门统计信息
        /// </summary>
        /// <returns>部门统计</returns>
        Task<Dictionary<string, int>> GetDepartmentStatisticsAsync();
        
        /// <summary>
        /// 获取职位统计信息
        /// </summary>
        /// <returns>职位统计</returns>
        Task<Dictionary<string, int>> GetPositionStatisticsAsync();
    }
    
    /// <summary>
    /// 员工信息仓储实现类
    /// </summary>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDatabaseService databaseService) 
            : base(databaseService, "Employees", "EmployeeId")
        {
        }
        
        #region 重写基类方法
        
        protected override object EntityToInsertParameters(Employee entity)
        {
            return new
            {
                entity.EmployeeNumber,
                entity.Name,
                entity.Department,
                entity.Position,
                entity.Level,
                entity.HireDate,
                entity.IdCard,
                entity.Phone,
                entity.Email,
                entity.Address,
                entity.IsActive,
                entity.Remarks,
                CreatedAt = DateTime.Now,
                CreatedBy = entity.CreatedBy ?? "System",
                UpdatedAt = DateTime.Now,
                UpdatedBy = entity.UpdatedBy ?? "System"
            };
        }
        
        protected override object EntityToUpdateParameters(Employee entity)
        {
            return new
            {
                entity.EmployeeId,
                entity.EmployeeNumber,
                entity.Name,
                entity.Department,
                entity.Position,
                entity.Level,
                entity.HireDate,
                entity.IdCard,
                entity.Phone,
                entity.Email,
                entity.Address,
                entity.IsActive,
                entity.Remarks,
                UpdatedAt = DateTime.Now,
                UpdatedBy = entity.UpdatedBy ?? "System"
            };
        }
        
        protected override string GetInsertSql()
        {
            return @"
                INSERT INTO Employees (
                    EmployeeNumber, Name, Department, Position, Level, HireDate, 
                    IdCard, Phone, Email, Address, IsActive, Remarks,
                    CreatedAt, CreatedBy, UpdatedAt, UpdatedBy
                ) VALUES (
                    @EmployeeNumber, @Name, @Department, @Position, @Level, @HireDate,
                    @IdCard, @Phone, @Email, @Address, @IsActive, @Remarks,
                    @CreatedAt, @CreatedBy, @UpdatedAt, @UpdatedBy
                )";
        }
        
        protected override string GetUpdateSql()
        {
            return @"
                UPDATE Employees SET 
                    EmployeeNumber = @EmployeeNumber,
                    Name = @Name,
                    Department = @Department,
                    Position = @Position,
                    Level = @Level,
                    HireDate = @HireDate,
                    IdCard = @IdCard,
                    Phone = @Phone,
                    Email = @Email,
                    Address = @Address,
                    IsActive = @IsActive,
                    Remarks = @Remarks,
                    UpdatedAt = @UpdatedAt,
                    UpdatedBy = @UpdatedBy
                WHERE EmployeeId = @EmployeeId";
        }
        
        protected override string GetSelectByIdSql()
        {
            return "SELECT * FROM Employees WHERE EmployeeId = @Id";
        }
        
        protected override string GetDeleteSql()
        {
            return "DELETE FROM Employees WHERE EmployeeId = @Id";
        }
        
        #endregion
        
        #region 接口实现
        
        public async Task<Employee> GetByEmployeeNumberAsync(string employeeNumber)
        {
            const string sql = "SELECT * FROM Employees WHERE EmployeeNumber = @EmployeeNumber";
            var result = await ExecuteQueryAsync(sql, new { EmployeeNumber = employeeNumber });
            return result.FirstOrDefault();
        }
        
        public async Task<Employee> GetByIdCardAsync(string idCard)
        {
            const string sql = "SELECT * FROM Employees WHERE IdCard = @IdCard";
            var result = await ExecuteQueryAsync(sql, new { IdCard = idCard });
            return result.FirstOrDefault();
        }
        
        public async Task<IEnumerable<Employee>> GetByDepartmentAsync(string department, bool? isActive = null)
        {
            var sql = "SELECT * FROM Employees WHERE Department = @Department";
            var parameters = new { Department = department };
            
            if (isActive.HasValue)
            {
                sql += " AND IsActive = @IsActive";
                parameters = new { Department = department, IsActive = isActive.Value };
            }
            
            return await ExecuteQueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<Employee>> GetByPositionAsync(string position, bool? isActive = null)
        {
            var sql = "SELECT * FROM Employees WHERE Position = @Position";
            var parameters = new { Position = position };
            
            if (isActive.HasValue)
            {
                sql += " AND IsActive = @IsActive";
                parameters = new { Position = position, IsActive = isActive.Value };
            }
            
            return await ExecuteQueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<Employee>> GetByLevelAsync(string level, bool? isActive = null)
        {
            var sql = "SELECT * FROM Employees WHERE Level = @Level";
            var parameters = new { Level = level };
            
            if (isActive.HasValue)
            {
                sql += " AND IsActive = @IsActive";
                parameters = new { Level = level, IsActive = isActive.Value };
            }
            
            return await ExecuteQueryAsync(sql, parameters);
        }
        
        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
        {
            const string sql = "SELECT * FROM Employees WHERE IsActive = 1 ORDER BY EmployeeNumber";
            return await ExecuteQueryAsync(sql);
        }
        
        public async Task<IEnumerable<Employee>> GetInactiveEmployeesAsync()
        {
            const string sql = "SELECT * FROM Employees WHERE IsActive = 0 ORDER BY EmployeeNumber";
            return await ExecuteQueryAsync(sql);
        }
        
        public async Task<IEnumerable<Employee>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM Employees 
                WHERE HireDate >= @StartDate AND HireDate <= @EndDate 
                ORDER BY HireDate";
            return await ExecuteQueryAsync(sql, new { StartDate = startDate, EndDate = endDate });
        }
        
        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(string keyword, bool? isActive = null)
        {
            var sql = @"
                SELECT * FROM Employees 
                WHERE (Name LIKE @Keyword 
                    OR EmployeeNumber LIKE @Keyword 
                    OR Department LIKE @Keyword 
                    OR Position LIKE @Keyword)";
            
            var parameters = new { Keyword = $"%{keyword}%" };
            
            if (isActive.HasValue)
            {
                sql += " AND IsActive = @IsActive";
                parameters = new { Keyword = $"%{keyword}%", IsActive = isActive.Value };
            }
            
            sql += " ORDER BY EmployeeNumber";
            
            return await ExecuteQueryAsync(sql, parameters);
        }
        
        public async Task<bool> IsEmployeeNumberExistsAsync(string employeeNumber, int? excludeId = null)
        {
            var sql = "SELECT COUNT(*) FROM Employees WHERE EmployeeNumber = @EmployeeNumber";
            var parameters = new { EmployeeNumber = employeeNumber };
            
            if (excludeId.HasValue)
            {
                sql += " AND EmployeeId != @ExcludeId";
                parameters = new { EmployeeNumber = employeeNumber, ExcludeId = excludeId.Value };
            }
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, parameters);
            return count > 0;
        }
        
        public async Task<bool> IsIdCardExistsAsync(string idCard, int? excludeId = null)
        {
            var sql = "SELECT COUNT(*) FROM Employees WHERE IdCard = @IdCard";
            var parameters = new { IdCard = idCard };
            
            if (excludeId.HasValue)
            {
                sql += " AND EmployeeId != @ExcludeId";
                parameters = new { IdCard = idCard, ExcludeId = excludeId.Value };
            }
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            var count = await connection.QuerySingleAsync<int>(sql, parameters);
            return count > 0;
        }
        
        public async Task<Dictionary<string, int>> GetDepartmentStatisticsAsync()
        {
            const string sql = @"
                SELECT Department, COUNT(*) as Count 
                FROM Employees 
                WHERE IsActive = 1 
                GROUP BY Department 
                ORDER BY Count DESC";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            var result = await connection.QueryAsync(sql);
            
            return result.ToDictionary(
                row => (string)row.Department ?? "未分配",
                row => (int)row.Count
            );
        }
        
        public async Task<Dictionary<string, int>> GetPositionStatisticsAsync()
        {
            const string sql = @"
                SELECT Position, COUNT(*) as Count 
                FROM Employees 
                WHERE IsActive = 1 
                GROUP BY Position 
                ORDER BY Count DESC";
            
            using var connection = new Microsoft.Data.Sqlite.SqliteConnection(_databaseService.GetConnectionString());
            var result = await connection.QueryAsync(sql);
            
            return result.ToDictionary(
                row => (string)row.Position ?? "未分配",
                row => (int)row.Count
            );
        }
        
        #endregion
    }
}