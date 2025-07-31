using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using XTHR.Common.Entities;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 社保缴纳项目预设
    /// </summary>
    public class SocialSecurityItem : BaseEntity<int>
    {
        /// <summary>
        /// 社保项目名称（如：养老保险、医疗保险）
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ItemName { get; set; }

        /// <summary>
        /// 默认缴纳基数
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DefaultContributionBase { get; set; }

        /// <summary>
        /// 公司缴纳比例
        /// </summary>
        [Column(TypeName = "decimal(5, 4)")]
        public decimal CompanyContributionRatio { get; set; }

        /// <summary>
        /// 个人缴纳比例
        /// </summary>
        [Column(TypeName = "decimal(5, 4)")]
        public decimal PersonalContributionRatio { get; set; }
    }
}