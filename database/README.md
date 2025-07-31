# XTHR人力资源管理系统 - 数据库部署指南

## 概述

本目录包含XTHR人力资源管理系统的完整数据库设计和部署脚本。数据库采用SQL Server设计，支持完整的人力资源管理业务流程。

## 文件说明

### 核心文件

| 文件名 | 描述 | 用途 |
|--------|------|------|
| `table_structure.sql` | 完整表结构定义 | 创建所有业务表和索引 |
| `deploy_database.sql` | 数据库部署脚本 | 一键部署数据库环境 |
| `maintenance_scripts.sql` | 维护脚本集合 | 数据库日常维护操作 |
| `table_design_document.md` | 设计文档 | 详细的表结构设计说明 |
| `README.md` | 部署指南 | 本文档 |

### 脚本功能对比

| 脚本 | 环境检查 | 数据库创建 | 用户权限 | 表结构 | 初始数据 | 维护功能 |
|------|----------|------------|----------|--------|----------|----------|
| `deploy_database.sql` | ✅ | ✅ | ✅ | 部分 | ✅ | ❌ |
| `table_structure.sql` | ❌ | ❌ | ❌ | 完整 | ✅ | ❌ |
| `maintenance_scripts.sql` | ❌ | ❌ | ❌ | ❌ | ❌ | ✅ |

## 系统要求

### 最低要求
- SQL Server 2016 或更高版本
- 磁盘空间：至少 1GB 可用空间
- 内存：建议 4GB 或以上
- 操作系统：Windows Server 2012 R2 或更高版本

### 推荐配置
- SQL Server 2019 或 2022
- 磁盘空间：10GB 或以上（用于数据增长和备份）
- 内存：8GB 或以上
- SSD 存储（提高性能）

## 快速部署

### 方式一：一键部署（推荐）

1. **打开 SQL Server Management Studio (SSMS)**

2. **连接到 SQL Server 实例**

3. **执行部署脚本**
   ```sql
   -- 执行一键部署脚本
   :r "d:\Git\XTHR\database\deploy_database.sql"
   ```

4. **验证部署结果**
   ```sql
   USE XTHR;
   SELECT COUNT(*) AS TableCount FROM sys.tables;
   SELECT COUNT(*) AS EmployeeCount FROM Employees;
   SELECT COUNT(*) AS ConfigCount FROM SystemConfigs;
   ```

### 方式二：分步部署

1. **创建数据库和用户**
   ```sql
   :r "d:\Git\XTHR\database\init_database.sql"
   ```

2. **创建完整表结构**
   ```sql
   :r "d:\Git\XTHR\database\table_structure.sql"
   ```

3. **安装维护脚本**
   ```sql
   :r "d:\Git\XTHR\database\maintenance_scripts.sql"
   ```

## 数据库结构

### 核心业务表

1. **员工管理**
   - `Employees` - 员工基本信息
   - `SalaryBases` - 工资基础信息

2. **考勤管理**
   - `AttendanceRecords` - 考勤记录

3. **绩效管理**
   - `PerformanceScores` - 绩效考核记录

4. **薪资管理**
   - `PayrollResults` - 工资核算结果
   - `PayrollRules` - 工资计算规则
   - `PayrollCostAnalyses` - 成本分析

5. **社保管理**
   - `SocialSecurities` - 社保参保信息

6. **其他管理**
   - `OtherCompensationPenalties` - 奖惩信息
   - `SystemConfigs` - 系统配置

### 数据库用户

| 用户名 | 密码 | 权限 | 用途 |
|--------|------|------|------|
| `XTHR_AppUser` | `XTHR@2024!App` | 读写执行 | 应用程序连接 |
| `XTHR_ReadOnlyUser` | `XTHR@2024!Read` | 只读 | 报表查询 |

## 配置说明

### 连接字符串示例

**应用程序连接**
```
Server=localhost;Database=XTHR;User Id=XTHR_AppUser;Password=XTHR@2024!App;TrustServerCertificate=true;
```

**只读连接**
```
Server=localhost;Database=XTHR;User Id=XTHR_ReadOnlyUser;Password=XTHR@2024!Read;TrustServerCertificate=true;
```

### 重要配置参数

系统配置表 `SystemConfigs` 中的关键参数：

