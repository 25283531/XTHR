# XTHR人力资源管理系统 - 数据库表结构设计文档

## 1. 设计概述

### 1.1 设计原则
- **规范化设计**：遵循第三范式，减少数据冗余
- **性能优化**：合理设置索引，提高查询效率
- **扩展性**：预留扩展字段，支持业务发展
- **审计追踪**：记录创建和修改信息
- **数据完整性**：设置合适的约束和外键关系

### 1.2 命名规范
- 表名：使用复数形式，如 `Employees`
- 字段名：使用帕斯卡命名法，如 `EmployeeId`
- 主键：统一使用 `表名单数 + Id` 格式
- 外键：使用被引用表的主键名称

### 1.3 数据类型规范
- 主键：`INT IDENTITY(1,1)`
- 字符串：`NVARCHAR(长度)`，支持Unicode
- 金额：`DECIMAL(10,2)` 或 `DECIMAL(12,2)`
- 日期：`DATE`
- 时间戳：`DATETIME2`
- 布尔值：`BIT`

## 2. 核心业务表设计

### 2.1 员工信息表 (Employees)

**设计目的**：存储员工的基本信息和就业状态

**核心字段说明**：
- `EmployeeNumber`：工号，业务主键，具有唯一性
- `EmploymentStatus`：就业状态（在职/离职/试用期等）
- `HireDate`：入职日期，必填字段
- `TerminationDate`：离职日期，可为空
- `BankAccount`/`BankName`：银行信息，用于工资发放

**索引设计**：
- 部门索引：支持按部门查询员工
- 职位索引：支持按职位统计
- 就业状态索引：快速筛选在职员工
- 入职日期索引：支持按入职时间排序

**业务规则**：
- 工号必须唯一
- 身份证号必须唯一（如果提供）
- 离职员工必须有离职日期和原因

### 2.2 工资基础信息表 (SalaryBases)

**设计目的**：存储员工的工资基础信息，支持历史版本管理

**核心字段说明**：
- `BaseSalary`：基本工资，核心字段
- `PositionAllowance`：岗位津贴
- `PerformanceAllowance`：绩效津贴
- `EffectiveDate`/`ExpiryDate`：生效和失效日期，支持版本管理
- `IsActive`：是否有效，便于快速查询当前有效记录

**版本管理机制**：
- 通过生效日期和失效日期实现版本控制
- 同一员工同一时间只能有一条有效记录
- 支持未来生效的工资调整

### 2.3 考勤记录表 (AttendanceRecords)

**设计目的**：记录员工的日常考勤信息

**核心字段说明**：
- `AttendanceDate`：考勤日期，与员工ID组成复合业务主键
- `CheckInTime`/`CheckOutTime`：签到签退时间
- `WorkHours`：实际工作小时数
- `OvertimeHours`：加班小时数
- `LateMinutes`/`EarlyLeaveMinutes`：迟到早退分钟数
- `AbsenceType`/`LeaveType`：缺勤和请假类型
- `ApprovalStatus`：审批状态，支持考勤异常审批流程

**特殊设计**：
- 支持节假日和周末标识
- 支持多种考勤状态和请假类型
- 包含审批流程字段

### 2.4 绩效考核表 (PerformanceScores)

**设计目的**：存储员工绩效考核结果

**核心字段说明**：
- `AssessmentPeriod`：考核期间（如2024Q1）
- 各项得分字段：工作质量、效率、团队合作、创新、出勤
- `TotalScore`：总分
- `Grade`：等级（优秀/良好/合格/不合格）
- `Ranking`：排名
- 评价字段：自我评价、主管评价、改进计划

**评分体系**：
- 采用百分制评分
- 支持多维度评价
- 包含定性和定量评价

### 2.5 工资核算结果表 (PayrollResults)

**设计目的**：存储每月工资核算的最终结果

**核心字段说明**：
- `PayrollPeriod`：工资期间（如2024-01）
- 收入项：基本工资、各类津贴、加班费
- `GrossSalary`：应发工资总额
- 扣除项：社保、个税、其他扣除
- `NetSalary`：实发工资
- 统计字段：出勤天数、加班小时数、请假天数
- 流程字段：审批状态、发放状态

**业务流程支持**：
- 支持工资审批流程
- 支持工资发放状态跟踪
- 记录发放日期

### 2.6 社保参保信息表 (SocialSecurities)

**设计目的**：管理员工社保和公积金缴费信息

**核心字段说明**：
- `PaymentPeriod`：缴费期间
- 缴费基数：各险种的缴费基数
- 个人缴费：各险种个人承担部分
- 公司缴费：各险种公司承担部分
- 汇总字段：个人和公司缴费总额
- `PaymentStatus`：缴费状态

**险种覆盖**：
- 养老保险、医疗保险、失业保险
- 工伤保险、生育保险（公司承担）
- 住房公积金

### 2.7 其他奖惩信息表 (OtherCompensationPenalties)

**设计目的**：管理员工的奖励和惩罚信息

**核心字段说明**：
- `Type`：类型（奖励/惩罚）
- `Category`：分类（绩效奖金/全勤奖/迟到罚款等）
- `Amount`：金额
- `Reason`：原因说明
- `ApplicablePeriod`：适用期间
- `EffectiveDate`/`ExpiryDate`：生效和失效日期
- `IsRecurring`：是否重复发生
- 流程字段：审批状态、处理状态

**灵活性设计**：
- 支持一次性和重复性奖惩
- 支持不同的奖惩分类
- 包含完整的审批和处理流程

### 2.8 工资成本分析表 (PayrollCostAnalyses)

**设计目的**：提供工资成本的统计分析数据

