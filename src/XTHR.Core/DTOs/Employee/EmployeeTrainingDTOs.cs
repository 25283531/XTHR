using System;

namespace XTHR.Core.DTOs.Employee
{
    /// <summary>
    /// 员工培训记录DTO
    /// </summary>
    public class EmployeeTrainingRecordDto
    {
        /// <summary>
        /// 培训记录ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训机构
        /// </summary>
        public string TrainingOrganization { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 培训结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 培训时长（小时）
        /// </summary>
        public decimal TrainingHours { get; set; }

        /// <summary>
        /// 培训费用
        /// </summary>
        public decimal TrainingCost { get; set; }

        /// <summary>
        /// 培训地点
        /// </summary>
        public string TrainingLocation { get; set; } = string.Empty;

        /// <summary>
        /// 培训内容
        /// </summary>
        public string TrainingContent { get; set; } = string.Empty;

        /// <summary>
        /// 培训结果
        /// </summary>
        public string TrainingResult { get; set; } = string.Empty;

        /// <summary>
        /// 获得证书
        /// </summary>
        public string CertificateObtained { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
    }
}