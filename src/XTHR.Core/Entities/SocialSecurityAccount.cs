using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 社保账户信息
    /// </summary>
    public class SocialSecurityAccount : BaseEntity<int>
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        /// <summary>
        /// 社保项目名称（如：养老保险、医疗保险）
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; }

        /// <summary>
        /// 缴纳基数
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ContributionBase { get; set; }

        /// <summary>
        /// 公司缴纳比例
        /// </summary>
        [Column(TypeName = "decimal(5, 4)")]
        public decimal CompanyContributionRatio { get; set; }

        /// <summary>
        /// 公司缴纳金额
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CompanyContributionAmount { get; set; }

        /// <summary>
        /// 个人缴纳比例
        /// </summary>
        [Column(TypeName = "decimal(5, 4)")]
        public decimal PersonalContributionRatio { get; set; }

        /// <summary>
        /// 个人缴纳金额
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PersonalContributionAmount { get; set; }
    }
}