**核心字段说明**：
- `AnalysisPeriod`：分析期间
- `Department`：部门（可为空，表示全公司）
- 成本统计：各项成本的汇总数据
- `TotalCost`：总成本
- `AverageCostPerEmployee`：人均成本
- 预算对比：预算金额、差异、差异百分比

**分析维度**：
- 按期间分析
- 按部门分析
- 预算执行分析

### 2.9 工资规则配置表 (PayrollRules)

**设计目的**：配置工资计算的各种规则

**核心字段说明**：
- `RuleName`：规则名称，必须唯一
- `RuleType`：规则类型（工资计算/税费计算/社保计算等）
- `RuleFormula`：规则公式
- `Parameters`：参数配置（JSON格式）
- `Priority`：优先级，控制规则执行顺序
- 生效期间：支持规则的时效性管理

**规则引擎支持**：
- 支持公式化配置
- 支持参数化配置
- 支持优先级控制
- 支持版本管理

### 2.10 系统配置表 (SystemConfigs)

**设计目的**：存储系统的各种配置参数

**核心字段说明**：
- `ConfigKey`：配置键，必须唯一
- `ConfigValue`：配置值
- `Category`：配置分类
- `DataType`：数据类型（string/int/decimal/bool等）
- `DefaultValue`：默认值
- `IsRequired`：是否必需
- `IsEncrypted`：是否加密存储
- `SortOrder`：排序顺序

**配置管理特性**：
- 支持分类管理
- 支持数据类型验证
- 支持敏感信息加密
- 支持默认值机制

## 3. 审计表设计

### 3.1 工资规则变更历史表 (PayrollRuleChangeHistory)

**设计目的**：记录工资规则的变更历史

**核心字段**：
- `RuleId`：关联的规则ID
- `ChangeType`：变更类型（创建/修改/删除/启用/禁用）
- `OldValue`/`NewValue`：变更前后的值
- `ChangedBy`：变更人
- `ChangeReason`：变更原因

### 3.2 系统配置变更历史表 (SystemConfigChangeHistory)

**设计目的**：记录系统配置的变更历史

**核心字段**：
- `ConfigKey`：配置键
- `OldValue`/`NewValue`：变更前后的值
- `ChangedBy`：变更人
- `ChangeReason`：变更原因

## 4. 索引策略

### 4.1 主要索引
- **主键索引**：所有表的主键自动创建聚集索引
- **外键索引**：所有外键字段创建非聚集索引
- **业务查询索引**：根据常用查询条件创建复合索引

### 4.2 复合索引建议
- `AttendanceRecords`：(EmployeeId, AttendanceDate)
- `PayrollResults`：(EmployeeId, PayrollPeriod)
- `SocialSecurities`：(EmployeeId, PaymentPeriod)
- `PerformanceScores`：(EmployeeId, AssessmentPeriod)

## 5. 数据完整性约束

### 5.1 外键约束
- 所有关联员工的表都设置外键约束到Employees表
- 审计表设置外键约束到对应的主表

### 5.2 检查约束建议
```sql
-- 员工表约束
ALTER TABLE Employees ADD CONSTRAINT CK_Employees_Gender 
    CHECK (Gender IN ('男', '女', '其他'));

ALTER TABLE Employees ADD CONSTRAINT CK_Employees_EmploymentStatus 
    CHECK (EmploymentStatus IN ('在职', '离职', '试用期', '实习'));

-- 工资表约束
ALTER TABLE SalaryBases ADD CONSTRAINT CK_SalaryBases_BaseSalary 
    CHECK (BaseSalary >= 0);

-- 考勤表约束
ALTER TABLE AttendanceRecords ADD CONSTRAINT CK_AttendanceRecords_WorkHours 
    CHECK (WorkHours >= 0 AND WorkHours <= 24);
```

## 6. 性能优化建议

### 6.1 分区策略
- 对于大数据量的表（如AttendanceRecords、PayrollResults），可考虑按月份分区
- 历史数据可考虑归档策略

### 6.2 查询优化
- 避免在WHERE子句中使用函数
- 合理使用EXISTS代替IN
- 对于统计查询，考虑创建物化视图

### 6.3 维护策略
- 定期更新统计信息
- 定期重建索引
- 监控慢查询并优化

## 7. 扩展性考虑

### 7.1 预留字段
- 各主要业务表预留了Notes字段用于备注
- 可根据业务需要添加自定义字段

### 7.2 多租户支持
- 如需支持多租户，可在各表添加TenantId字段
- 相应调整索引和查询逻辑

### 7.3 国际化支持
- 字符串字段使用NVARCHAR支持Unicode
- 枚举值可考虑使用配置表管理

## 8. 安全性考虑

### 8.1 敏感数据保护
- 身份证号、银行账号等敏感信息考虑加密存储
- 工资信息访问需要严格的权限控制

### 8.2 审计日志
- 重要操作记录审计日志
- 敏感数据访问记录日志

### 8.3 数据备份
- 定期备份数据库
- 重要操作前进行备份
- 测试恢复流程

## 9. 初始化数据

### 9.1 系统配置
- 公司基本信息
- 工资计算参数（个税起征点、标准工作时间等）
- 社保费率配置

### 9.2 工资规则
- 基本工资计算规则
- 加班费计算规则
- 税费计算规则
- 社保计算规则

## 10. 总结

本数据库设计遵循了人力资源管理系统的业务特点，具有以下特色：

1. **完整性**：覆盖了人力资源管理的核心业务流程
2. **灵活性**：支持多种业务场景和配置需求
3. **扩展性**：预留了扩展空间，支持业务发展
4. **性能**：合理的索引设计，支持高效查询
5. **安全性**：考虑了数据安全和审计需求
6. **规范性**：遵循数据库设计最佳实践

该设计为XTHR人力资源管理系统提供了坚实的数据基础，能够支撑系统的稳定运行和持续发展。