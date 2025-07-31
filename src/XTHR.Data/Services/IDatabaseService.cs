using System;
using System.Threading.Tasks;

namespace XTHR.Data.Services
{
    /// <summary>
    /// 数据库服务接口
    /// 负责数据库的初始化、连接管理和基础操作
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 初始化数据库
        /// 创建数据库文件和表结构
        /// </summary>
        void InitializeDatabase();

        /// <summary>
        /// 异步初始化数据库
        /// </summary>
        /// <returns>初始化任务</returns>
        Task InitializeDatabaseAsync();

        /// <summary>
        /// 检查数据库连接是否正常
        /// </summary>
        /// <returns>连接是否正常</returns>
        bool TestConnection();

        /// <summary>
        /// 异步检查数据库连接
        /// </summary>
        /// <returns>连接检查任务</returns>
        Task<bool> TestConnectionAsync();

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        void BackupDatabase(string backupPath);

        /// <summary>
        /// 异步备份数据库
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <returns>备份任务</returns>
        Task BackupDatabaseAsync(string backupPath);

        /// <summary>
        /// 恢复数据库
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        void RestoreDatabase(string backupPath);

        /// <summary>
        /// 异步恢复数据库
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <returns>恢复任务</returns>
        Task RestoreDatabaseAsync(string backupPath);

        /// <summary>
        /// 获取数据库版本
        /// </summary>
        /// <returns>数据库版本</returns>
        string GetDatabaseVersion();

        /// <summary>
        /// 升级数据库结构
        /// </summary>
        /// <param name="targetVersion">目标版本</param>
        void UpgradeDatabase(string targetVersion);

        /// <summary>
        /// 异步升级数据库结构
        /// </summary>
        /// <param name="targetVersion">目标版本</param>
        /// <returns>升级任务</returns>
        Task UpgradeDatabaseAsync(string targetVersion);

        /// <summary>
        /// 清理数据库
        /// 删除过期数据、优化数据库性能
        /// </summary>
        void CleanupDatabase();

        /// <summary>
        /// 异步清理数据库
        /// </summary>
        /// <returns>清理任务</returns>
        Task CleanupDatabaseAsync();

        /// <summary>
        /// 获取数据库统计信息
        /// </summary>
        /// <returns>数据库统计信息</returns>
        DatabaseStatistics GetDatabaseStatistics();

        /// <summary>
        /// 异步获取数据库统计信息
        /// </summary>
        /// <returns>数据库统计信息任务</returns>
        Task<DatabaseStatistics> GetDatabaseStatisticsAsync();
    }

    /// <summary>
    /// 数据库统计信息
    /// </summary>
    public class DatabaseStatistics
    {
        /// <summary>
        /// 数据库文件大小（字节）
        /// </summary>
        public long DatabaseSize { get; set; }

        /// <summary>
        /// 员工总数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 工资记录总数
        /// </summary>
        public int PayrollRecordCount { get; set; }

        /// <summary>
        /// 考勤记录总数
        /// </summary>
        public int AttendanceRecordCount { get; set; }

        /// <summary>
        /// 最早记录日期
        /// </summary>
        public DateTime? EarliestRecordDate { get; set; }

        /// <summary>
        /// 最新记录日期
        /// </summary>
        public DateTime? LatestRecordDate { get; set; }

        /// <summary>
        /// 数据库创建时间
        /// </summary>
        public DateTime DatabaseCreatedDate { get; set; }

        /// <summary>
        /// 最后备份时间
        /// </summary>
        public DateTime? LastBackupDate { get; set; }
    }
}