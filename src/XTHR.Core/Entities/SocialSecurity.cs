using System;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 社保信息实体
    /// </summary>
    public class SocialSecurity
    {
        /// <summary>
        /// 社保ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// 社保账号
        /// </summary>
        public string SocialSecurityAccount { get; set; } = string.Empty;

        /// <summary>
        /// 社保缴纳地
        /// </summary>
        public string PaymentLocation { get; set; } = string.Empty;

        /// <summary>
        /// 社保基数
        /// </summary>
        public decimal BaseAmount { get; set; }

        /// <summary>
        /// 养老保险个人比例
        /// </summary>
        public decimal PensionPersonalRate { get; set; }

        /// <summary>
        /// 养老保险单位比例
        /// </summary>
        public decimal PensionCompanyRate { get; set; }

        /// <summary>
        /// 医疗保险个人比例
        /// </summary>
        public decimal MedicalPersonalRate { get; set; }

        /// <summary>
        /// 医疗保险单位比例
        /// </summary>
        public decimal MedicalCompanyRate { get; set; }

        /// <summary>
        /// 失业保险个人比例
        /// </summary>
        public decimal UnemploymentPersonalRate { get; set; }

        /// <summary>
        /// 失业保险单位比例
        /// </summary>
        public decimal UnemploymentCompanyRate { get; set; }

        /// <summary>
        /// 工伤保险单位比例
        /// </summary>
        public decimal WorkInjuryCompanyRate { get; set; }

        /// <summary>
        /// 生育保险单位比例
        /// </summary>
        public decimal MaternityCompanyRate { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 员工实体
        /// </summary>
        public virtual Employee Employee { get; set; } = null!;
    }
}