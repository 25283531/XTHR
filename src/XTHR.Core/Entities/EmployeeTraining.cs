using System;

namespace XTHR.Core.Entities
{
    /// <summary>
    /// 员工培训实体
    /// </summary>
    public class EmployeeTraining
    {
        /// <summary>
        /// 培训记录ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训机构
        /// </summary>
        public string? TrainingInstitution { get; set; }

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingType { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 培训结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 培训时长（小时）
        /// </summary>
        public decimal TrainingHours { get; set; }

        /// <summary>
        /// 培训费用
        /// </summary>
        public decimal? TrainingCost { get; set; }

        /// <summary>
        /// 培训地点
        /// </summary>
        public string? TrainingLocation { get; set; }

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string? Trainer { get; set; }

        /// <summary>
        /// 培训内容
        /// </summary>
        public string? TrainingContent { get; set; }

        /// <summary>
        /// 培训成绩
        /// </summary>
        public string? TrainingScore { get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
        public string? CertificateNumber { get; set; }

        /// <summary>
        /// 证书有效期
        /// </summary>
        public DateTime? CertificateExpiryDate { get; set; }

        /// <summary>
        /// 培训状态
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