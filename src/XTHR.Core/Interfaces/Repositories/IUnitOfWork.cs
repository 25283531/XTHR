using System;
using System.Threading.Tasks;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region 仓储属性
        
        /// <summary>
        /// 员工仓储
        /// </summary>
        IEmployeeRepository Employees { get; }
        
        /// <summary>
        /// 工资管理仓储
        /// </summary>
        IPayrollRepository Payrolls { get; }
        
        /// <summary>
        /// 考勤管理仓储
        /// </summary>
        IAttendanceRepository Attendances { get; }
        
        /// <summary>
        /// 系统配置仓储
        /// </summary>
        ISystemConfigRepository SystemConfigs { get; }
        
        #endregion
        
        #region 事务管理
        
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>事务对象</returns>
        Task<IDbTransaction> BeginTransactionAsync();
        
        /// <summary>
        /// 提交所有更改
        /// </summary>
        /// <returns>受影响的行数</returns>
        Task<int> SaveChangesAsync();
        
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <returns>受影响的行数</returns>
        Task<int> CommitTransactionAsync(IDbTransaction transaction);
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        Task RollbackTransactionAsync(IDbTransaction transaction);
        
        /// <summary>
        /// 执行事务操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="operation">事务操作</param>
        /// <returns>操作结果</returns>
        Task<T> ExecuteInTransactionAsync<T>(Func<IDbTransaction, Task<T>> operation);
        
        /// <summary>
        /// 执行事务操作
        /// </summary>
        /// <param name="operation">事务操作</param>
        Task ExecuteInTransactionAsync(Func<IDbTransaction, Task> operation);
        
        #endregion
        
        #region 批量操作
        
        /// <summary>
        /// 批量保存更改
        /// </summary>
        /// <param name="batchSize">批次大小</param>
        /// <returns>受影响的行数</returns>
        Task<int> BatchSaveChangesAsync(int batchSize = 1000);
        
        /// <summary>
        /// 清除所有更改跟踪
        /// </summary>
        void ClearChangeTracker();
        
        /// <summary>
        /// 重新加载实体
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        Task ReloadEntityAsync<TEntity>(TEntity entity) where TEntity : class;
        
        #endregion
        
        #region 缓存管理
        
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        Task ClearAllCacheAsync();
        
        /// <summary>
        /// 刷新所有缓存
        /// </summary>
        Task RefreshAllCacheAsync();
        
        /// <summary>
        /// 清除指定类型的缓存
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        Task ClearCacheAsync<TEntity>() where TEntity : class;
        
        /// <summary>
        /// 刷新指定类型的缓存
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        Task RefreshCacheAsync<TEntity>() where TEntity : class;
        
        #endregion
        
        #region 连接管理
        
        /// <summary>
        /// 获取数据库连接状态
        /// </summary>
        /// <returns>连接状态</returns>
        Task<DatabaseConnectionStatus> GetConnectionStatusAsync();
        
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <returns>连接是否正常</returns>
        Task<bool> TestConnectionAsync();
        
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <returns>数据库信息</returns>
        Task<DatabaseInfo> GetDatabaseInfoAsync();
        
        #endregion
        
        #region 性能监控
        
        /// <summary>
        /// 获取性能统计信息
        /// </summary>
        /// <returns>性能统计</returns>
        Task<PerformanceStatistics> GetPerformanceStatisticsAsync();
        
        /// <summary>
        /// 重置性能统计
        /// </summary>
        void ResetPerformanceStatistics();
        
        /// <summary>
        /// 启用性能监控
        /// </summary>
        void EnablePerformanceMonitoring();
        
        /// <summary>
        /// 禁用性能监控
        /// </summary>
        void DisablePerformanceMonitoring();
        
        #endregion
        
        #region 健康检查
        
        /// <summary>
        /// 执行健康检查
        /// </summary>
        /// <returns>健康检查结果</returns>
        Task<HealthCheckResult> PerformHealthCheckAsync();
        
        /// <summary>
        /// 获取系统状态
        /// </summary>
        /// <returns>系统状态</returns>
        Task<SystemStatus> GetSystemStatusAsync();
        
        #endregion
    }
    
    /// <summary>
    /// 数据库连接状态
    /// </summary>
    public enum DatabaseConnectionStatus
    {
        /// <summary>
        /// 已连接
        /// </summary>
        Connected,
        
        /// <summary>
        /// 已断开
        /// </summary>
        Disconnected,
        
        /// <summary>
        /// 连接中
        /// </summary>
        Connecting,
        
        /// <summary>
        /// 连接失败
        /// </summary>
        Failed,
        
        /// <summary>
        /// 未知状态
        /// </summary>
        Unknown
    }
    
    /// <summary>
    /// 数据库信息
    /// </summary>
    public class DatabaseInfo
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DatabaseType { get; set; }
        
        /// <summary>
        /// 数据库版本
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }
        
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName { get; set; }
        
        /// <summary>
        /// 连接字符串（脱敏）
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        /// 最大连接数
        /// </summary>
        public int MaxConnections { get; set; }
        
        /// <summary>
        /// 当前连接数
        /// </summary>
        public int CurrentConnections { get; set; }
        
        /// <summary>
        /// 数据库大小（MB）
        /// </summary>
        public long DatabaseSizeMB { get; set; }
    }
    
    /// <summary>
    /// 性能统计信息
    /// </summary>
    public class PerformanceStatistics
    {
        /// <summary>
        /// 总查询次数
        /// </summary>
        public long TotalQueries { get; set; }
        
        /// <summary>
        /// 总执行时间（毫秒）
        /// </summary>
        public long TotalExecutionTimeMs { get; set; }
        
        /// <summary>
        /// 平均执行时间（毫秒）
        /// </summary>
        public double AverageExecutionTimeMs { get; set; }
        
        /// <summary>
        /// 最慢查询时间（毫秒）
        /// </summary>
        public long SlowestQueryTimeMs { get; set; }
        
        /// <summary>
        /// 最快查询时间（毫秒）
        /// </summary>
        public long FastestQueryTimeMs { get; set; }
        
        /// <summary>
        /// 缓存命中次数
        /// </summary>
        public long CacheHits { get; set; }
        
        /// <summary>
        /// 缓存未命中次数
        /// </summary>
        public long CacheMisses { get; set; }
        
        /// <summary>
        /// 缓存命中率
        /// </summary>
        public double CacheHitRate { get; set; }
        
        /// <summary>
        /// 事务提交次数
        /// </summary>
        public long TransactionCommits { get; set; }
        
        /// <summary>
        /// 事务回滚次数
        /// </summary>
        public long TransactionRollbacks { get; set; }
        
        /// <summary>
        /// 统计开始时间
        /// </summary>
        public DateTime StatisticsStartTime { get; set; }
        
        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime StatisticsEndTime { get; set; }
    }
    
    /// <summary>
    /// 健康检查结果
    /// </summary>
    public class HealthCheckResult
    {
        /// <summary>
        /// 检查是否通过
        /// </summary>
        public bool IsHealthy { get; set; }
        
        /// <summary>
        /// 检查时间
        /// </summary>
        public DateTime CheckTime { get; set; }
        
        /// <summary>
        /// 响应时间（毫秒）
        /// </summary>
        public long ResponseTimeMs { get; set; }
        
        /// <summary>
        /// 数据库连接状态
        /// </summary>
        public DatabaseConnectionStatus ConnectionStatus { get; set; }
        
        /// <summary>
        /// 检查详情
        /// </summary>
        public List<HealthCheckDetail> Details { get; set; } = new List<HealthCheckDetail>();
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 健康检查详情
    /// </summary>
    public class HealthCheckDetail
    {
        /// <summary>
        /// 检查项名称
        /// </summary>
        public string CheckName { get; set; }
        
        /// <summary>
        /// 检查状态
        /// </summary>
        public HealthCheckStatus Status { get; set; }
        
        /// <summary>
        /// 检查结果描述
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// 响应时间（毫秒）
        /// </summary>
        public long ResponseTimeMs { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    
    /// <summary>
    /// 健康检查状态
    /// </summary>
    public enum HealthCheckStatus
    {
        /// <summary>
        /// 健康
        /// </summary>
        Healthy,
        
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        
        /// <summary>
        /// 不健康
        /// </summary>
        Unhealthy,
        
        /// <summary>
        /// 未知
        /// </summary>
        Unknown
    }
    
    /// <summary>
    /// 系统状态
    /// </summary>
    public class SystemStatus
    {
        /// <summary>
        /// 系统是否正常
        /// </summary>
        public bool IsOnline { get; set; }
        
        /// <summary>
        /// 系统启动时间
        /// </summary>
        public DateTime StartupTime { get; set; }
        
        /// <summary>
        /// 系统运行时间
        /// </summary>
        public TimeSpan Uptime { get; set; }
        
        /// <summary>
        /// 数据库状态
        /// </summary>
        public DatabaseConnectionStatus DatabaseStatus { get; set; }
        
        /// <summary>
        /// 缓存状态
        /// </summary>
        public string CacheStatus { get; set; }
        
        /// <summary>
        /// 内存使用情况（MB）
        /// </summary>
        public long MemoryUsageMB { get; set; }
        
        /// <summary>
        /// CPU使用率（%）
        /// </summary>
        public double CpuUsagePercent { get; set; }
        
        /// <summary>
        /// 活跃连接数
        /// </summary>
        public int ActiveConnections { get; set; }
        
        /// <summary>
        /// 当前用户数
        /// </summary>
        public int CurrentUsers { get; set; }
        
        /// <summary>
        /// 系统版本
        /// </summary>
        public string SystemVersion { get; set; }
        
        /// <summary>
        /// 环境信息
        /// </summary>
        public string Environment { get; set; }
    }
}