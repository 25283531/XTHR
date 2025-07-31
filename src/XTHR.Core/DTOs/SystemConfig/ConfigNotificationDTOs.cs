using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs.SystemConfig
{
    /// <summary>
    /// 配置变更通知设置DTO
    /// </summary>
    public class ConfigChangeNotificationSettingDto
    {
        /// <summary>
        /// 通知设置ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 通知方式（邮件/短信/站内消息）
        /// </summary>
        public string NotificationType { get; set; } = string.Empty;

        /// <summary>
        /// 通知接收人ID列表
        /// </summary>
        public List<int> RecipientIds { get; set; } = new();

        /// <summary>
        /// 通知接收人邮箱列表
        /// </summary>
        public List<string> RecipientEmails { get; set; } = new();

        /// <summary>
        /// 通知条件（所有变更/特定值变更/特定操作类型）
        /// </summary>
        public string NotificationCondition { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用通知
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 通知模板
        /// </summary>
        public string NotificationTemplate { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        public int? UpdatedBy { get; set; }
    }

    /// <summary>
    /// 配置变更通知请求DTO
    /// </summary>
    public class ConfigChangeNotificationRequest
    {
        /// <summary>
        /// 配置键
        /// </summary>
        [Required(ErrorMessage = "配置键不能为空")]
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 旧值
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        public string? NewValue { get; set; }

        /// <summary>
        /// 变更类型（创建/更新/删除）
        /// </summary>
        [Required(ErrorMessage = "变更类型不能为空")]
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        [Required(ErrorMessage = "操作人ID不能为空")]
        public int OperatedBy { get; set; }

        /// <summary>
        /// 操作人姓名
        /// </summary>
        public string? OperatedByName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否需要立即通知
        /// </summary>
        public bool ImmediateNotification { get; set; } = true;
    }

    /// <summary>
    /// 通知发送结果DTO
    /// </summary>
    public class NotificationSendResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 发送的通知数量
        /// </summary>
        public int NotificationCount { get; set; }

        /// <summary>
        /// 失败的接收人列表
        /// </summary>
        public List<string> FailedRecipients { get; set; } = new();

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// 通知记录DTO
    /// </summary>
    public class NotificationRecordDto
    {
        /// <summary>
        /// 通知记录ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; } = string.Empty;

        /// <summary>
        /// 通知类型
        /// </summary>
        public string NotificationType { get; set; } = string.Empty;

        /// <summary>
        /// 接收人ID
        /// </summary>
        public int RecipientId { get; set; }

        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string RecipientName { get; set; } = string.Empty;

        /// <summary>
        /// 接收人邮箱
        /// </summary>
        public string RecipientEmail { get; set; } = string.Empty;

        /// <summary>
        /// 通知内容
        /// </summary>
        public string NotificationContent { get; set; } = string.Empty;

        /// <summary>
        /// 发送状态
        /// </summary>
        public string SendStatus { get; set; } = string.Empty;

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }
    }
}