| 配置键 | 默认值 | 说明 |
|--------|--------|------|
| `Payroll.TaxThreshold` | 5000 | 个税起征点 |
| `Attendance.WorkHoursPerDay` | 8 | 每日标准工作小时 |
| `Attendance.WorkDaysPerMonth` | 22 | 每月标准工作天数 |
| `SocialSecurity.PensionEmployeeRate` | 0.08 | 个人养老保险费率 |

## 维护操作

### 日常维护

```sql
-- 执行每日维护任务
EXEC sp_DailyMaintenance;

-- 检查数据库健康状况
EXEC sp_DatabaseHealthCheck;

-- 检查空间使用情况
EXEC sp_CheckDatabaseSpace;
```

### 备份操作

```sql
-- 完整备份
EXEC sp_BackupXTHRDatabase @BackupType = 'FULL';

-- 差异备份
EXEC sp_BackupXTHRDatabase @BackupType = 'DIFF';

-- 清理旧备份
EXEC sp_CleanupOldBackups @RetentionDays = 30;
```

### 性能优化

```sql
-- 重建索引
EXEC sp_RebuildIndexes @FragmentationThreshold = 30;

-- 更新统计信息
EXEC sp_UpdateStatistics;

-- 性能监控
EXEC sp_PerformanceMonitor;
```

### 数据清理

```sql
-- 预览要清理的数据
EXEC sp_CleanupHistoryData @RetentionMonths = 24, @DryRun = 1;

-- 执行数据清理
EXEC sp_CleanupHistoryData @RetentionMonths = 24, @DryRun = 0;
```

## 监控建议

### 定期检查项目

1. **空间使用情况**
   - 数据文件增长趋势
   - 日志文件大小
   - 可用磁盘空间

2. **性能指标**
   - 查询执行时间
   - 索引碎片率
   - 等待统计

3. **备份状态**
   - 备份成功率
   - 备份文件完整性
   - 恢复测试

### 告警阈值建议

| 指标 | 警告阈值 | 严重阈值 |
|------|----------|----------|
| 数据文件使用率 | 80% | 90% |
| 日志文件使用率 | 70% | 85% |
| 索引碎片率 | 30% | 50% |
| 查询平均响应时间 | 5秒 | 10秒 |

## 故障排除

### 常见问题

**1. 部署脚本执行失败**
- 检查 SQL Server 版本兼容性
- 确认有足够的权限
- 检查磁盘空间

**2. 连接失败**
- 验证用户名密码
- 检查网络连接
- 确认 SQL Server 服务状态

**3. 性能问题**
- 检查索引碎片率
- 更新统计信息
- 分析慢查询

**4. 空间不足**
- 清理历史数据
- 压缩数据库
- 扩展磁盘空间

### 日志查看

```sql
-- 查看错误日志
EXEC xp_readerrorlog;

-- 查看作业历史
SELECT * FROM msdb.dbo.sysjobhistory 
WHERE job_id IN (
    SELECT job_id FROM msdb.dbo.sysjobs 
    WHERE name LIKE '%XTHR%'
)
ORDER BY run_date DESC, run_time DESC;
```

## 安全建议

### 密码策略
- 定期更改数据库用户密码
- 使用强密码策略
- 限制登录失败次数

### 权限管理
- 遵循最小权限原则
- 定期审查用户权限
- 禁用不必要的功能

### 数据保护
- 启用透明数据加密（TDE）
- 加密敏感字段
- 定期备份验证

## 升级指南

### 版本升级步骤

1. **备份现有数据库**
   ```sql
   EXEC sp_BackupXTHRDatabase @BackupType = 'FULL';
   ```

2. **测试环境验证**
   - 在测试环境执行升级脚本
   - 验证数据完整性
   - 测试应用程序功能

3. **生产环境升级**
   - 选择维护窗口
   - 执行升级脚本
   - 验证升级结果

4. **回滚计划**
   - 准备回滚脚本
   - 测试回滚流程
   - 制定应急预案

## 联系支持

如果在部署或使用过程中遇到问题，请：

1. 查看本文档的故障排除部分
2. 检查 SQL Server 错误日志
3. 联系系统管理员或开发团队

---

**注意事项：**
- 在生产环境部署前，请务必在测试环境验证
- 定期备份数据库，确保数据安全
- 监控系统性能，及时进行维护
- 遵循公司的安全策略和规范

**最后更新：** 2024年
**文档版本：** 1.0