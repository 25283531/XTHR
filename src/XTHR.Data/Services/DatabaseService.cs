using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Dapper;

namespace XTHR.Data.Services
{
    /// <summary>
    /// 数据库服务实现类
    /// 负责SQLite数据库的管理和操作
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _databasePath;
        private readonly string _connectionString;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="configuration">配置服务</param>
        public DatabaseService(ILogger<DatabaseService> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            // 获取数据库路径配置
            var databasePath = _configuration.GetConnectionString("DefaultConnection") ?? "database/payroll.db";
            _databasePath = Path.GetFullPath(databasePath.Replace("Data Source=", "").Split(';')[0]);
            _connectionString = $"Data Source={_databasePath};Version=3;";

            // 确保数据库目录存在
            var databaseDirectory = Path.GetDirectoryName(_databasePath);
            if (!string.IsNullOrEmpty(databaseDirectory) && !Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        public string ConnectionString => _connectionString;

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public void InitializeDatabase()
        {
            try
            {
                _logger.LogInformation("开始初始化数据库：{DatabasePath}", _databasePath);

                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                // 读取并执行初始化脚本
                var initScript = GetInitializationScript();
                connection.Execute(initScript);

                _logger.LogInformation("数据库初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库初始化失败");
                throw;
            }
        }

        /// <summary>
        /// 异步初始化数据库
        /// </summary>
        public async Task InitializeDatabaseAsync()
        {
            try
            {
                _logger.LogInformation("开始异步初始化数据库：{DatabasePath}", _databasePath);

                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();

                // 读取并执行初始化脚本
                var initScript = GetInitializationScript();
                await connection.ExecuteAsync(initScript);

                _logger.LogInformation("数据库异步初始化完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库异步初始化失败");
                throw;
            }
        }

        /// <summary>
        /// 检查数据库连接
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                var result = connection.QuerySingle<int>("SELECT 1");
                return result == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库连接测试失败");
                return false;
            }
        }

