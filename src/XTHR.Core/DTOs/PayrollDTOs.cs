using System;
using System.Collections.Generic;
using XTHR.Core.DTOs.Employee;
using XTHR.Core.DTOs.Requests;

namespace XTHR.Core.DTOs
{
    public class PayrollCalculationResultDto
    {
        public bool Success { get; set; }
        public decimal Result { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// 工资计算结果DTO
    /// </summary>
    public class PayrollCalculationResult
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 工资期间
        /// </summary>
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }
        
        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PositionSalary { get; set; }
        
        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; }
        
        /// <summary>
        /// 津贴补助
        /// </summary>
        public decimal Allowance { get; set; }
        
        /// <summary>
        /// 加班费
        /// </summary>
        public decimal OvertimePay { get; set; }
        
        /// <summary>
        /// 全勤奖
        /// </summary>
        public decimal AttendanceBonus { get; set; }
        
        /// <summary>
        /// 其他奖励
        /// </summary>
        public decimal OtherBonus { get; set; }
        
        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossPay { get; set; }
        
        /// <summary>
        /// 个人所得税
        /// </summary>
        public decimal IncomeTax { get; set; }
        
        /// <summary>
        /// 社保个人部分
        /// </summary>
        public decimal SocialSecurityPersonal { get; set; }
        
        /// <summary>
        /// 公积金个人部分
        /// </summary>
        public decimal HousingFundPersonal { get; set; }
        
        /// <summary>
        /// 迟到扣款
        /// </summary>
        public decimal LateDeduction { get; set; }
        
        /// <summary>
        /// 早退扣款
        /// </summary>
        public decimal EarlyLeaveDeduction { get; set; }
        
        /// <summary>
        /// 缺勤扣款
        /// </summary>
        public decimal AbsenceDeduction { get; set; }
        
        /// <summary>
        /// 其他扣款
        /// </summary>
        public decimal OtherDeduction { get; set; }
        
        /// <summary>
        /// 总扣款
        /// </summary>
        public decimal TotalDeduction { get; set; }
        
        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetPay { get; set; }
        
        /// <summary>
        /// 计算详情
        /// </summary>
        public List<PayrollCalculationDetail> CalculationDetails { get; set; } = new List<PayrollCalculationDetail>();
        
        /// <summary>
        /// 计算状态
        /// </summary>
        public string CalculationStatus { get; set; }
        
        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime CalculationTime { get; set; }
        
        /// <summary>
        /// 计算人
        /// </summary>
        public string CalculatedBy { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
    
    /// <summary>
    /// 工资计算明细
    /// </summary>
    public class PayrollCalculationDetail
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        
        /// <summary>
        /// 项目类型（收入/扣款）
        /// </summary>
        public string ItemType { get; set; }
        
        /// <summary>
        /// 计算公式
        /// </summary>
        public string Formula { get; set; }
        
        /// <summary>
        /// 计算基数
        /// </summary>
        public decimal BaseAmount { get; set; }
        
        /// <summary>
        /// 计算比例/系数
        /// </summary>
        public decimal Rate { get; set; }
        
        /// <summary>
        /// 计算结果
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }
    
    /// <summary>
    /// 批量工资计算结果
    /// </summary>
    public class BatchPayrollCalculationResult
    {
        /// <summary>
        /// 计算期间
        /// </summary>
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// 成功计算数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败计算数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 计算结果列表
        /// </summary>
        public List<PayrollCalculationResult> Results { get; set; } = new List<PayrollCalculationResult>();
        
        /// <summary>
        /// 失败记录
        /// </summary>
        public List<PayrollCalculationFailure> Failures { get; set; } = new List<PayrollCalculationFailure>();
        
        /// <summary>
        /// 汇总统计
        /// </summary>
        public PayrollSummaryStatistics Summary { get; set; }
        
        /// <summary>
        /// 计算开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 计算结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 计算耗时（秒）
        /// </summary>
        public double ElapsedSeconds => (EndTime - StartTime).TotalSeconds;
    }
    
