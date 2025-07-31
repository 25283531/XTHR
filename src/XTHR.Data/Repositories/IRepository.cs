using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XTHR.Common.Models;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IRepository<T> where T : class
    {
        #region 同步方法
        
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体对象</returns>
        T GetById(object id);
        
        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        IEnumerable<T> GetAll();
        
        /// <summary>
        /// 根据条件查找实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// 根据条件获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体对象</returns>
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>添加的实体</returns>
        T Add(T entity);
        
        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        void AddRange(IEnumerable<T> entities);
        
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);
        
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Remove(T entity);
        
        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        void Remove(object id);
        
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        void RemoveRange(IEnumerable<T> entities);
        
        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        bool Exists(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// 获取实体数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体数量</returns>
        int Count(Expression<Func<T, bool>> predicate = null);
        
        #endregion
        
        #region 异步方法
        
        /// <summary>
        /// 异步根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体对象</returns>
        Task<T> GetByIdAsync(object id);
        
        /// <summary>
        /// 异步获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// 异步根据条件查找实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// 异步根据条件获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体对象</returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// 异步添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>添加的实体</returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// 异步批量添加实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        Task AddRangeAsync(IEnumerable<T> entities);
        
        /// <summary>
        /// 异步检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// 异步获取实体数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体数量</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        
        #endregion
        
        #region 分页查询
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页索引（从0开始）</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="ascending">是否升序</param>
        /// <returns>分页结果</returns>
        (IEnumerable<T> Items, int TotalCount) GetPaged<TKey>(
            int pageIndex, 
            int pageSize, 
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, TKey>> orderBy = null,
            bool ascending = true);
        
        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="pageIndex">页索引（从0开始）</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="ascending">是否升序</param>
        /// <returns>分页结果</returns>
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync<TKey>(
            int pageIndex, 
            int pageSize, 
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, TKey>> orderBy = null,
            bool ascending = true);
        
        #endregion
    }
}