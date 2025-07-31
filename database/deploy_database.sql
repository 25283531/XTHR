-- ================================================================
-- XTHR人力资源管理系统 - 数据库部署脚本
-- 创建时间：2024年
-- 描述：完整的数据库部署流程，包含环境检查、数据库创建、表结构、初始数据等
-- ================================================================

-- 设置执行选项
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

PRINT '==========================================';
PRINT 'XTHR人力资源管理系统 - 数据库部署开始';
PRINT '部署时间：' + CAST(GETDATE() AS NVARCHAR(20));
PRINT '==========================================';

-- ================================================================
-- 1. 环境检查
-- ================================================================
PRINT '正在进行环境检查...';

-- 检查SQL Server版本
DECLARE @Version NVARCHAR(100) = @@VERSION;
DECLARE @MajorVersion INT = CAST(SERVERPROPERTY('ProductMajorVersion') AS INT);

PRINT 'SQL Server版本：' + @Version;

IF @MajorVersion < 13 -- SQL Server 2016
BEGIN
    PRINT 'WARNING: 建议使用SQL Server 2016或更高版本';
END
ELSE
    PRINT 'SQL Server版本检查通过';

-- 检查磁盘空间（简化检查）
PRINT '磁盘空间检查：请确保有足够的磁盘空间用于数据库文件';

-- ================================================================
-- 2. 数据库创建
-- ================================================================
PRINT '正在创建数据库...';

-- 检查数据库是否存在
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'XTHR')
BEGIN
    PRINT 'WARNING: 数据库 XTHR 已存在';
    PRINT '如需重新创建，请手动删除现有数据库后重新运行此脚本';
    -- 可以选择退出或继续
    -- RETURN;
END
ELSE
BEGIN
    -- 创建数据库目录（如果不存在）
    DECLARE @DataPath NVARCHAR(500) = 'C:\Database\XTHR\';
    DECLARE @CreateDirCmd NVARCHAR(1000) = 'IF NOT EXIST "' + @DataPath + '" MKDIR "' + @DataPath + '"';
    
    -- 创建数据库
    CREATE DATABASE XTHR
    ON 
    ( 
        NAME = 'XTHR_Data',
        FILENAME = 'C:\Database\XTHR\XTHR_Data.mdf',
        SIZE = 100MB,
        MAXSIZE = 10GB,
        FILEGROWTH = 10MB
    )
    LOG ON 
    (
        NAME = 'XTHR_Log',
        FILENAME = 'C:\Database\XTHR\XTHR_Log.ldf',
        SIZE = 10MB,
        MAXSIZE = 1GB,
        FILEGROWTH = 10%
    );
    
    PRINT '数据库 XTHR 创建成功';
END
GO

-- 使用新创建的数据库
USE XTHR;
GO

-- ================================================================
-- 3. 数据库配置
-- ================================================================
PRINT '正在配置数据库选项...';

-- 设置数据库选项
ALTER DATABASE XTHR SET RECOVERY SIMPLE;
ALTER DATABASE XTHR SET AUTO_SHRINK OFF;
ALTER DATABASE XTHR SET AUTO_CREATE_STATISTICS ON;
ALTER DATABASE XTHR SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE XTHR SET PAGE_VERIFY CHECKSUM;
ALTER DATABASE XTHR SET COMPATIBILITY_LEVEL = 150;

PRINT '数据库选项配置完成';

-- ================================================================
-- 4. 创建架构
-- ================================================================
PRINT '正在创建数据库架构...';

-- 创建架构
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'HR')
    EXEC('CREATE SCHEMA HR');

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Payroll')
    EXEC('CREATE SCHEMA Payroll');

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'System')
    EXEC('CREATE SCHEMA System');

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Audit')
    EXEC('CREATE SCHEMA Audit');

PRINT '数据库架构创建完成';

-- ================================================================
-- 5. 创建用户和权限
-- ================================================================
PRINT '正在创建用户和设置权限...';

-- 创建登录用户（如果不存在）
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'XTHR_AppUser')
BEGIN
    CREATE LOGIN XTHR_AppUser WITH PASSWORD = 'XTHR@2024!App', 
        DEFAULT_DATABASE = XTHR,
        CHECK_EXPIRATION = OFF,
        CHECK_POLICY = OFF;
    PRINT '应用程序登录用户创建成功';
END

IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'XTHR_ReadOnlyUser')
BEGIN
    CREATE LOGIN XTHR_ReadOnlyUser WITH PASSWORD = 'XTHR@2024!Read', 
        DEFAULT_DATABASE = XTHR,
        CHECK_EXPIRATION = OFF,
        CHECK_POLICY = OFF;
    PRINT '只读登录用户创建成功';
