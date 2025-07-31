-- ================================================================
-- XTHR人力资源管理系统 - 基础表结构设计
-- 创建时间：2024年
-- 描述：包含所有核心业务实体的表结构定义
-- ================================================================

-- 设置数据库选项
USE XTHR;
GO

-- ================================================================
-- 1. 员工信息表 (Employees)
-- ================================================================
CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeNumber NVARCHAR(20) NOT NULL UNIQUE,           -- 工号
    Name NVARCHAR(50) NOT NULL,                            -- 姓名
    Gender NVARCHAR(10),                                    -- 性别
    DateOfBirth DATE,                                       -- 出生日期
    IdCardNumber NVARCHAR(18) UNIQUE,                      -- 身份证号
    PhoneNumber NVARCHAR(20),                              -- 手机号码
    Email NVARCHAR(100),                                   -- 邮箱
    Address NVARCHAR(200),                                 -- 地址
    Department NVARCHAR(50),                               -- 部门
    Position NVARCHAR(50),                                 -- 职位
    Level NVARCHAR(20),                                    -- 职级
    HireDate DATE NOT NULL,                               -- 入职日期
    EmploymentStatus NVARCHAR(20) DEFAULT '在职',          -- 就业状态
    TerminationDate DATE,                                  -- 离职日期
    TerminationReason NVARCHAR(200),                      -- 离职原因
    BankAccount NVARCHAR(30),                             -- 银行账号
    BankName NVARCHAR(100),                               -- 开户银行
    EmergencyContact NVARCHAR(50),                        -- 紧急联系人
    EmergencyPhone NVARCHAR(20),                          -- 紧急联系电话
    Notes NVARCHAR(500),                                  -- 备注
    CreatedBy NVARCHAR(50),                               -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),                -- 创建时间
    UpdatedBy NVARCHAR(50),                               -- 更新人
    UpdatedAt DATETIME2                                    -- 更新时间
);

-- 创建索引
CREATE INDEX IX_Employees_Department ON Employees(Department);
CREATE INDEX IX_Employees_Position ON Employees(Position);
CREATE INDEX IX_Employees_EmploymentStatus ON Employees(EmploymentStatus);
CREATE INDEX IX_Employees_HireDate ON Employees(HireDate);

-- ================================================================
-- 2. 工资基础信息表 (SalaryBases)
-- ================================================================
CREATE TABLE SalaryBases (
    SalaryBaseId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,                              -- 员工ID
    BaseSalary DECIMAL(10,2) NOT NULL,                    -- 基本工资
    PositionAllowance DECIMAL(10,2) DEFAULT 0,           -- 岗位津贴
    PerformanceAllowance DECIMAL(10,2) DEFAULT 0,        -- 绩效津贴
    OtherAllowances DECIMAL(10,2) DEFAULT 0,             -- 其他津贴
    EffectiveDate DATE NOT NULL,                          -- 生效日期
    ExpiryDate DATE,                                      -- 失效日期
    IsActive BIT DEFAULT 1,                               -- 是否有效
    Notes NVARCHAR(500),                                  -- 备注
    CreatedBy NVARCHAR(50),                               -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),                -- 创建时间
    UpdatedBy NVARCHAR(50),                               -- 更新人
    UpdatedAt DATETIME2,                                   -- 更新时间
    
    CONSTRAINT FK_SalaryBases_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

-- 创建索引
CREATE INDEX IX_SalaryBases_EmployeeId ON SalaryBases(EmployeeId);
CREATE INDEX IX_SalaryBases_EffectiveDate ON SalaryBases(EffectiveDate);
CREATE INDEX IX_SalaryBases_IsActive ON SalaryBases(IsActive);

