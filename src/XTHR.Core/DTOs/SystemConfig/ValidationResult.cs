using System.Collections.Generic;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 系统配置验证结果
    /// </summary>
    public class SystemConfigValidationResult
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 错误消息列表
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 警告消息列表
        /// </summary>
        public List<string> Warnings { get; set; } = new List<string>();

        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="error">错误消息</param>
        public void AddError(string error)
        {
            Errors.Add(error);
            IsValid = false;
        }

        /// <summary>
        /// 添加警告
        /// </summary>
        /// <param name="warning">警告消息</param>
        public void AddWarning(string warning)
        {
            Warnings.Add(warning);
        }

        /// <summary>
        /// 合并验证结果
        /// </summary>
        /// <param name="other">另一个验证结果</param>
        public void Merge(SystemConfigValidationResult other)
        {
            if (other == null) return;

            Errors.AddRange(other.Errors);
            Warnings.AddRange(other.Warnings);
            if (!other.IsValid)
            {
                IsValid = false;
            }
        }
    }
}