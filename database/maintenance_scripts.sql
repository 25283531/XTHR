-- ================================================================
-- XTHR人力资源管理系统 - 数据库维护脚本
-- 创建时间：2024年
-- 描述：包含数据库备份、清理、优化、监控等维护操作
-- ================================================================

USE XTHR;
GO

-- ================================================================
-- 1. 数据库备份相关脚本
-- ================================================================

-- 完整备份存储过程
CREATE OR ALTER PROCEDURE sp_BackupXTHRDatabase
    @BackupPath NVARCHAR(500) = 'C:\Database\Backup\XTHR\',
    @BackupType NVARCHAR(10) = 'FULL', -- FULL, DIFF, LOG
    @Compression BIT = 1,
    @Verification BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @FileName NVARCHAR(500);
    DECLARE @SQL NVARCHAR(2000);
    DECLARE @DateTime NVARCHAR(20) = FORMAT(GETDATE(), 'yyyyMMdd_HHmmss');
    DECLARE @DatabaseName NVARCHAR(100) = 'XTHR';
    
    -- 确保备份目录存在
    DECLARE @CreateDirCmd NVARCHAR(1000) = 'IF NOT EXIST "' + @BackupPath + '" MKDIR "' + @BackupPath + '"';
    EXEC xp_cmdshell @CreateDirCmd, NO_OUTPUT;
    
    -- 构造备份文件名
    IF @BackupType = 'FULL'
        SET @FileName = @BackupPath + @DatabaseName + '_Full_' + @DateTime + '.bak';
    ELSE IF @BackupType = 'DIFF'
        SET @FileName = @BackupPath + @DatabaseName + '_Diff_' + @DateTime + '.bak';
    ELSE IF @BackupType = 'LOG'
        SET @FileName = @BackupPath + @DatabaseName + '_Log_' + @DateTime + '.trn';
    
    -- 构造备份SQL
    SET @SQL = 'BACKUP ';
    
    IF @BackupType IN ('FULL', 'DIFF')
        SET @SQL = @SQL + 'DATABASE ' + @DatabaseName;
    ELSE
        SET @SQL = @SQL + 'LOG ' + @DatabaseName;
    
    SET @SQL = @SQL + ' TO DISK = ''' + @FileName + '''';
    
    IF @BackupType = 'DIFF'
        SET @SQL = @SQL + ' WITH DIFFERENTIAL';
    
    IF @Compression = 1
        SET @SQL = @SQL + ', COMPRESSION';
    
    SET @SQL = @SQL + ', FORMAT, INIT';
    
    -- 执行备份
    PRINT '开始备份：' + @FileName;
    PRINT '执行SQL：' + @SQL;
    
    BEGIN TRY
        EXEC sp_executesql @SQL;
        PRINT '备份完成：' + @FileName;
        
        -- 验证备份
        IF @Verification = 1
        BEGIN
            DECLARE @VerifySQL NVARCHAR(1000) = 'RESTORE VERIFYONLY FROM DISK = ''' + @FileName + '''';
            EXEC sp_executesql @VerifySQL;
            PRINT '备份验证通过：' + @FileName;
        END
        
    END TRY
    BEGIN CATCH
        PRINT 'ERROR: 备份失败 - ' + ERROR_MESSAGE();
        THROW;
    END CATCH
END;
GO

-- 自动备份清理存储过程
CREATE OR ALTER PROCEDURE sp_CleanupOldBackups
    @BackupPath NVARCHAR(500) = 'C:\Database\Backup\XTHR\',
    @RetentionDays INT = 30
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CleanupDate NVARCHAR(20) = FORMAT(DATEADD(DAY, -@RetentionDays, GETDATE()), 'yyyyMMdd');
    DECLARE @CMD NVARCHAR(1000);
    
    -- 删除过期的完整备份
    SET @CMD = 'FORFILES /P "' + @BackupPath + '" /M "XTHR_Full_*.bak" /D -' + CAST(@RetentionDays AS NVARCHAR(10)) + ' /C "cmd /c del @path"';
    EXEC xp_cmdshell @CMD, NO_OUTPUT;
    
    -- 删除过期的差异备份
    SET @CMD = 'FORFILES /P "' + @BackupPath + '" /M "XTHR_Diff_*.bak" /D -' + CAST(@RetentionDays AS NVARCHAR(10)) + ' /C "cmd /c del @path"';
    EXEC xp_cmdshell @CMD, NO_OUTPUT;
    
    -- 删除过期的日志备份（保留时间较短）
    SET @CMD = 'FORFILES /P "' + @BackupPath + '" /M "XTHR_Log_*.trn" /D -7 /C "cmd /c del @path"';
    EXEC xp_cmdshell @CMD, NO_OUTPUT;
    
    PRINT '备份清理完成，保留 ' + CAST(@RetentionDays AS NVARCHAR(10)) + ' 天内的备份文件';
END;
GO

-- ================================================================
-- 2. 数据库优化相关脚本
-- ================================================================

-- 重建索引存储过程
CREATE OR ALTER PROCEDURE sp_RebuildIndexes
    @TableName NVARCHAR(128) = NULL,
    @FragmentationThreshold FLOAT = 30.0,
    @OnlineRebuild BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @SQL NVARCHAR(2000);
    DECLARE @IndexName NVARCHAR(128);
    DECLARE @SchemaName NVARCHAR(128);
    DECLARE @ObjectName NVARCHAR(128);
    DECLARE @Fragmentation FLOAT;
    DECLARE @PageCount BIGINT;
    
    -- 创建临时表存储索引信息
    CREATE TABLE #IndexStats (
        SchemaName NVARCHAR(128),
        ObjectName NVARCHAR(128),
        IndexName NVARCHAR(128),
        Fragmentation FLOAT,
        PageCount BIGINT
    );
    
    -- 获取需要重建的索引
    INSERT INTO #IndexStats
    SELECT 
        s.name AS SchemaName,
        o.name AS ObjectName,
        i.name AS IndexName,
        ps.avg_fragmentation_in_percent AS Fragmentation,
        ps.page_count AS PageCount
    FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ps
    INNER JOIN sys.indexes i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
    INNER JOIN sys.objects o ON i.object_id = o.object_id
    INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
    WHERE ps.avg_fragmentation_in_percent > @FragmentationThreshold
        AND ps.page_count > 1000 -- 只处理页数较多的索引
        AND i.name IS NOT NULL
        AND o.type = 'U' -- 只处理用户表
        AND (@TableName IS NULL OR o.name = @TableName);
    
    DECLARE index_cursor CURSOR FOR
    SELECT SchemaName, ObjectName, IndexName, Fragmentation, PageCount
    FROM #IndexStats
    ORDER BY Fragmentation DESC;
    
    OPEN index_cursor;
    
    FETCH NEXT FROM index_cursor INTO @SchemaName, @ObjectName, @IndexName, @Fragmentation, @PageCount;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- 根据碎片率决定重建还是重组
        IF @Fragmentation > 50
        BEGIN
            -- 重建索引
            SET @SQL = 'ALTER INDEX [' + @IndexName + '] ON [' + @SchemaName + '].[' + @ObjectName + '] REBUILD';
            
            IF @OnlineRebuild = 1
                SET @SQL = @SQL + ' WITH (ONLINE = ON)';
            
            PRINT '重建索引：' + @SchemaName + '.' + @ObjectName + '.' + @IndexName + 
                  ' (碎片率: ' + CAST(@Fragmentation AS NVARCHAR(10)) + '%, 页数: ' + CAST(@PageCount AS NVARCHAR(20)) + ')';
        END
        ELSE
        BEGIN
            -- 重组索引
            SET @SQL = 'ALTER INDEX [' + @IndexName + '] ON [' + @SchemaName + '].[' + @ObjectName + '] REORGANIZE';
            
            PRINT '重组索引：' + @SchemaName + '.' + @ObjectName + '.' + @IndexName + 
                  ' (碎片率: ' + CAST(@Fragmentation AS NVARCHAR(10)) + '%, 页数: ' + CAST(@PageCount AS NVARCHAR(20)) + ')';
        END
        
        BEGIN TRY
            EXEC sp_executesql @SQL;
        END TRY
        BEGIN CATCH
            PRINT 'ERROR: 索引操作失败 - ' + ERROR_MESSAGE();
        END CATCH
        
        FETCH NEXT FROM index_cursor INTO @SchemaName, @ObjectName, @IndexName, @Fragmentation, @PageCount;
    END;
    
    CLOSE index_cursor;
    DEALLOCATE index_cursor;
    
    DROP TABLE #IndexStats;
    
    PRINT '索引维护完成';
END;
GO

-- 更新统计信息存储过程
CREATE OR ALTER PROCEDURE sp_UpdateStatistics
    @TableName NVARCHAR(128) = NULL,
    @SamplePercent INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @SQL NVARCHAR(1000);
    DECLARE @SchemaName NVARCHAR(128);
    DECLARE @ObjectName NVARCHAR(128);
    
    DECLARE table_cursor CURSOR FOR
    SELECT s.name, o.name
    FROM sys.objects o
    INNER JOIN sys.schemas s ON o.schema_id = s.schema_id
    WHERE o.type = 'U'
        AND (@TableName IS NULL OR o.name = @TableName);
    
    OPEN table_cursor;
    
    FETCH NEXT FROM table_cursor INTO @SchemaName, @ObjectName;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SQL = 'UPDATE STATISTICS [' + @SchemaName + '].[' + @ObjectName + ']';
        
        IF @SamplePercent IS NOT NULL
            SET @SQL = @SQL + ' WITH SAMPLE ' + CAST(@SamplePercent AS NVARCHAR(10)) + ' PERCENT';
        
        PRINT '更新统计信息：' + @SchemaName + '.' + @ObjectName;
        
        BEGIN TRY
            EXEC sp_executesql @SQL;
        END TRY
        BEGIN CATCH
            PRINT 'ERROR: 更新统计信息失败 - ' + ERROR_MESSAGE();
        END CATCH
        
        FETCH NEXT FROM table_cursor INTO @SchemaName, @ObjectName;
    END;
    
    CLOSE table_cursor;
    DEALLOCATE table_cursor;
    
    PRINT '统计信息更新完成';
END;
GO

-- ================================================================
-- 3. 数据清理相关脚本
-- ================================================================

-- 清理历史数据存储过程
CREATE OR ALTER PROCEDURE sp_CleanupHistoryData
    @RetentionMonths INT = 24, -- 保留月数
    @BatchSize INT = 1000,     -- 批处理大小
    @DryRun BIT = 1            -- 是否只是预览
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CutoffDate DATE = DATEADD(MONTH, -@RetentionMonths, GETDATE());
    DECLARE @RowCount INT;
    DECLARE @TotalDeleted INT = 0;
    
    PRINT '数据清理开始，截止日期：' + CAST(@CutoffDate AS NVARCHAR(10));
    PRINT '批处理大小：' + CAST(@BatchSize AS NVARCHAR(10));
    PRINT '预览模式：' + CASE WHEN @DryRun = 1 THEN '是' ELSE '否' END;
    PRINT '----------------------------------------';
    
    -- 清理考勤记录历史数据
    IF @DryRun = 1
    BEGIN
        SELECT COUNT(*) AS [考勤记录-待删除数量]
        FROM AttendanceRecords 
        WHERE AttendanceDate < @CutoffDate;
    END
    ELSE
    BEGIN
        WHILE 1 = 1
        BEGIN
            DELETE TOP (@BatchSize) FROM AttendanceRecords 
            WHERE AttendanceDate < @CutoffDate;
            
            SET @RowCount = @@ROWCOUNT;
            SET @TotalDeleted = @TotalDeleted + @RowCount;
            
            IF @RowCount = 0 BREAK;
            
            PRINT '考勤记录：已删除 ' + CAST(@RowCount AS NVARCHAR(10)) + ' 条记录';
            WAITFOR DELAY '00:00:01'; -- 避免长时间锁定
        END
        
        PRINT '考勤记录清理完成，共删除 ' + CAST(@TotalDeleted AS NVARCHAR(10)) + ' 条记录';
    END
    
    -- 清理工资核算历史数据
    SET @TotalDeleted = 0;
    
    IF @DryRun = 1
    BEGIN
        SELECT COUNT(*) AS [工资核算-待删除数量]
        FROM PayrollResults 
        WHERE PayrollPeriod < FORMAT(DATEADD(MONTH, -@RetentionMonths, GETDATE()), 'yyyy-MM');
    END
    ELSE
    BEGIN
        WHILE 1 = 1
        BEGIN
            DELETE TOP (@BatchSize) FROM PayrollResults 
            WHERE PayrollPeriod < FORMAT(DATEADD(MONTH, -@RetentionMonths, GETDATE()), 'yyyy-MM');
            
            SET @RowCount = @@ROWCOUNT;
            SET @TotalDeleted = @TotalDeleted + @RowCount;
            
            IF @RowCount = 0 BREAK;
            
            PRINT '工资核算：已删除 ' + CAST(@RowCount AS NVARCHAR(10)) + ' 条记录';
            WAITFOR DELAY '00:00:01';
        END
        
        PRINT '工资核算清理完成，共删除 ' + CAST(@TotalDeleted AS NVARCHAR(10)) + ' 条记录';
    END
    
    -- 清理审计日志（如果存在）
    IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AuditLogs')
    BEGIN
        SET @TotalDeleted = 0;
        
        IF @DryRun = 1
        BEGIN
            EXEC('SELECT COUNT(*) AS [审计日志-待删除数量] FROM AuditLogs WHERE CreatedAt < ''' + CAST(@CutoffDate AS NVARCHAR(10)) + '''');
        END
        ELSE
        BEGIN
            WHILE 1 = 1
            BEGIN
                EXEC('DELETE TOP (' + CAST(@BatchSize AS NVARCHAR(10)) + ') FROM AuditLogs WHERE CreatedAt < ''' + CAST(@CutoffDate AS NVARCHAR(10)) + '''');
                
                SET @RowCount = @@ROWCOUNT;
                SET @TotalDeleted = @TotalDeleted + @RowCount;
                
                IF @RowCount = 0 BREAK;
                
                PRINT '审计日志：已删除 ' + CAST(@RowCount AS NVARCHAR(10)) + ' 条记录';
                WAITFOR DELAY '00:00:01';
            END
            
            PRINT '审计日志清理完成，共删除 ' + CAST(@TotalDeleted AS NVARCHAR(10)) + ' 条记录';
        END
    END
    
    PRINT '数据清理完成';
END;
GO

-- ================================================================
-- 4. 数据库监控相关脚本
-- ================================================================

-- 数据库空间使用情况检查
CREATE OR ALTER PROCEDURE sp_CheckDatabaseSpace
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 数据库文件空间使用情况
    SELECT 
        name AS [文件名],
        physical_name AS [物理路径],
        CAST(size * 8.0 / 1024 AS DECIMAL(10,2)) AS [文件大小(MB)],
        CAST(FILEPROPERTY(name, 'SpaceUsed') * 8.0 / 1024 AS DECIMAL(10,2)) AS [已使用空间(MB)],
        CAST((size - FILEPROPERTY(name, 'SpaceUsed')) * 8.0 / 1024 AS DECIMAL(10,2)) AS [可用空间(MB)],
        CAST(FILEPROPERTY(name, 'SpaceUsed') * 100.0 / size AS DECIMAL(5,2)) AS [使用率(%)],
        type_desc AS [文件类型]
    FROM sys.database_files
    ORDER BY type, name;
    
    -- 表空间使用情况
    SELECT TOP 10
        s.name AS [架构名],
        t.name AS [表名],
        p.rows AS [行数],
        CAST(SUM(a.total_pages) * 8.0 / 1024 AS DECIMAL(10,2)) AS [总空间(MB)],
        CAST(SUM(a.used_pages) * 8.0 / 1024 AS DECIMAL(10,2)) AS [已使用空间(MB)],
        CAST((SUM(a.total_pages) - SUM(a.used_pages)) * 8.0 / 1024 AS DECIMAL(10,2)) AS [未使用空间(MB)]
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    INNER JOIN sys.indexes i ON t.object_id = i.object_id
    INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
    INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
    WHERE t.is_ms_shipped = 0
        AND i.object_id > 255
    GROUP BY s.name, t.name, p.rows
    ORDER BY SUM(a.total_pages) DESC;
END;
GO

-- 性能监控存储过程
CREATE OR ALTER PROCEDURE sp_PerformanceMonitor
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 当前连接数
    SELECT 
        COUNT(*) AS [当前连接数],
        SUM(CASE WHEN status = 'running' THEN 1 ELSE 0 END) AS [活动连接数],
        SUM(CASE WHEN status = 'sleeping' THEN 1 ELSE 0 END) AS [休眠连接数]
    FROM sys.dm_exec_sessions
    WHERE is_user_process = 1;
    
    -- 最耗时的查询（最近执行的）
    SELECT TOP 10
        qs.execution_count AS [执行次数],
        CAST(qs.total_elapsed_time / 1000000.0 AS DECIMAL(10,2)) AS [总耗时(秒)],
        CAST(qs.total_elapsed_time / qs.execution_count / 1000000.0 AS DECIMAL(10,4)) AS [平均耗时(秒)],
        qs.last_execution_time AS [最后执行时间],
        SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
            ((CASE qs.statement_end_offset
                WHEN -1 THEN DATALENGTH(qt.text)
                ELSE qs.statement_end_offset
            END - qs.statement_start_offset)/2)+1) AS [SQL语句]
    FROM sys.dm_exec_query_stats qs
    CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
    WHERE qt.dbid = DB_ID()
    ORDER BY qs.total_elapsed_time DESC;
    
    -- 等待统计
    SELECT TOP 10
        wait_type AS [等待类型],
        waiting_tasks_count AS [等待任务数],
        CAST(wait_time_ms / 1000.0 AS DECIMAL(10,2)) AS [等待时间(秒)],
        CAST(signal_wait_time_ms / 1000.0 AS DECIMAL(10,2)) AS [信号等待时间(秒)],
        CAST(wait_time_ms * 100.0 / SUM(wait_time_ms) OVER() AS DECIMAL(5,2)) AS [等待时间占比(%)]
    FROM sys.dm_os_wait_stats
    WHERE wait_type NOT IN (
        'CLR_SEMAPHORE', 'LAZYWRITER_SLEEP', 'RESOURCE_QUEUE', 'SLEEP_TASK',
        'SLEEP_SYSTEMTASK', 'SQLTRACE_BUFFER_FLUSH', 'WAITFOR', 'LOGMGR_QUEUE',
        'CHECKPOINT_QUEUE', 'REQUEST_FOR_DEADLOCK_SEARCH', 'XE_TIMER_EVENT',
        'BROKER_TO_FLUSH', 'BROKER_TASK_STOP', 'CLR_MANUAL_EVENT', 'CLR_AUTO_EVENT',
        'DISPATCHER_QUEUE_SEMAPHORE', 'FT_IFTS_SCHEDULER_IDLE_WAIT',
        'XE_DISPATCHER_WAIT', 'XE_DISPATCHER_JOIN', 'SQLTRACE_INCREMENTAL_FLUSH_SLEEP'
    )
    ORDER BY wait_time_ms DESC;
END;
GO

-- ================================================================
-- 5. 数据库健康检查脚本
-- ================================================================

-- 综合健康检查存储过程
CREATE OR ALTER PROCEDURE sp_DatabaseHealthCheck
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT '==========================================';
    PRINT 'XTHR数据库健康检查报告';
    PRINT '检查时间：' + CAST(GETDATE() AS NVARCHAR(20));
    PRINT '==========================================';
    
    -- 1. 数据库基本信息
    PRINT '1. 数据库基本信息';
    SELECT 
        name AS [数据库名],
        database_id AS [数据库ID],
        create_date AS [创建时间],
        collation_name AS [排序规则],
        state_desc AS [状态],
        recovery_model_desc AS [恢复模式],
        compatibility_level AS [兼容级别]
    FROM sys.databases
    WHERE name = 'XTHR';
    
    -- 2. 文件空间使用情况
    PRINT '2. 文件空间使用情况';
    EXEC sp_CheckDatabaseSpace;
    
    -- 3. 索引碎片情况
    PRINT '3. 索引碎片情况（碎片率>30%的索引）';
    SELECT TOP 10
        OBJECT_SCHEMA_NAME(ps.object_id) AS [架构名],
        OBJECT_NAME(ps.object_id) AS [表名],
        i.name AS [索引名],
        ps.index_type_desc AS [索引类型],
        CAST(ps.avg_fragmentation_in_percent AS DECIMAL(5,2)) AS [碎片率(%)],
        ps.page_count AS [页数]
    FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ps
    INNER JOIN sys.indexes i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
    WHERE ps.avg_fragmentation_in_percent > 30
        AND ps.page_count > 100
        AND i.name IS NOT NULL
    ORDER BY ps.avg_fragmentation_in_percent DESC;
    
    -- 4. 表行数统计
    PRINT '4. 主要表行数统计';
    SELECT 
        OBJECT_SCHEMA_NAME(object_id) AS [架构名],
        OBJECT_NAME(object_id) AS [表名],
        SUM(rows) AS [行数]
    FROM sys.partitions
    WHERE object_id IN (
        OBJECT_ID('Employees'),
        OBJECT_ID('SalaryBases'),
        OBJECT_ID('AttendanceRecords'),
        OBJECT_ID('PerformanceScores'),
        OBJECT_ID('PayrollResults'),
        OBJECT_ID('SocialSecurities'),
        OBJECT_ID('SystemConfigs')
    )
    AND index_id IN (0, 1) -- 堆或聚集索引
    GROUP BY object_id
    ORDER BY SUM(rows) DESC;
    
    -- 5. 最近的备份信息
    PRINT '5. 最近的备份信息';
    SELECT TOP 5
        database_name AS [数据库名],
        type AS [备份类型],
        backup_start_date AS [备份开始时间],
        backup_finish_date AS [备份完成时间],
        CAST(backup_size / 1024.0 / 1024.0 AS DECIMAL(10,2)) AS [备份大小(MB)],
        CAST(compressed_backup_size / 1024.0 / 1024.0 AS DECIMAL(10,2)) AS [压缩后大小(MB)]
    FROM msdb.dbo.backupset
    WHERE database_name = 'XTHR'
    ORDER BY backup_start_date DESC;
    
    -- 6. 错误日志检查（最近的错误）
    PRINT '6. 系统状态检查';
    SELECT 
        @@SERVERNAME AS [服务器名],
        @@VERSION AS [SQL Server版本],
        GETDATE() AS [当前时间],
        @@CONNECTIONS AS [连接数],
        @@CPU_BUSY AS [CPU忙碌时间],
        @@IDLE AS [空闲时间],
        @@IO_BUSY AS [IO忙碌时间];
    
    PRINT '==========================================';
    PRINT '健康检查完成';
    PRINT '==========================================';
END;
GO

-- ================================================================
-- 6. 维护计划脚本
-- ================================================================

-- 每日维护任务
CREATE OR ALTER PROCEDURE sp_DailyMaintenance
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT '开始执行每日维护任务...';
    
    -- 1. 更新统计信息
    PRINT '1. 更新统计信息';
    EXEC sp_UpdateStatistics;
    
    -- 2. 重组碎片率较高的索引
    PRINT '2. 重组索引';
    EXEC sp_RebuildIndexes @FragmentationThreshold = 50;
    
    -- 3. 完整备份（如果是周日）
    IF DATEPART(WEEKDAY, GETDATE()) = 1 -- 周日
    BEGIN
        PRINT '3. 执行完整备份';
        EXEC sp_BackupXTHRDatabase @BackupType = 'FULL';
    END
    ELSE
    BEGIN
        PRINT '3. 执行差异备份';
        EXEC sp_BackupXTHRDatabase @BackupType = 'DIFF';
    END
    
    -- 4. 清理旧备份
    PRINT '4. 清理旧备份';
    EXEC sp_CleanupOldBackups;
    
    PRINT '每日维护任务完成';
END;
GO

-- 每周维护任务
CREATE OR ALTER PROCEDURE sp_WeeklyMaintenance
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT '开始执行每周维护任务...';
    
    -- 1. 重建所有索引
    PRINT '1. 重建索引';
    EXEC sp_RebuildIndexes @FragmentationThreshold = 10;
    
    -- 2. 数据库健康检查
    PRINT '2. 数据库健康检查';
    EXEC sp_DatabaseHealthCheck;
    
    -- 3. 清理历史数据（预览模式）
    PRINT '3. 历史数据清理预览';
    EXEC sp_CleanupHistoryData @DryRun = 1;
    
    PRINT '每周维护任务完成';
END;
GO

-- 每月维护任务
CREATE OR ALTER PROCEDURE sp_MonthlyMaintenance
AS
BEGIN
    SET NOCOUNT ON;
    
    PRINT '开始执行每月维护任务...';
    
    -- 1. 清理历史数据
    PRINT '1. 清理历史数据';
    EXEC sp_CleanupHistoryData @DryRun = 0;
    
    -- 2. 数据库完整性检查
    PRINT '2. 数据库完整性检查';
    DBCC CHECKDB('XTHR') WITH NO_INFOMSGS;
    
    -- 3. 完整备份
    PRINT '3. 月度完整备份';
    EXEC sp_BackupXTHRDatabase @BackupType = 'FULL';
    
    PRINT '每月维护任务完成';
END;
GO

PRINT '==========================================';
PRINT 'XTHR数据库维护脚本创建完成';
PRINT '==========================================';
PRINT '可用的维护存储过程：';
PRINT '- sp_BackupXTHRDatabase: 数据库备份';
PRINT '- sp_CleanupOldBackups: 清理旧备份';
PRINT '- sp_RebuildIndexes: 重建索引';
PRINT '- sp_UpdateStatistics: 更新统计信息';
PRINT '- sp_CleanupHistoryData: 清理历史数据';
PRINT '- sp_CheckDatabaseSpace: 检查空间使用';
PRINT '- sp_PerformanceMonitor: 性能监控';
PRINT '- sp_DatabaseHealthCheck: 健康检查';
PRINT '- sp_DailyMaintenance: 每日维护';
PRINT '- sp_WeeklyMaintenance: 每周维护';
PRINT '- sp_MonthlyMaintenance: 每月维护';
PRINT '==========================================';
GO