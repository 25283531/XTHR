using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置审计日志DTO
    /// </summary>
    public class ConfigAuditLogDto
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; } = string.Empty;

        /// <summary>
        /// 旧值
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string? NewValue { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int OperatedBy { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string OperatedByName { get; set; } = string.Empty;

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string? OperationIp { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string? OperationDescription { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// 配置审计日志查询请求
    /// </summary>
    public class ConfigAuditLogQueryRequest
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public string? ConfigKey { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string? OperationType { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public int? OperatedBy { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public string? SortDirection { get; set; }
    }
}