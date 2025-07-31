using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data;
using XTHR.Core.Common;
using XTHR.Core.DTOs;
using XTHR.Core.DTOs.Requests;
using XTHR.Common.Entities;

namespace XTHR.Core.Interfaces.Repositories
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IBaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        #region 查询操作
        
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体对象</returns>
        Task<TEntity> GetByIdAsync(TKey id);
        
        /// <summary>
        /// 根据条件获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体对象</returns>
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// 根据条件获取第一个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体对象</returns>
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <returns>分页结果</returns>
        Task<PagedResult<TEntity>> GetPagedAsync<TOrderBy>(
            Expression<Func<TEntity, bool>> predicate,
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, TOrderBy>>? orderBy = null,
            bool ascending = true);
        
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <returns>查询对象</returns>
        IQueryable<TEntity> GetQueryable();
        
        /// <summary>
        /// 根据条件获取查询对象
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>查询对象</returns>
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? predicate);
        
        #endregion
        
        #region 统计操作
        
        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns>总数</returns>
        Task<int> CountAsync();
        
        /// <summary>
        /// 根据条件获取数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>数量</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate);
        
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate);
        
        /// <summary>
        /// 检查ID是否存在
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsAsync(TKey id);
        
        #endregion
        
        #region 新增操作
        
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>添加后的实体</returns>
        Task<TEntity> AddAsync(TEntity entity);
        
        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>添加的数量</returns>
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        
        #endregion
        
        #region 更新操作
        
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>更新后的实体</returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        
        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>更新的数量</returns>
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);
        
        /// <summary>
        /// 根据条件批量更新
        /// </summary>
        /// <param name="predicate">更新条件</param>
        /// <param name="updateExpression">更新表达式</param>
        /// <returns>更新的数量</returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>>? predicate, Expression<Func<TEntity, TEntity>> updateExpression);
        
        #endregion
        
        #region 删除操作
        
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(TKey id);
        
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(TEntity entity);
        
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns>删除的数量</returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>>? predicate);
        
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>删除的数量</returns>
        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);
        
        /// <summary>
        /// 根据ID列表批量删除
        /// </summary>
        /// <param name="ids">ID列表</param>
        /// <returns>删除的数量</returns>
        Task<int> DeleteRangeAsync(IEnumerable<TKey> ids);
        
        #endregion
        
        #region 软删除操作（如果实体支持软删除）
        
        /// <summary>
        /// 软删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>是否软删除成功</returns>
        Task<bool> SoftDeleteAsync(TKey id);
        
        /// <summary>
        /// 批量软删除实体
        /// </summary>
        /// <param name="ids">实体ID列表</n        /// <returns>软删除的数量</returns>
        Task<int> SoftDeleteRangeAsync(IEnumerable<TKey> ids);
        
        /// <summary>
        /// 恢复软删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>是否恢复成功</returns>
        Task<bool> RestoreAsync(TKey id);
        
        #endregion
        
        #region 事务操作
        
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>事务对象</returns>
        Task<IDbTransaction> BeginTransactionAsync();
        
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        Task CommitTransactionAsync(IDbTransaction transaction);
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transaction">事务对象</param>
        Task RollbackTransactionAsync(IDbTransaction transaction);
        
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
        
        #endregion
    }


    
    /// <summary>
    /// 数据库事务接口
    /// </summary>
    public interface IDbTransaction : IDisposable
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        Task CommitAsync();
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        Task RollbackAsync();
        
        /// <summary>
        /// 事务ID
        /// </summary>
        string TransactionId { get; }
        
        /// <summary>
        /// 事务状态
        /// </summary>
        TransactionStatus Status { get; }
    }
    
    /// <summary>
    /// 事务状态枚举
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// 活动中
        /// </summary>
        Active,
        
        /// <summary>
        /// 已提交
        /// </summary>
        Committed,
        
        /// <summary>
        /// 已回滚
        /// </summary>
        RolledBack,
        
        /// <summary>
        /// 已释放
        /// </summary>
        Disposed
    }
}