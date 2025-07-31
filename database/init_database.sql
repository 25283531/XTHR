-- XTHR工资计算软件数据库初始化脚本
-- 创建日期: 2024年
-- 数据库类型: SQLite

-- 员工信息表
CREATE TABLE IF NOT EXISTS Employees (
    EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeCode TEXT NOT NULL UNIQUE,  -- 员工工号
    Name TEXT NOT NULL,                 -- 姓名
    Department TEXT NOT NULL,           -- 部门
    Position TEXT NOT NULL,             -- 职位
    JobGrade TEXT,                      -- 职级
    HireDate DATE NOT NULL,             -- 入职日期
    IDNumber TEXT UNIQUE,               -- 身份证号
    ContactInfo TEXT,                   -- 联系方式
    IsActive BOOLEAN DEFAULT 1,         -- 是否在职
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 工资基础信息表
CREATE TABLE IF NOT EXISTS SalaryBase (
    SalaryBaseID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeID INTEGER NOT NULL,
    BaseSalary DECIMAL(10,2) NOT NULL,  -- 基础工资
    PerformanceBase DECIMAL(10,2),      -- 绩效工资基数
    NonCompeteComp DECIMAL(10,2),       -- 竞业补偿
    FullAttendanceBonus DECIMAL(10,2),  -- 全勤奖
    EffectiveDate DATE NOT NULL,        -- 生效日期
    EndDate DATE,                       -- 结束日期
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 绩效考核结果表
CREATE TABLE IF NOT EXISTS PerformanceScores (
    PerformanceID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeID INTEGER NOT NULL,
    Score DECIMAL(5,2) NOT NULL,        -- 绩效评分
    Period TEXT NOT NULL,               -- 考核期间 (YYYY-MM)
    Evaluator TEXT,                     -- 评估人
    EvalDate DATE,                      -- 评估日期
    Comments TEXT,                      -- 评估备注
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 社保参保信息表
CREATE TABLE IF NOT EXISTS SocialSecurity (
    SSID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeID INTEGER NOT NULL,
    SocialSecurityBase DECIMAL(10,2),   -- 社保缴费基数
    MedicalBase DECIMAL(10,2),          -- 医保缴费基数
    ProvidentFundBase DECIMAL(10,2),    -- 公积金缴费基数
    SocialSecurityRate DECIMAL(5,4),    -- 社保缴费比例
    MedicalRate DECIMAL(5,4),           -- 医保缴费比例
    ProvidentFundRate DECIMAL(5,4),     -- 公积金缴费比例
    EffectiveDate DATE NOT NULL,        -- 生效日期
    EndDate DATE,                       -- 结束日期
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 其他奖惩信息表
CREATE TABLE IF NOT EXISTS OtherCompensationsPenalties (
    OCPID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeID INTEGER NOT NULL,
    Type TEXT NOT NULL CHECK (Type IN ('奖励', '惩罚')), -- 类型：奖励/惩罚
    Amount DECIMAL(10,2) NOT NULL,      -- 金额
    Reason TEXT NOT NULL,               -- 原因
    ApplyMonth TEXT NOT NULL,           -- 适用月份 (YYYY-MM)
    ApprovalDate DATE,                  -- 审批日期
    Approver TEXT,                      -- 审批人
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 考勤记录表
CREATE TABLE IF NOT EXISTS AttendanceRecords (
    AttendanceID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeID INTEGER NOT NULL,
    AttendanceDate DATE NOT NULL,       -- 考勤日期
    PunchInTime TIME,                   -- 打卡上班时间
    PunchOutTime TIME,                  -- 打卡下班时间
    Status TEXT NOT NULL,               -- 状态：正常/迟到/早退/缺勤/请假/加班
    AbsentHours DECIMAL(4,2) DEFAULT 0, -- 缺勤小时数
    OvertimeHours DECIMAL(4,2) DEFAULT 0, -- 加班小时数
    IsWeekend BOOLEAN DEFAULT 0,        -- 是否周末
    Remarks TEXT,                       -- 备注
    ImportBatch TEXT,                   -- 导入批次号
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

-- 工资核算结果表
CREATE TABLE IF NOT EXISTS PayrollResults (
    PayrollID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmployeeID INTEGER NOT NULL,
    PayrollMonth TEXT NOT NULL,         -- 工资月份 (YYYY-MM)
    BaseSalaryAmount DECIMAL(10,2),     -- 基础工资金额
    PerformanceSalaryAmount DECIMAL(10,2), -- 绩效工资金额
    SaturdayOvertimeComp DECIMAL(10,2), -- 周六加班补偿
    NonCompeteComp DECIMAL(10,2),       -- 竞业补偿
    FullAttendanceBonus DECIMAL(10,2),  -- 全勤奖
    AttendanceDeduction DECIMAL(10,2),  -- 考勤扣款
    SocialSecurityDeduction DECIMAL(10,2), -- 社保扣除
    MedicalDeduction DECIMAL(10,2),     -- 医保扣除
    ProvidentFundDeduction DECIMAL(10,2), -- 公积金扣除
    OtherCompensationAmount DECIMAL(10,2), -- 其他奖励金额
    OtherPenaltyAmount DECIMAL(10,2),   -- 其他惩罚金额
    GrossSalary DECIMAL(10,2),          -- 应发工资
    NetSalary DECIMAL(10,2),            -- 实发工资
    TaxAmount DECIMAL(10,2),            -- 个税金额
    CalculationDate DATETIME DEFAULT CURRENT_TIMESTAMP, -- 计算日期
    CalculatedBy TEXT,                  -- 计算人
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    UNIQUE(EmployeeID, PayrollMonth)
);

-- 工资成本分析表
CREATE TABLE IF NOT EXISTS PayrollCostAnalysis (
    AnalysisID INTEGER PRIMARY KEY AUTOINCREMENT,
    Department TEXT NOT NULL,           -- 部门
    Position TEXT,                      -- 职位
    JobGrade TEXT,                      -- 职级
    AnalysisMonth TEXT NOT NULL,        -- 分析月份 (YYYY-MM)
    EmployeeCount INTEGER,              -- 员工数量
    TotalSalaryCost DECIMAL(12,2),      -- 总工资成本
    AvgSalary DECIMAL(10,2),            -- 平均工资
    TotalBaseSalary DECIMAL(12,2),      -- 总基础工资
    TotalPerformanceSalary DECIMAL(12,2), -- 总绩效工资
    TotalOvertimeComp DECIMAL(12,2),    -- 总加班补偿
    TotalBonuses DECIMAL(12,2),         -- 总奖金
    TotalDeductions DECIMAL(12,2),      -- 总扣除
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 工资规则配置表
CREATE TABLE IF NOT EXISTS PayrollRules (
    RuleID INTEGER PRIMARY KEY AUTOINCREMENT,
    RuleName TEXT NOT NULL UNIQUE,      -- 规则名称
    RuleType TEXT NOT NULL,             -- 规则类型：计算公式/扣除规则/奖励规则
    RuleExpression TEXT NOT NULL,       -- 规则表达式
    Description TEXT,                   -- 规则描述
    IsActive BOOLEAN DEFAULT 1,         -- 是否启用
    EffectiveDate DATE NOT NULL,        -- 生效日期
    EndDate DATE,                       -- 结束日期
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 系统配置表
CREATE TABLE IF NOT EXISTS SystemConfig (
    ConfigID INTEGER PRIMARY KEY AUTOINCREMENT,
    ConfigKey TEXT NOT NULL UNIQUE,     -- 配置键
    ConfigValue TEXT NOT NULL,          -- 配置值
    ConfigType TEXT NOT NULL,           -- 配置类型：STRING/NUMBER/BOOLEAN/JSON
    Description TEXT,                   -- 配置描述
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 创建索引以提高查询性能
CREATE INDEX IF NOT EXISTS idx_employees_code ON Employees(EmployeeCode);
CREATE INDEX IF NOT EXISTS idx_employees_department ON Employees(Department);
CREATE INDEX IF NOT EXISTS idx_salarybase_employee ON SalaryBase(EmployeeID);
CREATE INDEX IF NOT EXISTS idx_performance_employee_period ON PerformanceScores(EmployeeID, Period);
CREATE INDEX IF NOT EXISTS idx_attendance_employee_date ON AttendanceRecords(EmployeeID, AttendanceDate);
CREATE INDEX IF NOT EXISTS idx_payroll_employee_month ON PayrollResults(EmployeeID, PayrollMonth);
CREATE INDEX IF NOT EXISTS idx_analysis_dept_month ON PayrollCostAnalysis(Department, AnalysisMonth);

-- 插入默认系统配置
INSERT OR IGNORE INTO SystemConfig (ConfigKey, ConfigValue, ConfigType, Description) VALUES
('DEFAULT_WORK_HOURS_PER_DAY', '8', 'NUMBER', '每日标准工作小时数'),
('DEFAULT_WORK_DAYS_PER_MONTH', '22', 'NUMBER', '每月标准工作天数'),
('OVERTIME_RATE_WEEKDAY', '1.5', 'NUMBER', '工作日加班倍率'),
('OVERTIME_RATE_WEEKEND', '2.0', 'NUMBER', '周末加班倍率'),
('ATTENDANCE_DEDUCTION_RATE', '0.1', 'NUMBER', '考勤异常扣除绩效工资比例'),
('TAX_THRESHOLD', '5000', 'NUMBER', '个税起征点'),
('DATABASE_VERSION', '1.0.0', 'STRING', '数据库版本');

-- 插入默认工资计算规则
INSERT OR IGNORE INTO PayrollRules (RuleName, RuleType, RuleExpression, Description, EffectiveDate) VALUES
('绩效工资计算', '计算公式', 'PerformanceBase * (Score / 100)', '绩效工资 = 绩效基数 × (绩效评分 / 100)', date('now')),
('考勤异常扣减', '扣除规则', 'PerformanceSalary * AttendanceDeductionRate * AbsentDays', '考勤异常扣减 = 绩效工资 × 扣减比例 × 异常天数', date('now')),
('周六加班补偿', '计算公式', 'BaseSalary / WorkDaysPerMonth * OvertimeDays * 2', '周六加班补偿 = 基础工资 / 月工作天数 × 加班天数 × 2倍', date('now')),
('全勤奖发放', '奖励规则', 'IF(AbsentDays = 0, FullAttendanceBonus, 0)', '全勤奖 = 如果无缺勤则发放全勤奖，否则为0', date('now'));