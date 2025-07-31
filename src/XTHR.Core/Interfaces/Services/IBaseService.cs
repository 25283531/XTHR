using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XTHR.Core.DTOs.Common;

namespace XTHR.Core.Interfaces.Services
{
    /// <summary>
    /// 基础服务接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDetailDto">详情DTO类型</typeparam>
    /// <typeparam name="TListDto">列表DTO类型</typeparam>
    /// <typeparam name="TCreateRequest">创建请求类型</typeparam>
    /// <typeparam name="TUpdateRequest">更新请求类型</typeparam>
    public interface IBaseService<TEntity, TKey, TDetailDto, TListDto, TCreateRequest, TUpdateRequest>
        where TEntity : class
        where TKey : notnull
        where TDetailDto : class
        where TListDto : class
        where TCreateRequest : class
        where TUpdateRequest : class
    {
        #region 查询操作
        
        /// <summary>
        /// 根据ID获取详情
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>详情DTO</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<TDetailDto>> GetByIdAsync(TKey id);
        
        /// <summary>
        /// 根据条件获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>详情DTO</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<TDetailDto>> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>列表DTO集合</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<TListDto>>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <returns>分页结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<XTHR.Core.DTOs.Common.PagedResult<TListDto>>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool ascending = true);
        
        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>总数</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> GetCountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ExistsAsync(Expression<Func<TEntity, bool>>? predicate);
        
        #endregion
        
        #region 新增操作
        
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<TDetailDto>> CreateAsync(TCreateRequest request);
        
        /// <summary>
        /// 批量创建实体
        /// </summary>
        /// <param name="requests">创建请求集合</param>
        /// <returns>创建结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<TDetailDto>>> CreateBatchAsync(IEnumerable<TCreateRequest> requests);
        
        #endregion
        
        #region 更新操作
        
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<TDetailDto>> UpdateAsync(TKey id, TUpdateRequest request);
        
        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="updates">更新请求字典（ID -> 更新请求）</param>
        /// <returns>更新结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<IEnumerable<TDetailDto>>> UpdateBatchAsync(Dictionary<TKey, TUpdateRequest> updates);
        
        /// <summary>
        /// 根据条件批量更新
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>受影响的行数</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> UpdateByConditionAsync(
            Expression<Func<TEntity, bool>>? predicate,
            Expression<Func<TEntity, TEntity>> updateExpression);
        
        #endregion
        
        #region 删除操作
        
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> DeleteAsync(TKey id);
        
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> DeleteBatchAsync(IEnumerable<TKey> ids);
        
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>受影响的行数</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> DeleteByConditionAsync(Expression<Func<TEntity, bool>>? predicate);
        
        /// <summary>
        /// 软删除实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> SoftDeleteAsync(TKey id);
        
        /// <summary>
        /// 批量软删除实体
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns>删除结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<int>> SoftDeleteBatchAsync(IEnumerable<TKey> ids);
        
        /// <summary>
        /// 恢复软删除的实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>恢复结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> RestoreAsync(TKey id);
        
        #endregion
        
        #region 验证操作
        
        /// <summary>
        /// 验证创建请求
        /// </summary>
        /// <param name="request">创建请求</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ValidationResult>> ValidateCreateAsync(TCreateRequest request);
        
        /// <summary>
        /// 验证更新请求
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="request">更新请求</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ValidationResult>> ValidateUpdateAsync(TKey id, TUpdateRequest request);
        
        /// <summary>
        /// 验证删除操作
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>验证结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ValidationResult>> ValidateDeleteAsync(TKey id);
        
        #endregion
        
        #region 业务规则
        
        /// <summary>
        /// 检查业务规则
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="operation">操作类型</param>
        /// <returns>业务规则检查结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<BusinessRuleResult>> CheckBusinessRulesAsync(TEntity entity, BusinessOperation operation);
        
        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="operation">操作类型</param>
        /// <returns>执行结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<bool>> ExecuteBusinessLogicAsync(TEntity entity, BusinessOperation operation);
        
        #endregion
        
        #region 缓存操作
        
        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task ClearCacheAsync(string? key = null);
        
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        Task RefreshCacheAsync(string? key = null);
        
        /// <summary>
        /// 预热缓存
        /// </summary>
        Task WarmupCacheAsync();
        
        #endregion
        
        #region 导入导出
        
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="data">导入数据</param>
        /// <param name="options">导入选项</param>
        /// <returns>导入结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ImportResult<TDetailDto>>> ImportAsync(IEnumerable<TCreateRequest> data, ImportOptions? options = null);
        
        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="options">导出选项</param>
        /// <returns>导出结果</returns>
        Task<XTHR.Core.DTOs.Common.ApiResult<ExportResult<TListDto>>> ExportAsync(Expression<Func<TEntity, bool>>? predicate = null, ExportOptions? options = null);
        
        #endregion
    }
    
    /// <summary>
    /// 验证结果
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 错误信息集合
        /// </summary>
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();
        
