using System;

namespace XTHR.Common.Entities
{
    /// <summary>
    /// 实体接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        TKey Id { get; set; }
    }
}