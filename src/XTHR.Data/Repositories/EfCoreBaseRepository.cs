using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using XTHR.Common.Entities;
using XTHR.Core.Interfaces.Repositories;
using XTHR.Data.Context;

namespace XTHR.Data.Repositories
{
    public class EfCoreBaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EfCoreBaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        #region Query Operations

        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.FirstOrDefaultAsync()
                : await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null
                ? _dbSet.AsQueryable()
                : _dbSet.Where(predicate).AsQueryable();
        }

        public virtual async Task<List<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null, bool ascending = true)
        {
            IQueryable<TEntity> query = predicate == null ? _dbSet : _dbSet.Where(predicate);

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetPagedAsync<TOrderBy>(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, Expression<Func<TEntity, TOrderBy>>? orderBy = null, bool ascending = true)
        {
            IQueryable<TEntity> query = _dbSet.Where(predicate);

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        }

        #endregion

        #region Count and Exists Operations

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            return predicate == null
                ? await _dbSet.AnyAsync()
                : await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            return await _dbSet.AnyAsync(e => e.Id!.Equals(id));
        }

        #endregion

        #region Add Operations

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        #endregion

        #region Update Operations

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>>? predicate, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            // EF Core 7+ supports ExecuteUpdateAsync for bulk updates
            // For older versions, entities need to be loaded and updated
            if (predicate == null)
            {
                return await _dbSet.ExecuteUpdateAsync(updateExpression);
            }
            else
            {
                return await _dbSet.Where(predicate).ExecuteUpdateAsync(updateExpression);
            }
        }

        #endregion

        #region Delete Operations

        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            // EF Core 7+ supports ExecuteDeleteAsync for bulk deletes
            // For older versions, entities need to be loaded and deleted
            if (predicate == null)
            {
                return await _dbSet.ExecuteDeleteAsync();
            }
            else
            {
                return await _dbSet.Where(predicate).ExecuteDeleteAsync();
            }
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TKey> ids)
        {
            var entitiesToDelete = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
            _dbSet.RemoveRange(entitiesToDelete);
            return await _context.SaveChangesAsync();
        }

        #endregion

        #region Soft Delete Operations

        public virtual async Task<bool> SoftDeleteAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            if (entity is ISoftDelete softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = true;
                _dbSet.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            throw new NotSupportedException("Entity does not support soft delete.");
        }

        public virtual async Task<int> SoftDeleteRangeAsync(IEnumerable<TKey> ids)
        {
            var entitiesToSoftDelete = await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
            int count = 0;
            foreach (var entity in entitiesToSoftDelete)
            {
                if (entity is ISoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsDeleted = true;
                    _dbSet.Update(entity);
                    count++;
                }
            }
            if (count > 0)
            {
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public virtual async Task<bool> RestoreAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            if (entity is ISoftDelete softDeleteEntity)
            {
                softDeleteEntity.IsDeleted = false;
                _dbSet.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            throw new NotSupportedException("Entity does not support soft delete.");
        }

        #endregion

        #region Transaction Operations

        public virtual async Task<IDbTransaction> BeginTransactionAsync()
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            return new EfCoreDbTransaction(transaction);
        }

        public virtual async Task CommitTransactionAsync(IDbTransaction transaction)
        {
            if (transaction is EfCoreDbTransaction efCoreTransaction)
            {
                await efCoreTransaction.CommitAsync();
            }
            else
            {
                throw new ArgumentException("Invalid transaction type.", nameof(transaction));
            }
        }

        public virtual async Task RollbackTransactionAsync(IDbTransaction transaction)
        {
            if (transaction is EfCoreDbTransaction efCoreTransaction)
            {
                await efCoreTransaction.RollbackAsync();
            }
            else
            {
                throw new ArgumentException("Invalid transaction type.", nameof(transaction));
            }
        }

        #endregion

        #region Cache Operations

        public virtual Task ClearCacheAsync(string? key = null)
        {
            // EF Core does not have a built-in cache that needs explicit clearing at this level.
            // Caching would typically be handled by a higher-level caching layer (e.g., Redis, MemoryCache).
            return Task.CompletedTask;
        }

        public virtual Task RefreshCacheAsync(string? key = null)
        {
            // Similar to ClearCacheAsync, this would be handled by a higher-level caching layer.
            return Task.CompletedTask;
        }

        #endregion
    }

    // Helper class to wrap EF Core's IDbContextTransaction into IDisposable and IDbTransaction
    public class EfCoreDbTransaction : IDbTransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;
        private TransactionStatus _status;

        public EfCoreDbTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction ?? throw new ArgumentNullException(nameof(dbContextTransaction));
            _status = TransactionStatus.Active;
            TransactionId = Guid.NewGuid().ToString(); // Generate a unique ID for this transaction
        }

        public async Task CommitAsync()
        {
            await _dbContextTransaction.CommitAsync();
            _status = TransactionStatus.Committed;
        }

        public async Task RollbackAsync()
        {
            await _dbContextTransaction.RollbackAsync();
            _status = TransactionStatus.RolledBack;
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
            _status = TransactionStatus.Disposed;
        }

        public string TransactionId { get; }

        public TransactionStatus Status => _status;
    }
}