-- ================================================================
-- 3. 考勤记录表 (AttendanceRecords)
-- ================================================================
CREATE TABLE AttendanceRecords (
    AttendanceId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,                              -- 员工ID
    AttendanceDate DATE NOT NULL,                         -- 考勤日期
    CheckInTime TIME,                                     -- 签到时间
    CheckOutTime TIME,                                    -- 签退时间
    WorkHours DECIMAL(4,2),                              -- 工作小时数
    OvertimeHours DECIMAL(4,2) DEFAULT 0,               -- 加班小时数
    LateMinutes INT DEFAULT 0,                           -- 迟到分钟数
    EarlyLeaveMinutes INT DEFAULT 0,                     -- 早退分钟数
    AbsenceType NVARCHAR(20),                            -- 缺勤类型
    LeaveType NVARCHAR(20),                              -- 请假类型
    IsHoliday BIT DEFAULT 0,                             -- 是否节假日
    IsWeekend BIT DEFAULT 0,                             -- 是否周末
    Status NVARCHAR(20) DEFAULT '正常',                  -- 状态
    ApprovalStatus NVARCHAR(20) DEFAULT '待审批',        -- 审批状态
    ApprovedBy NVARCHAR(50),                             -- 审批人
    ApprovedAt DATETIME2,                                -- 审批时间
    Notes NVARCHAR(500),                                 -- 备注
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2,                                 -- 更新时间
    
    CONSTRAINT FK_AttendanceRecords_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

-- 创建索引
CREATE INDEX IX_AttendanceRecords_EmployeeId ON AttendanceRecords(EmployeeId);
CREATE INDEX IX_AttendanceRecords_AttendanceDate ON AttendanceRecords(AttendanceDate);
CREATE INDEX IX_AttendanceRecords_Status ON AttendanceRecords(Status);
CREATE INDEX IX_AttendanceRecords_ApprovalStatus ON AttendanceRecords(ApprovalStatus);

-- ================================================================
-- 4. 绩效考核表 (PerformanceScores)
-- ================================================================
CREATE TABLE PerformanceScores (
    PerformanceId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,                              -- 员工ID
    AssessmentPeriod NVARCHAR(20) NOT NULL,              -- 考核期间
    WorkQualityScore DECIMAL(5,2),                       -- 工作质量得分
    WorkEfficiencyScore DECIMAL(5,2),                    -- 工作效率得分
    TeamworkScore DECIMAL(5,2),                          -- 团队合作得分
    InnovationScore DECIMAL(5,2),                        -- 创新能力得分
    AttendanceScore DECIMAL(5,2),                        -- 出勤表现得分
    TotalScore DECIMAL(5,2),                             -- 总分
    Grade NVARCHAR(10),                                  -- 等级
    Ranking INT,                                         -- 排名
    SelfAssessment NVARCHAR(1000),                      -- 自我评价
    SupervisorAssessment NVARCHAR(1000),                -- 主管评价
    ImprovementPlan NVARCHAR(1000),                     -- 改进计划
    ApprovalStatus NVARCHAR(20) DEFAULT '待审批',        -- 审批状态
    ApprovedBy NVARCHAR(50),                             -- 审批人
    ApprovedAt DATETIME2,                                -- 审批时间
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2,                                 -- 更新时间
    
    CONSTRAINT FK_PerformanceScores_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

-- 创建索引
CREATE INDEX IX_PerformanceScores_EmployeeId ON PerformanceScores(EmployeeId);
CREATE INDEX IX_PerformanceScores_AssessmentPeriod ON PerformanceScores(AssessmentPeriod);
CREATE INDEX IX_PerformanceScores_Grade ON PerformanceScores(Grade);
CREATE INDEX IX_PerformanceScores_ApprovalStatus ON PerformanceScores(ApprovalStatus);

-- ================================================================
-- 5. 工资核算结果表 (PayrollResults)
-- ================================================================
CREATE TABLE PayrollResults (
    PayrollId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,                              -- 员工ID
    PayrollPeriod NVARCHAR(20) NOT NULL,                 -- 工资期间
    BaseSalary DECIMAL(10,2),                            -- 基本工资
    PositionAllowance DECIMAL(10,2) DEFAULT 0,           -- 岗位津贴
    PerformanceAllowance DECIMAL(10,2) DEFAULT 0,        -- 绩效津贴
    OvertimeAllowance DECIMAL(10,2) DEFAULT 0,           -- 加班费
    OtherAllowances DECIMAL(10,2) DEFAULT 0,             -- 其他津贴
    GrossSalary DECIMAL(10,2),                           -- 应发工资
    SocialSecurityDeduction DECIMAL(10,2) DEFAULT 0,     -- 社保扣除
    TaxDeduction DECIMAL(10,2) DEFAULT 0,                -- 个税扣除
    OtherDeductions DECIMAL(10,2) DEFAULT 0,             -- 其他扣除
    NetSalary DECIMAL(10,2),                             -- 实发工资
    AttendanceDays INT,                                   -- 出勤天数
    OvertimeHours DECIMAL(5,2) DEFAULT 0,               -- 加班小时数
    LeaveDays DECIMAL(5,2) DEFAULT 0,                   -- 请假天数
    ApprovalStatus NVARCHAR(20) DEFAULT '待审批',        -- 审批状态
    ApprovedBy NVARCHAR(50),                             -- 审批人
    ApprovedAt DATETIME2,                                -- 审批时间
    PaymentStatus NVARCHAR(20) DEFAULT '未发放',         -- 发放状态
    PaymentDate DATE,                                    -- 发放日期
    Notes NVARCHAR(500),                                 -- 备注
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2,                                 -- 更新时间
    
    CONSTRAINT FK_PayrollResults_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

-- 创建索引
CREATE INDEX IX_PayrollResults_EmployeeId ON PayrollResults(EmployeeId);
CREATE INDEX IX_PayrollResults_PayrollPeriod ON PayrollResults(PayrollPeriod);
CREATE INDEX IX_PayrollResults_ApprovalStatus ON PayrollResults(ApprovalStatus);
CREATE INDEX IX_PayrollResults_PaymentStatus ON PayrollResults(PaymentStatus);

-- ================================================================
-- 6. 社保参保信息表 (SocialSecurities)
-- ================================================================
CREATE TABLE SocialSecurities (
    SocialSecurityId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,                              -- 员工ID
    PaymentPeriod NVARCHAR(20) NOT NULL,                 -- 缴费期间
    PensionBase DECIMAL(10,2),                           -- 养老保险基数
    MedicalBase DECIMAL(10,2),                           -- 医疗保险基数
    UnemploymentBase DECIMAL(10,2),                      -- 失业保险基数
    WorkInjuryBase DECIMAL(10,2),                        -- 工伤保险基数
    MaternityBase DECIMAL(10,2),                         -- 生育保险基数
    HousingFundBase DECIMAL(10,2),                       -- 公积金基数
    PensionEmployeeAmount DECIMAL(10,2),                 -- 个人养老保险
    MedicalEmployeeAmount DECIMAL(10,2),                 -- 个人医疗保险
    UnemploymentEmployeeAmount DECIMAL(10,2),            -- 个人失业保险
    HousingFundEmployeeAmount DECIMAL(10,2),             -- 个人公积金
    PensionCompanyAmount DECIMAL(10,2),                  -- 公司养老保险
    MedicalCompanyAmount DECIMAL(10,2),                  -- 公司医疗保险
    UnemploymentCompanyAmount DECIMAL(10,2),             -- 公司失业保险
    WorkInjuryCompanyAmount DECIMAL(10,2),               -- 公司工伤保险
    MaternityCompanyAmount DECIMAL(10,2),                -- 公司生育保险
    HousingFundCompanyAmount DECIMAL(10,2),              -- 公司公积金
    TotalEmployeeAmount DECIMAL(10,2),                   -- 个人缴费总额
    TotalCompanyAmount DECIMAL(10,2),                    -- 公司缴费总额
    PaymentStatus NVARCHAR(20) DEFAULT '未缴费',         -- 缴费状态
    PaymentDate DATE,                                    -- 缴费日期
    Notes NVARCHAR(500),                                 -- 备注
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2,                                 -- 更新时间
    
    CONSTRAINT FK_SocialSecurities_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

-- 创建索引
CREATE INDEX IX_SocialSecurities_EmployeeId ON SocialSecurities(EmployeeId);
CREATE INDEX IX_SocialSecurities_PaymentPeriod ON SocialSecurities(PaymentPeriod);
CREATE INDEX IX_SocialSecurities_PaymentStatus ON SocialSecurities(PaymentStatus);

-- ================================================================
-- 7. 其他奖惩信息表 (OtherCompensationPenalties)
-- ================================================================
CREATE TABLE OtherCompensationPenalties (
    CompensationPenaltyId INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,                              -- 员工ID
    Type NVARCHAR(20) NOT NULL,                          -- 类型（奖励/惩罚）
    Category NVARCHAR(50),                               -- 分类
    Amount DECIMAL(10,2) NOT NULL,                       -- 金额
    Reason NVARCHAR(500),                                -- 原因
    ApplicablePeriod NVARCHAR(20),                       -- 适用期间
    EffectiveDate DATE,                                  -- 生效日期
    ExpiryDate DATE,                                     -- 失效日期
    IsRecurring BIT DEFAULT 0,                           -- 是否重复
    ApprovalStatus NVARCHAR(20) DEFAULT '待审批',        -- 审批状态
    ApprovedBy NVARCHAR(50),                             -- 审批人
    ApprovedAt DATETIME2,                                -- 审批时间
    ProcessedStatus NVARCHAR(20) DEFAULT '未处理',       -- 处理状态
    ProcessedDate DATE,                                  -- 处理日期
    Notes NVARCHAR(500),                                 -- 备注
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2,                                 -- 更新时间
    
    CONSTRAINT FK_OtherCompensationPenalties_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

-- 创建索引
CREATE INDEX IX_OtherCompensationPenalties_EmployeeId ON OtherCompensationPenalties(EmployeeId);
CREATE INDEX IX_OtherCompensationPenalties_Type ON OtherCompensationPenalties(Type);
CREATE INDEX IX_OtherCompensationPenalties_ApprovalStatus ON OtherCompensationPenalties(ApprovalStatus);
CREATE INDEX IX_OtherCompensationPenalties_ApplicablePeriod ON OtherCompensationPenalties(ApplicablePeriod);

-- ================================================================
-- 8. 工资成本分析表 (PayrollCostAnalyses)
-- ================================================================
CREATE TABLE PayrollCostAnalyses (
    CostAnalysisId INT IDENTITY(1,1) PRIMARY KEY,
    AnalysisPeriod NVARCHAR(20) NOT NULL,                -- 分析期间
    Department NVARCHAR(50),                             -- 部门
    TotalEmployees INT,                                  -- 员工总数
    TotalBaseSalary DECIMAL(12,2),                       -- 基本工资总额
    TotalAllowances DECIMAL(12,2),                       -- 津贴总额
    TotalDeductions DECIMAL(12,2),                       -- 扣除总额
    TotalNetSalary DECIMAL(12,2),                        -- 实发工资总额
    TotalSocialSecurity DECIMAL(12,2),                   -- 社保总额
    TotalOtherCosts DECIMAL(12,2),                       -- 其他成本总额
    TotalCost DECIMAL(12,2),                             -- 总成本
    AverageCostPerEmployee DECIMAL(10,2),                -- 人均成本
    BudgetAmount DECIMAL(12,2),                          -- 预算金额
    BudgetVariance DECIMAL(12,2),                        -- 预算差异
    BudgetVariancePercentage DECIMAL(5,2),               -- 预算差异百分比
    Notes NVARCHAR(500),                                 -- 备注
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2                                  -- 更新时间
);

-- 创建索引
CREATE INDEX IX_PayrollCostAnalyses_AnalysisPeriod ON PayrollCostAnalyses(AnalysisPeriod);
CREATE INDEX IX_PayrollCostAnalyses_Department ON PayrollCostAnalyses(Department);

-- ================================================================
-- 9. 工资规则配置表 (PayrollRules)
-- ================================================================
CREATE TABLE PayrollRules (
    RuleId INT IDENTITY(1,1) PRIMARY KEY,
    RuleName NVARCHAR(100) NOT NULL UNIQUE,              -- 规则名称
    RuleType NVARCHAR(50) NOT NULL,                      -- 规则类型
    RuleDescription NVARCHAR(500),                       -- 规则描述
    RuleFormula NVARCHAR(1000),                          -- 规则公式
    Parameters NVARCHAR(2000),                           -- 参数配置
    Priority INT DEFAULT 0,                             -- 优先级
    IsActive BIT DEFAULT 1,                              -- 是否有效
    EffectiveDate DATE,                                  -- 生效日期
    ExpiryDate DATE,                                     -- 失效日期
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2                                  -- 更新时间
);

-- 创建索引
CREATE INDEX IX_PayrollRules_RuleType ON PayrollRules(RuleType);
CREATE INDEX IX_PayrollRules_IsActive ON PayrollRules(IsActive);
CREATE INDEX IX_PayrollRules_Priority ON PayrollRules(Priority);

-- ================================================================
-- 10. 系统配置表 (SystemConfigs)
-- ================================================================
CREATE TABLE SystemConfigs (
    ConfigId INT IDENTITY(1,1) PRIMARY KEY,
    ConfigKey NVARCHAR(100) NOT NULL UNIQUE,             -- 配置键
    ConfigValue NVARCHAR(2000),                          -- 配置值
    ConfigDescription NVARCHAR(500),                     -- 配置描述
    Category NVARCHAR(50),                               -- 配置分类
    DataType NVARCHAR(20) DEFAULT 'string',              -- 数据类型
    DefaultValue NVARCHAR(2000),                         -- 默认值
    IsRequired BIT DEFAULT 0,                            -- 是否必需
    IsEncrypted BIT DEFAULT 0,                           -- 是否加密
    IsActive BIT DEFAULT 1,                              -- 是否有效
    SortOrder INT DEFAULT 0,                             -- 排序
    CreatedBy NVARCHAR(50),                              -- 创建人
    CreatedAt DATETIME2 DEFAULT GETDATE(),               -- 创建时间
    UpdatedBy NVARCHAR(50),                              -- 更新人
    UpdatedAt DATETIME2                                  -- 更新时间
);

-- 创建索引
CREATE INDEX IX_SystemConfigs_Category ON SystemConfigs(Category);
CREATE INDEX IX_SystemConfigs_IsActive ON SystemConfigs(IsActive);
CREATE INDEX IX_SystemConfigs_SortOrder ON SystemConfigs(SortOrder);

-- ================================================================
-- 审计表（可选）
-- ================================================================

-- 工资规则变更历史表
CREATE TABLE PayrollRuleChangeHistory (
    ChangeId INT IDENTITY(1,1) PRIMARY KEY,
    RuleId INT NOT NULL,
    ChangeType NVARCHAR(20) NOT NULL,                    -- 变更类型
    OldValue NVARCHAR(MAX),                              -- 旧值
    NewValue NVARCHAR(MAX),                              -- 新值
    ChangedBy NVARCHAR(50),                              -- 变更人
    ChangedAt DATETIME2 DEFAULT GETDATE(),               -- 变更时间
    ChangeReason NVARCHAR(500),                          -- 变更原因
    
    CONSTRAINT FK_PayrollRuleChangeHistory_PayrollRules FOREIGN KEY (RuleId) REFERENCES PayrollRules(RuleId)
);

-- 系统配置变更历史表
CREATE TABLE SystemConfigChangeHistory (
    ChangeId INT IDENTITY(1,1) PRIMARY KEY,
    ConfigKey NVARCHAR(100) NOT NULL,
    OldValue NVARCHAR(2000),                             -- 旧值
    NewValue NVARCHAR(2000),                             -- 新值
    ChangedBy NVARCHAR(50),                              -- 变更人
    ChangedAt DATETIME2 DEFAULT GETDATE(),               -- 变更时间
    ChangeReason NVARCHAR(500)                           -- 变更原因
);

-- ================================================================
-- 初始化基础数据
-- ================================================================

-- 插入系统配置基础数据
INSERT INTO SystemConfigs (ConfigKey, ConfigValue, ConfigDescription, Category, DataType, DefaultValue, IsRequired, CreatedBy) VALUES
('Company.Name', 'XTHR人力资源管理系统', '公司名称', '基础设置', 'string', 'XTHR人力资源管理系统', 1, 'System'),
('Payroll.TaxThreshold', '5000', '个税起征点', '工资设置', 'decimal', '5000', 1, 'System'),
('Attendance.WorkHoursPerDay', '8', '每日标准工作小时', '考勤设置', 'decimal', '8', 1, 'System'),
('Attendance.WorkDaysPerMonth', '22', '每月标准工作天数', '考勤设置', 'int', '22', 1, 'System'),
('SocialSecurity.PensionEmployeeRate', '0.08', '个人养老保险费率', '社保设置', 'decimal', '0.08', 1, 'System'),
('SocialSecurity.MedicalEmployeeRate', '0.02', '个人医疗保险费率', '社保设置', 'decimal', '0.02', 1, 'System'),
('SocialSecurity.UnemploymentEmployeeRate', '0.005', '个人失业保险费率', '社保设置', 'decimal', '0.005', 1, 'System'),
('SocialSecurity.HousingFundEmployeeRate', '0.12', '个人公积金费率', '社保设置', 'decimal', '0.12', 1, 'System'),
('SocialSecurity.PensionCompanyRate', '0.16', '公司养老保险费率', '社保设置', 'decimal', '0.16', 1, 'System'),
('SocialSecurity.MedicalCompanyRate', '0.10', '公司医疗保险费率', '社保设置', 'decimal', '0.10', 1, 'System'),
('SocialSecurity.UnemploymentCompanyRate', '0.005', '公司失业保险费率', '社保设置', 'decimal', '0.005', 1, 'System'),
('SocialSecurity.WorkInjuryCompanyRate', '0.005', '公司工伤保险费率', '社保设置', 'decimal', '0.005', 1, 'System'),
('SocialSecurity.MaternityCompanyRate', '0.008', '公司生育保险费率', '社保设置', 'decimal', '0.008', 1, 'System'),
('SocialSecurity.HousingFundCompanyRate', '0.12', '公司公积金费率', '社保设置', 'decimal', '0.12', 1, 'System');

-- 插入工资规则基础数据
INSERT INTO PayrollRules (RuleName, RuleType, RuleDescription, RuleFormula, Priority, CreatedBy) VALUES
('基本工资计算', '工资计算', '根据出勤天数计算基本工资', 'BaseSalary * (AttendanceDays / StandardWorkDays)', 1, 'System'),
('加班费计算', '工资计算', '根据加班小时数计算加班费', 'BaseSalary / (StandardWorkDays * StandardWorkHours) * OvertimeHours * OvertimeRate', 2, 'System'),
('个税计算', '税费计算', '根据应税收入计算个人所得税', 'CalculateTax(TaxableIncome, TaxThreshold)', 3, 'System'),
('社保个人部分计算', '社保计算', '计算社保个人缴费部分', 'SocialSecurityBase * EmployeeRate', 4, 'System'),
('社保公司部分计算', '社保计算', '计算社保公司缴费部分', 'SocialSecurityBase * CompanyRate', 5, 'System');

PRINT '基础表结构创建完成！';
GO