using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using XTHR.Common.Models;
using XTHR.Data.Repositories;
using XTHR.Data.Services;

namespace XTHR.Data.UnitOfWork
{
    /// <summary>
    /// 工作单元实现类
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseService _databaseService;
        private SqliteConnection _connection;
        private SqliteTransaction _transaction;
        private bool _disposed = false;
        
        #region Repository实例
        
        private IEmployeeRepository _employees;
        private ISalaryBaseRepository _salaryBases;
        private IAttendanceRepository _attendanceRecords;
        private IPerformanceRepository _performanceScores;
        private IPayrollResultRepository _payrollResults;
        private ISocialSecurityRepository _socialSecurities;
        private IOtherCompensationPenaltyRepository _otherCompensationPenalties;
        private IPayrollCostAnalysisRepository _payrollCostAnalyses;
        private IPayrollRuleRepository _payrollRules;
        private ISystemConfigRepository _systemConfigs;
        
        #endregion
        
        public UnitOfWork(IDatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }
        
        #region Repository属性
        
        public IEmployeeRepository Employees
        {
            get { return _employees ??= new EmployeeRepository(_databaseService); }
        }
        
        public ISalaryBaseRepository SalaryBases
        {
            get { return _salaryBases ??= new SalaryBaseRepository(_databaseService); }
        }
        
        public IAttendanceRepository AttendanceRecords
        {
            get { return _attendanceRecords ??= new AttendanceRepository(_databaseService); }
        }
        
        public IPerformanceRepository PerformanceScores
        {
            get { return _performanceScores ??= new PerformanceRepository(_databaseService); }
        }
        
        public IPayrollResultRepository PayrollResults
        {
            get { return _payrollResults ??= new PayrollResultRepository(_databaseService); }
        }
        
        public ISocialSecurityRepository SocialSecurities
        {
            get { return _socialSecurities ??= new SocialSecurityRepository(_databaseService); }
        }
        
        public IOtherCompensationPenaltyRepository OtherCompensationPenalties
        {
            get { return _otherCompensationPenalties ??= new OtherCompensationPenaltyRepository(_databaseService); }
        }
        
        public IPayrollCostAnalysisRepository PayrollCostAnalyses
        {
            get { return _payrollCostAnalyses ??= new PayrollCostAnalysisRepository(_databaseService); }
        }
        
        public IPayrollRuleRepository PayrollRules
        {
            get { return _payrollRules ??= new PayrollRuleRepository(_databaseService); }
        }
        
        public ISystemConfigRepository SystemConfigs
        {
            get { return _systemConfigs ??= new SystemConfigRepository(_databaseService); }
        }
        
        #endregion
        
        #region 事务管理
        