END

-- 在数据库中创建用户
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'XTHR_AppUser')
    CREATE USER XTHR_AppUser FOR LOGIN XTHR_AppUser;

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'XTHR_ReadOnlyUser')
    CREATE USER XTHR_ReadOnlyUser FOR LOGIN XTHR_ReadOnlyUser;

-- 创建角色
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'XTHR_DataWriter')
    CREATE ROLE XTHR_DataWriter;

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'XTHR_DataReader')
    CREATE ROLE XTHR_DataReader;

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'XTHR_Executor')
    CREATE ROLE XTHR_Executor;

-- 分配权限
GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::dbo TO XTHR_DataWriter;
GRANT SELECT ON SCHEMA::dbo TO XTHR_DataReader;
GRANT EXECUTE ON SCHEMA::dbo TO XTHR_Executor;

-- 将用户添加到角色
ALTER ROLE XTHR_DataWriter ADD MEMBER XTHR_AppUser;
ALTER ROLE XTHR_DataReader ADD MEMBER XTHR_AppUser;
ALTER ROLE XTHR_Executor ADD MEMBER XTHR_AppUser;
ALTER ROLE XTHR_DataReader ADD MEMBER XTHR_ReadOnlyUser;

PRINT '用户和权限设置完成';

-- ================================================================
-- 6. 创建表结构
-- ================================================================
PRINT '正在创建表结构...';

-- 员工信息表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
BEGIN
    CREATE TABLE Employees (
        EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeNumber NVARCHAR(20) NOT NULL UNIQUE,
        Name NVARCHAR(50) NOT NULL,
        Gender NVARCHAR(10),
        DateOfBirth DATE,
        IdCardNumber NVARCHAR(18) UNIQUE,
        PhoneNumber NVARCHAR(20),
        Email NVARCHAR(100),
        Address NVARCHAR(200),
        Department NVARCHAR(50),
        Position NVARCHAR(50),
        Level NVARCHAR(20),
        HireDate DATE NOT NULL,
        EmploymentStatus NVARCHAR(20) DEFAULT '在职',
        TerminationDate DATE,
        TerminationReason NVARCHAR(200),
        BankAccount NVARCHAR(30),
        BankName NVARCHAR(100),
        EmergencyContact NVARCHAR(50),
        EmergencyPhone NVARCHAR(20),
        Notes NVARCHAR(500),
        CreatedBy NVARCHAR(50),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedBy NVARCHAR(50),
        UpdatedAt DATETIME2
    );
    
    -- 创建索引
    CREATE INDEX IX_Employees_Department ON Employees(Department);
    CREATE INDEX IX_Employees_Position ON Employees(Position);
    CREATE INDEX IX_Employees_EmploymentStatus ON Employees(EmploymentStatus);
    CREATE INDEX IX_Employees_HireDate ON Employees(HireDate);
    
    PRINT '员工信息表创建完成';
END

-- 工资基础信息表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalaryBases]') AND type in (N'U'))
BEGIN
    CREATE TABLE SalaryBases (
        SalaryBaseId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        BaseSalary DECIMAL(10,2) NOT NULL,
        PositionAllowance DECIMAL(10,2) DEFAULT 0,
        PerformanceAllowance DECIMAL(10,2) DEFAULT 0,
        OtherAllowances DECIMAL(10,2) DEFAULT 0,
        EffectiveDate DATE NOT NULL,
        ExpiryDate DATE,
        IsActive BIT DEFAULT 1,
        Notes NVARCHAR(500),
        CreatedBy NVARCHAR(50),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedBy NVARCHAR(50),
        UpdatedAt DATETIME2,
        
        CONSTRAINT FK_SalaryBases_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
    );
    
    CREATE INDEX IX_SalaryBases_EmployeeId ON SalaryBases(EmployeeId);
    CREATE INDEX IX_SalaryBases_EffectiveDate ON SalaryBases(EffectiveDate);
    CREATE INDEX IX_SalaryBases_IsActive ON SalaryBases(IsActive);
    
    PRINT '工资基础信息表创建完成';
END