    /// <summary>
    /// 工资计算失败记录
    /// </summary>
    public class PayrollCalculationFailure
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        
        /// <summary>
        /// 详细错误
        /// </summary>
        public List<string> DetailErrors { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 工资汇总统计
    /// </summary>
    public class PayrollSummaryStatistics
    {
        /// <summary>
        /// 总应发工资
        /// </summary>
        public decimal TotalGrossPay { get; set; }
        
        /// <summary>
        /// 总实发工资
        /// </summary>
        public decimal TotalNetPay { get; set; }
        
        /// <summary>
        /// 总扣款
        /// </summary>
        public decimal TotalDeduction { get; set; }
        
        /// <summary>
        /// 总个税
        /// </summary>
        public decimal TotalIncomeTax { get; set; }
        
        /// <summary>
        /// 总社保个人部分
        /// </summary>
        public decimal TotalSocialSecurityPersonal { get; set; }
        
        /// <summary>
        /// 总公积金个人部分
        /// </summary>
        public decimal TotalHousingFundPersonal { get; set; }
        
        /// <summary>
        /// 平均应发工资
        /// </summary>
        public decimal AverageGrossPay { get; set; }
        
        /// <summary>
        /// 平均实发工资
        /// </summary>
        public decimal AverageNetPay { get; set; }
        
        /// <summary>
        /// 最高工资
        /// </summary>
        public decimal MaxSalary { get; set; }
        
        /// <summary>
        /// 最低工资
        /// </summary>
        public decimal MinSalary { get; set; }
    }
    
    /// <summary>
    /// 工资结果详情DTO
    /// </summary>
    public class PayrollResultDetailDto : BaseDto
    {
        /// <summary>
        /// 员工信息
        /// </summary>
        public EmployeeListDto Employee { get; set; }
        
        /// <summary>
        /// 工资期间
        /// </summary>
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 工资计算结果
        /// </summary>
        public PayrollCalculationResult CalculationResult { get; set; }
        
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; }
        
        /// <summary>
        /// 审核人
        /// </summary>
        public string ApprovedBy { get; set; }
        
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? ApprovedAt { get; set; }
        
        /// <summary>
        /// 审核意见
        /// </summary>
        public string ApprovalComments { get; set; }
        
        /// <summary>
        /// 发放状态
        /// </summary>
        public string PaymentStatus { get; set; }
        
        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// 发放方式
        /// </summary>
        public string PaymentMethod { get; set; }
    }
    
    /// <summary>
    /// 工资结果列表DTO
    /// </summary>
    public class PayrollResultListDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }
        
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; }
        
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// 工资年月
        /// </summary>
        public string PayrollPeriod { get; set; }
        
        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossPay { get; set; }
        
        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetPay { get; set; }
        
        /// <summary>
        /// 计算状态
        /// </summary>
        public string CalculationStatus { get; set; }
        
        /// <summary>
        /// 审核状态
        /// </summary>
        public string ApprovalStatus { get; set; }
        
        /// <summary>
        /// 发放状态
        /// </summary>
        public string PaymentStatus { get; set; }
        
        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime CalculationTime { get; set; }
    }
    
    /// <summary>
    /// 工资历史DTO
    /// </summary>
    public class PayrollHistoryDto
    {
        /// <summary>
        /// 工资期间
        /// </summary>
        public string PayrollPeriod { get; set; }
        
        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossPay { get; set; }
        
        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetPay { get; set; }
        
        /// <summary>
        /// 总扣款
        /// </summary>
        public decimal TotalDeduction { get; set; }
        
        /// <summary>
        /// 发放状态
        /// </summary>
        public string PaymentStatus { get; set; }
        
        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// 计算时间
        /// </summary>
        public DateTime CalculationTime { get; set; }
    }
    
    /// <summary>
    /// 工资统计DTO
    /// </summary>
    public class PayrollStatisticsDto
    {
        /// <summary>
        /// 统计期间
        /// </summary>
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 员工总数
        /// </summary>
        public int TotalEmployees { get; set; }
        
        /// <summary>
        /// 已计算员工数
        /// </summary>
        public int CalculatedEmployees { get; set; }
        
        /// <summary>
        /// 已审核员工数
        /// </summary>
        public int ApprovedEmployees { get; set; }
        
        /// <summary>
        /// 已发放员工数
        /// </summary>
        public int PaidEmployees { get; set; }
        
        /// <summary>
        /// 工资汇总统计
        /// </summary>
        public PayrollSummaryStatistics Summary { get; set; }
        
        /// <summary>
        /// 部门工资统计
        /// </summary>
        public List<DepartmentPayrollStatisticsDto> DepartmentStatistics { get; set; } = new List<DepartmentPayrollStatisticsDto>();
    }
    
    /// <summary>
    /// 部门工资统计DTO
    /// </summary>
    public class DepartmentPayrollStatisticsDto
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// 员工数量
        /// </summary>
        public int EmployeeCount { get; set; }
        
        /// <summary>
        /// 总应发工资
        /// </summary>
        public decimal TotalGrossPay { get; set; }
        
        /// <summary>
        /// 总实发工资
        /// </summary>
        public decimal TotalNetPay { get; set; }
        
        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }
        
        /// <summary>
        /// 占总工资比例
        /// </summary>
        public decimal Percentage { get; set; }
    }
    
    /// <summary>
    /// 工资条DTO
    /// </summary>
    public class PayslipDto
    {
        /// <summary>
        /// 员工信息
        /// </summary>
        public EmployeeListDto Employee { get; set; }
        
        /// <summary>
        /// 工资期间
        /// </summary>
        public PayrollPeriod Period { get; set; }
        
        /// <summary>
        /// 工资明细
        /// </summary>
        public List<PayslipItem> Items { get; set; } = new List<PayslipItem>();
        
        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal GrossPay { get; set; }
        
        /// <summary>
        /// 总扣款
        /// </summary>
        public decimal TotalDeduction { get; set; }
        
        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetPay { get; set; }
        
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime GeneratedAt { get; set; }
    }
    
    /// <summary>
    /// 工资条项目
    /// </summary>
    public class PayslipItem
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }
        
        /// <summary>
        /// 项目类型
        /// </summary>
        public string ItemType { get; set; }
        
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }
}