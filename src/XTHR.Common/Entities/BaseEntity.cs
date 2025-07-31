using System;

namespace XTHR.Common.Entities
{
    /// <summary>
    /// 基础实体类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public TKey Id { get; set; } = default!;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}