        /// <summary>
        /// 警告信息集合
        /// </summary>
        public List<ValidationWarning> Warnings { get; set; } = new List<ValidationWarning>();
    }
    
    /// <summary>
    /// 验证错误
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误值
        /// </summary>
        public object AttemptedValue { get; set; }
    }
    
    /// <summary>
    /// 验证警告
    /// </summary>
    public class ValidationWarning
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        
        /// <summary>
        /// 警告代码
        /// </summary>
        public string WarningCode { get; set; }
        
        /// <summary>
        /// 警告消息
        /// </summary>
        public string WarningMessage { get; set; }
    }
    
    /// <summary>
    /// 业务规则结果
    /// </summary>
    public class BusinessRuleResult
    {
        /// <summary>
        /// 规则检查是否通过
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// 违反的规则集合
        /// </summary>
        public List<BusinessRuleViolation> Violations { get; set; } = new List<BusinessRuleViolation>();
    }
    
    /// <summary>
    /// 业务规则违反
    /// </summary>
    public class BusinessRuleViolation
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public string RuleName { get; set; }
        
        /// <summary>
        /// 规则描述
        /// </summary>
        public string RuleDescription { get; set; }
        
        /// <summary>
        /// 违反原因
        /// </summary>
        public string ViolationReason { get; set; }
        
        /// <summary>
        /// 严重级别
        /// </summary>
        public BusinessRuleSeverity Severity { get; set; }
    }
    
    /// <summary>
    /// 业务规则严重级别
    /// </summary>
    public enum BusinessRuleSeverity
    {
        /// <summary>
        /// 信息
        /// </summary>
        Info,
        
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        
        /// <summary>
        /// 错误
        /// </summary>
        Error,
        
        /// <summary>
        /// 严重错误
        /// </summary>
        Critical
    }
    
    /// <summary>
    /// 业务操作类型
    /// </summary>
    public enum BusinessOperation
    {
        /// <summary>
        /// 创建
        /// </summary>
        Create,
        
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        
        /// <summary>
        /// 查询
        /// </summary>
        Read,
        
        /// <summary>
        /// 批量操作
        /// </summary>
        Batch,
        
        /// <summary>
        /// 导入
        /// </summary>
        Import,
        
        /// <summary>
        /// 导出
        /// </summary>
        Export
    }
    
    /// <summary>
    /// 导入结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ImportResult<T>
    {
        /// <summary>
        /// 导入是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 成功记录数
        /// </summary>
        public int SuccessRecords { get; set; }
        
        /// <summary>
        /// 失败记录数
        /// </summary>
        public int FailedRecords { get; set; }
        
        /// <summary>
        /// 成功导入的数据
        /// </summary>
        public List<T> SuccessData { get; set; } = new List<T>();
        
        /// <summary>
        /// 失败记录详情
        /// </summary>
        public List<ImportFailure> Failures { get; set; } = new List<ImportFailure>();
        
        /// <summary>
        /// 导入开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 导入结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 导入耗时
        /// </summary>
        public TimeSpan Duration => EndTime - StartTime;
    }
    
    /// <summary>
    /// 导入失败记录
    /// </summary>
    public class ImportFailure
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex { get; set; }
        
        /// <summary>
        /// 原始数据
        /// </summary>
        public object OriginalData { get; set; }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        
        /// <summary>
        /// 错误详情
        /// </summary>
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
    }
    
    /// <summary>
    /// 导出结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ExportResult<T>
    {
        /// <summary>
        /// 导出是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
        /// <summary>
        /// 导出的数据
        /// </summary>
        public List<T> Data { get; set; } = new List<T>();
        
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }
        
        /// <summary>
        /// 导出格式
        /// </summary>
        public string Format { get; set; }
        
        /// <summary>
        /// 记录总数
        /// </summary>
        public int TotalRecords { get; set; }
        
        /// <summary>
        /// 导出开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 导出结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 导出耗时
        /// </summary>
        public TimeSpan Duration => EndTime - StartTime;
    }
    
    /// <summary>
    /// 导入选项
    /// </summary>
    public class ImportOptions
    {
        /// <summary>
        /// 是否跳过重复记录
        /// </summary>
        public bool SkipDuplicates { get; set; } = true;
        
        /// <summary>
        /// 是否更新已存在的记录
        /// </summary>
        public bool UpdateExisting { get; set; } = false;
        
        /// <summary>
        /// 批次大小
        /// </summary>
        public int BatchSize { get; set; } = 1000;
        
        /// <summary>
        /// 是否验证数据
        /// </summary>
        public bool ValidateData { get; set; } = true;
        
        /// <summary>
        /// 是否使用事务
        /// </summary>
        public bool UseTransaction { get; set; } = true;
        
        /// <summary>
        /// 最大错误数（超过则停止导入）
        /// </summary>
        public int MaxErrors { get; set; } = 100;
    }
    
    /// <summary>
    /// 导出选项
    /// </summary>
    public class ExportOptions
    {
        /// <summary>
        /// 导出格式
        /// </summary>
        public ExportFormat Format { get; set; } = ExportFormat.Excel;
        
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// 是否包含标题行
        /// </summary>
        public bool IncludeHeaders { get; set; } = true;
        
        /// <summary>
        /// 最大记录数
        /// </summary>
        public int MaxRecords { get; set; } = 100000;
        
        /// <summary>
        /// 导出字段
        /// </summary>
        public List<string> Fields { get; set; } = new List<string>();
        
        /// <summary>
        /// 是否压缩文件
        /// </summary>
        public bool CompressFile { get; set; } = false;
    }
    
    /// <summary>
    /// 导出格式
    /// </summary>
    public enum ExportFormat
    {
        /// <summary>
        /// Excel格式
        /// </summary>
        Excel,
        
        /// <summary>
        /// CSV格式
        /// </summary>
        Csv,
        
        /// <summary>
        /// JSON格式
        /// </summary>
        Json,
        
        /// <summary>
        /// XML格式
        /// </summary>
        Xml,
        
        /// <summary>
        /// PDF格式
        /// </summary>
        Pdf
    }
}