using System;
using System.Threading.Tasks;
using XTHR.Data.Repositories;

namespace XTHR.Data.UnitOfWork
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Repository属性
        
        /// <summary>
        /// 员工信息仓储
        /// </summary>
        IEmployeeRepository Employees { get; }
        
        /// <summary>
        /// 工资基础信息仓储
        /// </summary>
        ISalaryBaseRepository SalaryBases { get; }
        
        /// <summary>
        /// 考勤记录仓储
        /// </summary>
        IAttendanceRepository AttendanceRecords { get; }
        
        /// <summary>
        /// 绩效考核仓储
        /// </summary>
        IPerformanceRepository PerformanceScores { get; }
        
        /// <summary>
        /// 工资核算结果仓储
        /// </summary>
        IPayrollResultRepository PayrollResults { get; }
        
        /// <summary>
        /// 社保参保信息仓储
        /// </summary>
        ISocialSecurityRepository SocialSecurities { get; }
        
        /// <summary>
        /// 其他奖惩信息仓储
        /// </summary>
        IOtherCompensationPenaltyRepository OtherCompensationPenalties { get; }
        
        /// <summary>
        /// 工资成本分析仓储
        /// </summary>
        IPayrollCostAnalysisRepository PayrollCostAnalyses { get; }
        
        /// <summary>
        /// 工资规则配置仓储
        /// </summary>
        IPayrollRuleRepository PayrollRules { get; }
        
        /// <summary>
        /// 系统配置仓储
        /// </summary>
        ISystemConfigRepository SystemConfigs { get; }
        
        #endregion
        
        #region 事务管理
        
        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTransaction();
        
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
        
        /// <summary>
        /// 异步开始事务
        /// </summary>
        Task BeginTransactionAsync();
        
        /// <summary>
        /// 异步提交事务
        /// </summary>
        Task CommitAsync();
        
        /// <summary>
        /// 异步回滚事务
        /// </summary>
        Task RollbackAsync();
        
        #endregion
        
        #region 保存更改
        
        /// <summary>
        /// 保存所有更改
        /// </summary>
        /// <returns>受影响的行数</returns>
        int SaveChanges();
        
        /// <summary>
        /// 异步保存所有更改
        /// </summary>
        /// <returns>受影响的行数</returns>
        Task<int> SaveChangesAsync();
        
        #endregion
        
        #region 执行原生SQL
        
        /// <summary>
        /// 执行原生SQL查询
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        Task<System.Collections.Generic.IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
        
        /// <summary>
        /// 执行原生SQL查询（单个结果）
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null);
        
        /// <summary>
        /// 执行非查询SQL
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        Task<int> ExecuteAsync(string sql, object parameters = null);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="tableName">表名</param>
        /// <returns>受影响的行数</returns>
        Task<int> BulkInsertAsync<T>(System.Collections.Generic.IEnumerable<T> entities, string tableName) where T : class;
        
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyColumn">主键列名</param>
        /// <returns>受影响的行数</returns>
        Task<int> BulkUpdateAsync<T>(System.Collections.Generic.IEnumerable<T> entities, string tableName, string keyColumn = "Id") where T : class;
        
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="whereClause">WHERE条件</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        Task<int> BulkDeleteAsync(string tableName, string whereClause, object parameters = null);
        
        #endregion
        
        #region 数据库操作
        
        /// <summary>
        /// 检查数据库连接
        /// </summary>
        /// <returns>连接是否正常</returns>
        Task<bool> TestConnectionAsync();
        
        /// <summary>
        /// 获取数据库版本信息
        /// </summary>
        /// <returns>版本信息</returns>
        Task<string> GetDatabaseVersionAsync();
        
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <returns>是否成功</returns>
        Task<bool> BackupDatabaseAsync(string backupPath);
        
        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <returns>是否成功</returns>
        Task<bool> RestoreDatabaseAsync(string backupPath);
        
        #endregion
    }
}