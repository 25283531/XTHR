using System;
using System.ComponentModel.DataAnnotations;

namespace XTHR.Core.DTOs
{
    /// <summary>
    /// 基础DTO类
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string UpdatedBy { get; set; }
    }
    

    
    /// <summary>
    /// 服务结果
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误代码
        /// </summary>
        public string? ErrorCode { get; set; }
        
        /// <summary>
        /// 详细错误信息
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
        
        /// <summary>
        /// 成功结果
        /// </summary>
        public static ServiceResult Success() => new ServiceResult { IsSuccess = true };
        
        /// <summary>
        /// 失败结果
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns>失败结果</returns>
        public static ServiceResult Failure(string errorMessage, string? errorCode = null)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode
            };
        }
        
        /// <summary>
        /// 失败结果（多个错误）
        /// </summary>
        /// <param name="errors">错误列表</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns>失败结果</returns>
        public static ServiceResult Failure(List<string> errors, string? errorCode = null)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                ErrorMessage = errors.FirstOrDefault(),
                ErrorCode = errorCode,
                Errors = errors
            };
        }
    }
    
    /// <summary>
    /// 带返回值的服务结果
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// 成功结果
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns>成功结果</returns>
        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T>
            {
                IsSuccess = true,
                Data = data
            };
        }
        
        /// <summary>
        /// 失败结果
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns>失败结果</returns>
        public new static ServiceResult<T> Failure(string errorMessage, string? errorCode = null)
        {
            return new ServiceResult<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                ErrorCode = errorCode
            };
        }
    }
    
    /// <summary>
    /// 批量导入结果
    /// </summary>
    /// <typeparam name="T">导入数据类型</typeparam>
    public class BatchImportResult<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// 成功导入数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败记录数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 跳过记录数
        /// </summary>
        public int SkippedCount { get; set; }
        
        /// <summary>
        /// 成功记录
        /// </summary>
        public List<T> SuccessItems { get; set; } = new List<T>();
        
        /// <summary>
        /// 失败记录
        /// </summary>
        public List<ImportFailureItem<T>> FailureItems { get; set; } = new List<ImportFailureItem<T>>();
        
        /// <summary>
        /// 是否全部成功
        /// </summary>
        public bool IsAllSuccess => FailureCount == 0;
        
        /// <summary>
        /// 成功率
        /// </summary>
        public decimal SuccessRate => TotalCount > 0 ? (decimal)SuccessCount / TotalCount * 100 : 0;
    }
    
    /// <summary>
    /// 导入失败项
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ImportFailureItem<T>
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }
        
        /// <summary>
        /// 原始数据
        /// </summary>
        public T OriginalData { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误代码
        /// </summary>
        public string? ErrorCode { get; set; }
        
        /// <summary>
        /// 详细错误
        /// </summary>
        public List<string> DetailErrors { get; set; } = new List<string>();
    }
    
    /// <summary>
    /// 批量计算结果
    /// </summary>
    /// <typeparam name="T">计算结果类型</typeparam>
    public class BatchCalculationResult<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// 成功计算数
        /// </summary>
        public int SuccessCount { get; set; }
        
        /// <summary>
        /// 失败记录数
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// 计算结果
        /// </summary>
        public List<T> Results { get; set; } = new List<T>();
        
        /// <summary>
        /// 失败记录
        /// </summary>
        public List<CalculationFailureItem> FailureItems { get; set; } = new List<CalculationFailureItem>();
        
        /// <summary>
        /// 是否全部成功
        /// </summary>
        public bool IsAllSuccess => FailureCount == 0;
    }
    
    /// <summary>
    /// 计算失败项
    /// </summary>
    public class CalculationFailureItem
    {
        /// <summary>
        /// 标识符（如员工ID）
        /// </summary>
        public string Identifier { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误代码
        /// </summary>
        public string? ErrorCode { get; set; }
    }
}