using System;

namespace XTHR.Core.Common
{
    /// <summary>
    /// API通用响应结果
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 响应数据
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建成功响应
        /// </summary>
        public static ApiResult<T> Ok(T data, string message = "操作成功")
        {
            return new ApiResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// 创建失败响应
        /// </summary>
        public static ApiResult<T> Fail(string message, string? errorCode = null)
        {
            return new ApiResult<T>
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode
            };
        }
    }

    /// <summary>
    /// API通用响应结果（无数据）
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 错误代码
        /// </summary>
        public string? ErrorCode { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建成功响应
        /// </summary>
        public static ApiResult Ok(string message = "操作成功")
        {
            return new ApiResult
            {
                Success = true,
                Message = message
            };
        }

        /// <summary>
        /// 创建失败响应
        /// </summary>
        public static ApiResult Fail(string message, string? errorCode = null)
        {
            return new ApiResult
            {
                Success = false,
                Message = message,
                ErrorCode = errorCode
            };
        }
    }
}