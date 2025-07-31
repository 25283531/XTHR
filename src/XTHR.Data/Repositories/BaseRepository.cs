using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using XTHR.Data.Services;

namespace XTHR.Data.Repositories
{
    /// <summary>
    /// 基础仓储实现类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly IDatabaseService _databaseService;
        protected readonly string _tableName;
        protected readonly string _primaryKeyColumn;
        
        protected BaseRepository(IDatabaseService databaseService, string tableName, string primaryKeyColumn = "Id")
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            _primaryKeyColumn = primaryKeyColumn ?? throw new ArgumentNullException(nameof(primaryKeyColumn));
        }
        
        #region 抽象方法
        
        /// <summary>
        /// 将实体转换为插入参数
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>参数对象</returns>
        protected abstract object EntityToInsertParameters(T entity);
        
        /// <summary>
        /// 将实体转换为更新参数
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>参数对象</returns>
        protected abstract object EntityToUpdateParameters(T entity);
        
        /// <summary>
        /// 获取插入SQL语句
        /// </summary>
        /// <returns>插入SQL</returns>
        protected abstract string GetInsertSql();
        
        /// <summary>
        /// 获取更新SQL语句
        /// </summary>
        /// <returns>更新SQL</returns>
        protected abstract string GetUpdateSql();
        
        /// <summary>
        /// 获取查询所有记录的SQL语句
        /// </summary>
        /// <returns>查询SQL</returns>
        protected virtual string GetSelectAllSql()
        {
            return $"SELECT * FROM {_tableName}";
        }
        
        /// <summary>
        /// 获取根据ID查询的SQL语句
        /// </summary>
        /// <returns>查询SQL</returns>
        protected virtual string GetSelectByIdSql()
        {
            return $"SELECT * FROM {_tableName} WHERE {_primaryKeyColumn} = @Id";
        }
        
        /// <summary>
        /// 获取删除SQL语句
        /// </summary>
        /// <returns>删除SQL</returns>
        protected virtual string GetDeleteSql()
        {
            return $"DELETE FROM {_tableName} WHERE {_primaryKeyColumn} = @Id";
        }
        
        /// <summary>
        /// 获取计数SQL语句
        /// </summary>
        /// <returns>计数SQL</returns>
        protected virtual string GetCountSql()
        {
            return $"SELECT COUNT(*) FROM {_tableName}";
        }
        
        #endregion
        
        #region 同步方法实现
        
        public virtual T GetById(object id)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return connection.QuerySingleOrDefault<T>(GetSelectByIdSql(), new { Id = id });
        }
        
        public virtual IEnumerable<T> GetAll()
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return connection.Query<T>(GetSelectAllSql());
        }
        
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            // 注意：这里简化实现，实际项目中可能需要表达式树转SQL的复杂逻辑
            // 或者使用Entity Framework Core等ORM
            var all = GetAll();
            return all.Where(predicate.Compile());
        }
        
        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Find(predicate).SingleOrDefault();
        }
        
        public virtual T Add(T entity)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            var parameters = EntityToInsertParameters(entity);
            connection.Execute(GetInsertSql(), parameters);
            
            // 如果主键是自增的，获取最后插入的ID
            if (_primaryKeyColumn.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                var lastId = connection.QuerySingle<long>("SELECT last_insert_rowid()");
                var idProperty = typeof(T).GetProperty(_primaryKeyColumn);
                if (idProperty != null && idProperty.CanWrite)
                {
                    idProperty.SetValue(entity, Convert.ChangeType(lastId, idProperty.PropertyType));
                }
            }
            
            return entity;
        }
        
        public virtual void AddRange(IEnumerable<T> entities)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var entity in entities)
                {
                    var parameters = EntityToInsertParameters(entity);
                    connection.Execute(GetInsertSql(), parameters, transaction);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        
        public virtual void Update(T entity)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            var parameters = EntityToUpdateParameters(entity);
            connection.Execute(GetUpdateSql(), parameters);
        }
        
        public virtual void Remove(T entity)
        {
            var idProperty = typeof(T).GetProperty(_primaryKeyColumn);
            if (idProperty != null)
            {
                var id = idProperty.GetValue(entity);
                Remove(id);
            }
        }
        
        public virtual void Remove(object id)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            connection.Execute(GetDeleteSql(), new { Id = id });
        }
        
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var entity in entities)
                {
                    Remove(entity);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Find(predicate).Any();
        }
        
        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                using var connection = new SqliteConnection(_databaseService.GetConnectionString());
                return connection.QuerySingle<int>(GetCountSql());
            }
            
            return Find(predicate).Count();
        }
        
        #endregion
        
        #region 异步方法实现
        
        public virtual async Task<T> GetByIdAsync(object id)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<T>(GetSelectByIdSql(), new { Id = id });
        }
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<T>(GetSelectAllSql());
        }
        
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var all = await GetAllAsync();
            return all.Where(predicate.Compile());
        }
        
        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var results = await FindAsync(predicate);
            return results.SingleOrDefault();
        }
        
        public virtual async Task<T> AddAsync(T entity)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            var parameters = EntityToInsertParameters(entity);
            await connection.ExecuteAsync(GetInsertSql(), parameters);
            
            // 如果主键是自增的，获取最后插入的ID
            if (_primaryKeyColumn.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                var lastId = await connection.QuerySingleAsync<long>("SELECT last_insert_rowid()");
                var idProperty = typeof(T).GetProperty(_primaryKeyColumn);
                if (idProperty != null && idProperty.CanWrite)
                {
                    idProperty.SetValue(entity, Convert.ChangeType(lastId, idProperty.PropertyType));
                }
            }
            
            return entity;
        }
        
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            using var transaction = connection.BeginTransaction();
            try
            {
                foreach (var entity in entities)
                {
                    var parameters = EntityToInsertParameters(entity);
                    await connection.ExecuteAsync(GetInsertSql(), parameters, transaction);
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        
        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            var results = await FindAsync(predicate);
            return results.Any();
        }
        
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
            {
                using var connection = new SqliteConnection(_databaseService.GetConnectionString());
                return await connection.QuerySingleAsync<int>(GetCountSql());
            }
            
            var results = await FindAsync(predicate);
            return results.Count();
        }
        
        #endregion
        
        #region 分页查询实现
        
        public virtual (IEnumerable<T> Items, int TotalCount) GetPaged<TKey>(
            int pageIndex, 
            int pageSize, 
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, TKey>> orderBy = null,
            bool ascending = true)
        {
            var query = GetAll();
            
            if (predicate != null)
            {
                query = query.Where(predicate.Compile());
            }
            
            var totalCount = query.Count();
            
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy.Compile()) : query.OrderByDescending(orderBy.Compile());
            }
            
            var items = query.Skip(pageIndex * pageSize).Take(pageSize);
            
            return (items, totalCount);
        }
        
        public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync<TKey>(
            int pageIndex, 
            int pageSize, 
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, TKey>> orderBy = null,
            bool ascending = true)
        {
            var query = await GetAllAsync();
            
            if (predicate != null)
            {
                query = query.Where(predicate.Compile());
            }
            
            var totalCount = query.Count();
            
            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy.Compile()) : query.OrderByDescending(orderBy.Compile());
            }
            
            var items = query.Skip(pageIndex * pageSize).Take(pageSize);
            
            return (items, totalCount);
        }
        
        #endregion
        
        #region 辅助方法
        
        /// <summary>
        /// 执行原生SQL查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        protected IEnumerable<T> ExecuteQuery(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return connection.Query<T>(sql, parameters);
        }
        
        /// <summary>
        /// 异步执行原生SQL查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        protected async Task<IEnumerable<T>> ExecuteQueryAsync(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.QueryAsync<T>(sql, parameters);
        }
        
        /// <summary>
        /// 执行非查询SQL
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        protected int ExecuteNonQuery(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return connection.Execute(sql, parameters);
        }
        
        /// <summary>
        /// 异步执行非查询SQL
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        protected async Task<int> ExecuteNonQueryAsync(string sql, object parameters = null)
        {
            using var connection = new SqliteConnection(_databaseService.GetConnectionString());
            return await connection.ExecuteAsync(sql, parameters);
        }
        
        #endregion
    }
}