        /// <summary>
        /// 异步检查数据库连接
        /// </summary>
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();
                var result = await connection.QuerySingleAsync<int>("SELECT 1");
                return result == 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库连接异步测试失败");
                return false;
            }
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        public void BackupDatabase(string backupPath)
        {
            try
            {
                _logger.LogInformation("开始备份数据库到：{BackupPath}", backupPath);

                // 确保备份目录存在
                var backupDirectory = Path.GetDirectoryName(backupPath);
                if (!string.IsNullOrEmpty(backupDirectory) && !Directory.Exists(backupDirectory))
                {
                    Directory.CreateDirectory(backupDirectory);
                }

                // 复制数据库文件
                File.Copy(_databasePath, backupPath, true);

                _logger.LogInformation("数据库备份完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库备份失败");
                throw;
            }
        }

        /// <summary>
        /// 异步备份数据库
        /// </summary>
        public async Task BackupDatabaseAsync(string backupPath)
        {
            await Task.Run(() => BackupDatabase(backupPath));
        }

        /// <summary>
        /// 恢复数据库
        /// </summary>
        public void RestoreDatabase(string backupPath)
        {
            try
            {
                _logger.LogInformation("开始从备份恢复数据库：{BackupPath}", backupPath);

                if (!File.Exists(backupPath))
                {
                    throw new FileNotFoundException($"备份文件不存在：{backupPath}");
                }

                // 复制备份文件到数据库位置
                File.Copy(backupPath, _databasePath, true);

                _logger.LogInformation("数据库恢复完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库恢复失败");
                throw;
            }
        }

        /// <summary>
        /// 异步恢复数据库
        /// </summary>
        public async Task RestoreDatabaseAsync(string backupPath)
        {
            await Task.Run(() => RestoreDatabase(backupPath));
        }

        /// <summary>
        /// 获取数据库版本
        /// </summary>
        public string GetDatabaseVersion()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                var version = connection.QuerySingleOrDefault<string>(
                    "SELECT ConfigValue FROM SystemConfig WHERE ConfigKey = 'DATABASE_VERSION'");

                return version ?? "1.0.0";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取数据库版本失败");
                return "Unknown";
            }
        }

        /// <summary>
        /// 升级数据库
        /// </summary>
        public void UpgradeDatabase(string targetVersion)
        {
            try
            {
                _logger.LogInformation("开始升级数据库到版本：{TargetVersion}", targetVersion);

                var currentVersion = GetDatabaseVersion();
                if (currentVersion == targetVersion)
                {
                    _logger.LogInformation("数据库已是最新版本，无需升级");
                    return;
                }

                // 这里可以添加版本升级逻辑
                // 根据当前版本和目标版本执行相应的升级脚本

                _logger.LogInformation("数据库升级完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库升级失败");
                throw;
            }
        }

        /// <summary>
        /// 异步升级数据库
        /// </summary>
        public async Task UpgradeDatabaseAsync(string targetVersion)
        {
            await Task.Run(() => UpgradeDatabase(targetVersion));
        }

        /// <summary>
        /// 清理数据库
        /// </summary>
        public void CleanupDatabase()
        {
            try
            {
                _logger.LogInformation("开始清理数据库");

                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                // 执行VACUUM命令优化数据库
                connection.Execute("VACUUM;");

                // 分析表以更新统计信息
                connection.Execute("ANALYZE;");

                _logger.LogInformation("数据库清理完成");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "数据库清理失败");
                throw;
            }
        }

        /// <summary>
        /// 异步清理数据库
        /// </summary>
        public async Task CleanupDatabaseAsync()
        {
            await Task.Run(() => CleanupDatabase());
        }

        /// <summary>
        /// 获取数据库统计信息
        /// </summary>
        public DatabaseStatistics GetDatabaseStatistics()
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();

                var statistics = new DatabaseStatistics();

                // 获取数据库文件大小
                if (File.Exists(_databasePath))
                {
                    statistics.DatabaseSize = new FileInfo(_databasePath).Length;
                    statistics.DatabaseCreatedDate = File.GetCreationTime(_databasePath);
                }

                // 获取各表记录数
                statistics.EmployeeCount = connection.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM Employees");
                statistics.PayrollRecordCount = connection.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM PayrollResults");
                statistics.AttendanceRecordCount = connection.QuerySingleOrDefault<int>("SELECT COUNT(*) FROM AttendanceRecords");

                // 获取记录日期范围
                statistics.EarliestRecordDate = connection.QuerySingleOrDefault<DateTime?>(
                    "SELECT MIN(AttendanceDate) FROM AttendanceRecords");
                statistics.LatestRecordDate = connection.QuerySingleOrDefault<DateTime?>(
                    "SELECT MAX(AttendanceDate) FROM AttendanceRecords");

                return statistics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取数据库统计信息失败");
                throw;
            }
        }

        /// <summary>
        /// 异步获取数据库统计信息
        /// </summary>
        public async Task<DatabaseStatistics> GetDatabaseStatisticsAsync()
        {
            return await Task.Run(() => GetDatabaseStatistics());
        }

        /// <summary>
        /// 获取数据库初始化脚本
        /// </summary>
        private string GetInitializationScript()
        {
            var scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database", "init_database.sql");
            
            if (File.Exists(scriptPath))
            {
                return File.ReadAllText(scriptPath);
            }

            // 如果脚本文件不存在，返回基本的表创建语句
            return GetDefaultInitializationScript();
        }

        /// <summary>
        /// 获取默认的初始化脚本
        /// </summary>
        private string GetDefaultInitializationScript()
        {
            return @"
                -- 基本表结构创建脚本
                CREATE TABLE IF NOT EXISTS Employees (
                    EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmployeeCode TEXT NOT NULL UNIQUE,
                    Name TEXT NOT NULL,
                    Department TEXT NOT NULL,
                    Position TEXT NOT NULL,
                    JobGrade TEXT,
                    HireDate DATE NOT NULL,
                    IDNumber TEXT UNIQUE,
                    ContactInfo TEXT,
                    IsActive BOOLEAN DEFAULT 1,
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                CREATE TABLE IF NOT EXISTS SystemConfig (
                    ConfigID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ConfigKey TEXT NOT NULL UNIQUE,
                    ConfigValue TEXT NOT NULL,
                    ConfigType TEXT NOT NULL,
                    Description TEXT,
                    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                INSERT OR IGNORE INTO SystemConfig (ConfigKey, ConfigValue, ConfigType, Description) VALUES
                ('DATABASE_VERSION', '1.0.0', 'STRING', '数据库版本');
            ";
        }
    }
}