        public void BeginTransaction()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection(_databaseService.GetConnectionString());
                _connection.Open();
            }
            
            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction();
            }
        }
        
        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
        
        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }
        
        public async Task BeginTransactionAsync()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection(_databaseService.GetConnectionString());
                await _connection.OpenAsync();
            }
            
            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction();
            }
        }
        
        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        
        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }
        
        #endregion
        
        #region 保存更改
        
        public int SaveChanges()
        {
            // 在简单的Repository模式中，每个操作都是立即执行的
            // 这里主要用于事务的提交
            if (_transaction != null)
            {
                Commit();
                return 1; // 返回一个表示成功的值
            }
            return 0;
        }
        
        public async Task<int> SaveChangesAsync()
        {
            // 在简单的Repository模式中，每个操作都是立即执行的
            // 这里主要用于事务的提交
            if (_transaction != null)
            {
                await CommitAsync();
                return 1; // 返回一个表示成功的值
            }
            return 0;
        }
        
        #endregion
        
        #region 执行原生SQL
        
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<T>(sql, parameters);
        }
        
        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
        
        public async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            if (_transaction != null)
            {
                return await _connection.ExecuteAsync(sql, parameters, _transaction);
            }
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, parameters);
        }
        
        #endregion
        
        #region 批量操作
        
        public async Task<int> BulkInsertAsync<T>(IEnumerable<T> entities, string tableName) where T : class
        {
            if (entities == null || !entities.Any())
                return 0;
            
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.Name != "Id" && !p.Name.EndsWith("Id"))
                .ToArray();
            
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
            
            var sql = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";
            
            if (_transaction != null)
            {
                return await _connection.ExecuteAsync(sql, entities, _transaction);
            }
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, entities);
        }
        
        public async Task<int> BulkUpdateAsync<T>(IEnumerable<T> entities, string tableName, string keyColumn = "Id") where T : class
        {
            if (entities == null || !entities.Any())
                return 0;
            
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.Name != keyColumn)
                .ToArray();
            
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE {keyColumn} = @{keyColumn}";
            
            if (_transaction != null)
            {
                return await _connection.ExecuteAsync(sql, entities, _transaction);
            }
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, entities);
        }
        
        public async Task<int> BulkDeleteAsync(string tableName, string whereClause, object parameters = null)
        {
            var sql = $"DELETE FROM {tableName} WHERE {whereClause}";
            
            if (_transaction != null)
            {
                return await _connection.ExecuteAsync(sql, parameters, _transaction);
            }
            
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, parameters);
        }
        
        #endregion
        
        #region 数据库操作
        
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                return await _databaseService.TestConnectionAsync();
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<string> GetDatabaseVersionAsync()
        {
            try
            {
                const string sql = "SELECT sqlite_version()";
                return await QuerySingleOrDefaultAsync<string>(sql);
            }
            catch
            {
                return "Unknown";
            }
        }
        
        public async Task<bool> BackupDatabaseAsync(string backupPath)
        {
            try
            {
                return await _databaseService.BackupDatabaseAsync(backupPath);
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<bool> RestoreDatabaseAsync(string backupPath)
        {
            try
            {
                return await _databaseService.RestoreDatabaseAsync(backupPath);
            }
            catch
            {
                return false;
            }
        }
        
        #endregion
        
        #region IDisposable实现
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                try
                {
                    _transaction?.Rollback();
                }
                catch
                {
                    // 忽略回滚时的异常
                }
                finally
                {
                    _transaction?.Dispose();
                    _connection?.Dispose();
                }
                
                _disposed = true;
            }
        }
        
        ~UnitOfWork()
        {
            Dispose(false);
        }
        
        #endregion
    }
    
    /// <summary>
    /// 通用Repository实现类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    internal class GenericRepository<T> : BaseRepository<T> where T : class
    {
        public GenericRepository(IDatabaseService databaseService, string tableName, string primaryKeyColumn)
            : base(databaseService, tableName, primaryKeyColumn)
        {
        }
        
        protected override object EntityToInsertParameters(T entity)
        {
            // 使用反射获取所有属性（排除主键）
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.Name != _primaryKeyColumn)
                .ToDictionary(p => p.Name, p => p.GetValue(entity));
            
            // 添加审计字段
            properties["CreatedAt"] = DateTime.Now;
            properties["CreatedBy"] = "System";
            properties["UpdatedAt"] = DateTime.Now;
            properties["UpdatedBy"] = "System";
            
            return properties;
        }
        
        protected override object EntityToUpdateParameters(T entity)
        {
            // 使用反射获取所有属性
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p.GetValue(entity));
            
            // 更新审计字段
            properties["UpdatedAt"] = DateTime.Now;
            properties["UpdatedBy"] = "System";
            
            return properties;
        }
        
        protected override string GetInsertSql()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.Name != _primaryKeyColumn)
                .Select(p => p.Name)
                .Concat(new[] { "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" })
                .ToArray();
            
            var columnNames = string.Join(", ", properties);
            var parameterNames = string.Join(", ", properties.Select(p => "@" + p));
            
            return $"INSERT INTO {_tableName} ({columnNames}) VALUES ({parameterNames})";
        }
        
        protected override string GetUpdateSql()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.Name != _primaryKeyColumn)
                .Select(p => p.Name)
                .Concat(new[] { "UpdatedAt", "UpdatedBy" })
                .ToArray();
            
            var setClause = string.Join(", ", properties.Select(p => $"{p} = @{p}"));
            
            return $"UPDATE {_tableName} SET {setClause} WHERE {_primaryKeyColumn} = @{_primaryKeyColumn}";
        }
    }
}