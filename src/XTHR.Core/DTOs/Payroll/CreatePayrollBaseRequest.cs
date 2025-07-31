using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.Payroll
{
    /// <summary>
    /// 创建薪资基础数据请求
    /// </summary>
    public class CreatePayrollBaseRequest
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "基本工资不能为负数")]
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 岗位工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "岗位工资不能为负数")]
        public decimal PositionSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "绩效工资不能为负数")]
        public decimal PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴补贴
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "津贴补贴不能为负数")]
        public decimal Allowance { get; set; }

        /// <summary>
        /// 社保基数
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "社保基数不能为负数")]
        public decimal SocialSecurityBase { get; set; }

        /// <summary>
        /// 公积金基数
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "公积金基数不能为负数")]
        public decimal ProvidentFundBase { get; set; }

        /// <summary>
        /// 个税起征点
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "个税起征点不能为负数")]
        public decimal TaxThreshold { get; set; } = 5000;

        /// <summary>
        /// 生效日期
        /// </summary>
        [Required(ErrorMessage = "生效日期不能为空")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500, ErrorMessage = "备注不能超过500个字符")]
        public string? Remark { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatorId { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        [StringLength(50, ErrorMessage = "操作人姓名不能超过50个字符")]
        public string? OperatorName { get; set; }
    }
}