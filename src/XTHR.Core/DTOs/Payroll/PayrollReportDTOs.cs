using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 工资明细报表DTO
    /// </summary>
    public class PayrollDetailReportDto
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 工资月份
        /// </summary>
        public DateTime PayrollMonth { get; set; }

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
        /// 津贴补贴
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
        /// 其他奖金
        /// </summary>
        public decimal OtherBonus { get; set; }

        /// <summary>
        /// 应发工资合计
        /// </summary>
        public decimal GrossSalary { get; set; }

        /// <summary>
        /// 社保个人部分
        /// </summary>
        public decimal SocialInsurancePersonal { get; set; }

        /// <summary>
        /// 公积金个人部分
        /// </summary>
        public decimal HousingFundPersonal { get; set; }

        /// <summary>
        /// 个税
        /// </summary>
        public decimal IncomeTax { get; set; }

        /// <summary>
        /// 迟到扣款
        /// </summary>
        public decimal LateDeduction { get; set; }

        /// <summary>
        /// 早退扣款
        /// </summary>
        public decimal EarlyLeaveDeduction { get; set; }

        /// <summary>
        /// 旷工扣款
        /// </summary>
        public decimal AbsenceDeduction { get; set; }

        /// <summary>
        /// 其他扣款
        /// </summary>
        public decimal OtherDeductions { get; set; }

        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal NetSalary { get; set; }
    }

    /// <summary>
    /// 工资汇总报表DTO
    /// </summary>
    public class PayrollSummaryReportDto
    {
        /// <summary>
        /// 工资月份
        /// </summary>
        public DateTime PayrollMonth { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 员工人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 应发工资总额
        /// </summary>
        public decimal TotalGrossSalary { get; set; }

        /// <summary>
        /// 社保总额
        /// </summary>
        public decimal TotalSocialInsurance { get; set; }

        /// <summary>
        /// 公积金总额
        /// </summary>
        public decimal TotalHousingFund { get; set; }

        /// <summary>
        /// 个税总额
        /// </summary>
        public decimal TotalIncomeTax { get; set; }

        /// <summary>
        /// 实发工资总额
        /// </summary>
        public decimal TotalNetSalary { get; set; }
    }

    /// <summary>
    /// 工资统计DTO
    /// </summary>
    public class PayrollStatisticsDto
    {
        /// <summary>
        /// 工资月份
        /// </summary>
        public DateTime PayrollMonth { get; set; }

        /// <summary>
        /// 总员工数
        /// </summary>
        public int TotalEmployees { get; set; }

        /// <summary>
        /// 平均工资
        /// </summary>
        public decimal AverageSalary { get; set; }

        /// <summary>
        /// 最高工资
        /// </summary>
        public decimal MaxSalary { get; set; }

        /// <summary>
        /// 最低工资
        /// </summary>
        public decimal MinSalary { get; set; }

        /// <summary>
        /// 工资中位数
        /// </summary>
        public decimal MedianSalary { get; set; }
    }

    /// <summary>
    /// 工资报表查询请求
    /// </summary>
    public class PayrollReportQueryRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}