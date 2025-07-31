using System;
using System.Collections.Generic;

namespace XTHR.Core.DTOs.Common
{
    /// <summary>
    /// API通用返回结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 返回数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResult<T> Ok(T data, string message = "操作成功")
        {
            return new ApiResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResult<T> Fail(string message, List<string>? errors = null)
        {
            return new ApiResult<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// API通用返回结果（无数据）
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResult Ok(string message = "操作成功")
        {
            return new ApiResult
            {
                Success = true,
                Message = message
            };
        }

        public static ApiResult Fail(string message, List<string>? errors = null)
        {
            return new ApiResult
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}