-- 考勤记录表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AttendanceRecords]') AND type in (N'U'))
BEGIN
    CREATE TABLE AttendanceRecords (
        AttendanceId INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        AttendanceDate DATE NOT NULL,
        CheckInTime TIME,
        CheckOutTime TIME,
        WorkHours DECIMAL(4,2),
        OvertimeHours DECIMAL(4,2) DEFAULT 0,
        LateMinutes INT DEFAULT 0,
        EarlyLeaveMinutes INT DEFAULT 0,
        AbsenceType NVARCHAR(20),
        LeaveType NVARCHAR(20),
        IsHoliday BIT DEFAULT 0,
        IsWeekend BIT DEFAULT 0,
        Status NVARCHAR(20) DEFAULT '正常',
        ApprovalStatus NVARCHAR(20) DEFAULT '待审批',
        ApprovedBy NVARCHAR(50),
        ApprovedAt DATETIME2,
        Notes NVARCHAR(500),
        CreatedBy NVARCHAR(50),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedBy NVARCHAR(50),
        UpdatedAt DATETIME2,
        
        CONSTRAINT FK_AttendanceRecords_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
    );
    
    CREATE INDEX IX_AttendanceRecords_EmployeeId ON AttendanceRecords(EmployeeId);
    CREATE INDEX IX_AttendanceRecords_AttendanceDate ON AttendanceRecords(AttendanceDate);
    CREATE INDEX IX_AttendanceRecords_Status ON AttendanceRecords(Status);
    CREATE INDEX IX_AttendanceRecords_ApprovalStatus ON AttendanceRecords(ApprovalStatus);
    
    PRINT '考勤记录表创建完成';
END

-- 系统配置表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemConfigs]') AND type in (N'U'))
BEGIN
    CREATE TABLE SystemConfigs (
        ConfigId INT IDENTITY(1,1) PRIMARY KEY,
        ConfigKey NVARCHAR(100) NOT NULL UNIQUE,
        ConfigValue NVARCHAR(2000),
        ConfigDescription NVARCHAR(500),
        Category NVARCHAR(50),
        DataType NVARCHAR(20) DEFAULT 'string',
        DefaultValue NVARCHAR(2000),
        IsRequired BIT DEFAULT 0,
        IsEncrypted BIT DEFAULT 0,
        IsActive BIT DEFAULT 1,
        SortOrder INT DEFAULT 0,
        CreatedBy NVARCHAR(50),
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedBy NVARCHAR(50),
        UpdatedAt DATETIME2
    );
    
    CREATE INDEX IX_SystemConfigs_Category ON SystemConfigs(Category);
    CREATE INDEX IX_SystemConfigs_IsActive ON SystemConfigs(IsActive);
    CREATE INDEX IX_SystemConfigs_SortOrder ON SystemConfigs(SortOrder);
    
    PRINT '系统配置表创建完成';
END

PRINT '核心表结构创建完成';

-- ================================================================
-- 7. 插入初始数据
-- ================================================================
PRINT '正在插入初始数据...';

-- 插入系统配置
IF NOT EXISTS (SELECT 1 FROM SystemConfigs WHERE ConfigKey = 'Company.Name')
BEGIN
    INSERT INTO SystemConfigs (ConfigKey, ConfigValue, ConfigDescription, Category, DataType, DefaultValue, IsRequired, CreatedBy) VALUES
    ('Company.Name', 'XTHR人力资源管理系统', '公司名称', '基础设置', 'string', 'XTHR人力资源管理系统', 1, 'System'),
    ('Payroll.TaxThreshold', '5000', '个税起征点', '工资设置', 'decimal', '5000', 1, 'System'),
    ('Attendance.WorkHoursPerDay', '8', '每日标准工作小时', '考勤设置', 'decimal', '8', 1, 'System'),
    ('Attendance.WorkDaysPerMonth', '22', '每月标准工作天数', '考勤设置', 'int', '22', 1, 'System'),
    ('SocialSecurity.PensionEmployeeRate', '0.08', '个人养老保险费率', '社保设置', 'decimal', '0.08', 1, 'System'),
    ('SocialSecurity.MedicalEmployeeRate', '0.02', '个人医疗保险费率', '社保设置', 'decimal', '0.02', 1, 'System'),
    ('SocialSecurity.UnemploymentEmployeeRate', '0.005', '个人失业保险费率', '社保设置', 'decimal', '0.005', 1, 'System'),
    ('SocialSecurity.HousingFundEmployeeRate', '0.12', '个人公积金费率', '社保设置', 'decimal', '0.12', 1, 'System');
    
    PRINT '系统配置数据插入完成';
END

-- 插入示例员工数据
IF NOT EXISTS (SELECT 1 FROM Employees WHERE EmployeeNumber = 'EMP001')
BEGIN
    INSERT INTO Employees (EmployeeNumber, Name, Gender, Department, Position, Level, HireDate, PhoneNumber, Email, CreatedBy) VALUES
    ('EMP001', '张三', '男', '人力资源部', '人事专员', '初级', '2024-01-01', '13800138001', 'zhangsan@xthr.com', 'System'),
    ('EMP002', '李四', '女', '财务部', '会计', '中级', '2024-01-15', '13800138002', 'lisi@xthr.com', 'System'),
    ('EMP003', '王五', '男', '技术部', '软件工程师', '高级', '2024-02-01', '13800138003', 'wangwu@xthr.com', 'System');
    
    PRINT '示例员工数据插入完成';
END

-- 插入示例工资数据
IF NOT EXISTS (SELECT 1 FROM SalaryBases WHERE EmployeeId = 1)
BEGIN
    INSERT INTO SalaryBases (EmployeeId, BaseSalary, PositionAllowance, PerformanceAllowance, EffectiveDate, CreatedBy) VALUES
    (1, 8000.00, 1000.00, 500.00, '2024-01-01', 'System'),
    (2, 9000.00, 1200.00, 600.00, '2024-01-15', 'System'),
    (3, 15000.00, 2000.00, 1000.00, '2024-02-01', 'System');
    
    PRINT '示例工资数据插入完成';
END

PRINT '初始数据插入完成';

-- ================================================================
-- 8. 创建视图和函数
-- ================================================================
PRINT '正在创建视图和函数...';

-- 创建员工基本信息视图
IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_EmployeeBasicInfo')
BEGIN
    EXEC('CREATE VIEW dbo.vw_EmployeeBasicInfo
    AS
    SELECT 
        EmployeeId,
        EmployeeNumber,
        Name,
        Gender,
        Department,
        Position,
        Level,
        HireDate,
        EmploymentStatus,
        PhoneNumber,
        Email
    FROM Employees
    WHERE EmploymentStatus = ''在职''');
    
    PRINT '员工基本信息视图创建完成';
END

-- 创建当前有效工资视图
IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'vw_CurrentSalary')
BEGIN
    EXEC('CREATE VIEW dbo.vw_CurrentSalary
    AS
    SELECT 
        sb.EmployeeId,
        e.EmployeeNumber,
        e.Name,
        e.Department,
        e.Position,
        sb.BaseSalary,
        sb.PositionAllowance,
        sb.PerformanceAllowance,
        sb.OtherAllowances,
        (sb.BaseSalary + sb.PositionAllowance + sb.PerformanceAllowance + sb.OtherAllowances) AS TotalSalary,
        sb.EffectiveDate
    FROM SalaryBases sb
    INNER JOIN Employees e ON sb.EmployeeId = e.EmployeeId
    WHERE sb.IsActive = 1
        AND e.EmploymentStatus = ''在职''
        AND sb.EffectiveDate <= GETDATE()
        AND (sb.ExpiryDate IS NULL OR sb.ExpiryDate > GETDATE())');
    
    PRINT '当前有效工资视图创建完成';
END

PRINT '视图和函数创建完成';

-- ================================================================
-- 9. 数据验证
-- ================================================================
PRINT '正在进行数据验证...';

-- 验证表是否创建成功
DECLARE @TableCount INT;
SELECT @TableCount = COUNT(*) FROM sys.tables WHERE name IN ('Employees', 'SalaryBases', 'AttendanceRecords', 'SystemConfigs');

IF @TableCount = 4
    PRINT '核心表创建验证通过';
ELSE
    PRINT 'ERROR: 核心表创建不完整，请检查错误信息';

-- 验证数据是否插入成功
DECLARE @EmployeeCount INT, @ConfigCount INT;
SELECT @EmployeeCount = COUNT(*) FROM Employees;
SELECT @ConfigCount = COUNT(*) FROM SystemConfigs;

PRINT '员工数据条数：' + CAST(@EmployeeCount AS NVARCHAR(10));
PRINT '配置数据条数：' + CAST(@ConfigCount AS NVARCHAR(10));

-- ================================================================
-- 10. 部署完成
-- ================================================================
PRINT '==========================================';
PRINT 'XTHR人力资源管理系统 - 数据库部署完成';
PRINT '完成时间：' + CAST(GETDATE() AS NVARCHAR(20));
PRINT '==========================================';
PRINT '';
PRINT '部署信息汇总：';
PRINT '- 数据库名称：XTHR';
PRINT '- 应用程序用户：XTHR_AppUser';
PRINT '- 只读用户：XTHR_ReadOnlyUser';
PRINT '- 核心表数量：' + CAST(@TableCount AS NVARCHAR(10));
PRINT '- 示例员工数量：' + CAST(@EmployeeCount AS NVARCHAR(10));
PRINT '- 系统配置数量：' + CAST(@ConfigCount AS NVARCHAR(10));
PRINT '';
PRINT '后续步骤：';
PRINT '1. 根据需要执行完整的 table_structure.sql 创建所有表';
PRINT '2. 配置应用程序连接字符串';
PRINT '3. 运行应用程序进行功能测试';
PRINT '4. 根据需要调整系统配置参数';
PRINT '==========================================';
GO