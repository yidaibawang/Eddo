using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eddo.Domain.Entities;
using Eddo.Utils;
using Eddo.Collections.Extensions;

namespace Eddo.Domain.Repository
{
    public abstract class EddoRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {

        public abstract TEntity Add(TEntity entity);
        public void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            foreach (TEntity Entity in entities)
            {
                Add(Entity);
            }
        }
        public abstract Task<TEntity> AddAsync(TEntity entity);

        public void AddAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            foreach (TEntity Entity in entities)
            {
                AddAsync(Entity);
            }
        }
        /// <summary>
        /// 增加返回编号
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TKey AddGetId(TEntity entity)
        {
            return Add(entity).Id;
        }
        /// <summary>
        ///  同步增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<TKey> AddAsyncGetId(TEntity entity)
        {
            return Task.FromResult(Add(entity).Id);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract TEntity Update(TEntity entity);
        /// <summary>
        /// 同步更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Remove(TEntity entity);

        public void Remove(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            foreach (TEntity entity in entities)
            {
                Remove(entity);
            }
        }
        public   Task RemoveAsync(TEntity entity)
        {
            Remove(entity);
            return Task.FromResult(0);
        }

        public async Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            foreach (TEntity entity in entities)
            {
                await RemoveAsync(entity);
            }
        }


        public abstract void Remove(TKey id);
        public  Task RemoveAsync(TKey id)
        {
            Remove(id);
            return Task.FromResult(0);
        }

        public void Remove(IEnumerable<TKey> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }
            foreach (TKey id in ids)
            {
                Remove(id);
            }
        }
        public  Task RemoveAsync(IEnumerable<TKey> ids)
        {
            Remove(ids);
            return Task.FromResult(0);
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            var ids = FindAll().Where(predicate);
            foreach (TEntity id in ids)
            {
                Remove(id);
            }
        }

        public  Task RemoveAsync(Expression<Func<TEntity, bool>> predicate)
        {
              Remove(predicate);
              return Task.FromResult(0);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(TKey id)
        {
            Expression<Func<TEntity, bool>> predicate = null;
            if (id == null)
                return false;
            predicate = t => t.Id.Equals(id);

            return Exists(predicate);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return false;
            return FindAll().Any(predicate);
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            if (id == null)
                return false;
            return await ExistsAsync(t => t.Id.Equals(id));
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return false;
            return FindAll().Any(predicate);
        }

        public TEntity Find(TKey id)
        {
            return FindAll().Single(t => t.Id.Equals(id));
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                return FindAll();
            }
            return FindAll().Where<TEntity>(predicate);
        }

        public IQueryable<TEntity> Find(ICriteria<TEntity> criteria)
        {
            if (criteria == null)
            {
                return FindAll();
            }
            return FindAll().Where<TEntity>(criteria.getpredicate());
        }
    
        public virtual Task<TEntity> FindAsync(TKey id)
        {
            return Task.FromResult(FirstOrDefault(t => t.Id.Equals(id)));
        }

        public List<TEntity> FindByIds(string ids)
        {
            if (ids == null)
                return null;
            return FindAll().Where(t => ids.Contains(t.Id.ToString())).ToList();
        }

        public List<TEntity> FindByIds(IEnumerable<TKey> ids)
        {
            return FindAll().Where(Item => ids.Contains(Item.Id)).ToList();
        }

        public List<TEntity> FindByIds(params TKey[] ids)
        {
            return FindByIds((IEnumerable<TKey>)ids);
        }

  

        public async Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids)
        {
            return FindByIds(ids);
        }


        public async Task<List<TEntity>> FindByIdsAsync(params TKey[] ids)
        {
            return FindByIds((IEnumerable<TKey>)ids);
        }
        public async Task<List<TEntity>> FindByIdsAsync(string ids)
        {
            var idList = ConvertHelper.ToList<TKey>(ids).ToArray();
            return FindByIds(idList);
        }
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return FindAll().Where(predicate).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await Task.FromResult<int>(Count(predicate));
        }

        public abstract IQueryable<TEntity> FindAll();

   
        public async Task<IQueryable<TEntity>> FindAllAsync()
        {
           return await Task.FromResult(FindAll());
        }

       
        public PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query)
        {
            var order = query.GetOrder() ;
            if (string.IsNullOrWhiteSpace(order))
                order = "Id";
            return FindAll().Where(query).OrderBy(order).ToPagerList(query.GetPager());

        }

        public PagerList<TEntity> PagerQueryAsNoTracking(IQueryBase<TEntity> query)
        {
            var order = query.GetOrder();
            if (string.IsNullOrWhiteSpace(order))
                order = "Id";
            return TableNoTracking.Where(query).OrderBy(order).ToPagerList(query.GetPager());
        }

        public async Task<PagerList<TEntity>> PagerQueryAsNoTrackingAsync(IQueryBase<TEntity> query)
        {
            var order = query.GetOrder();
            if (string.IsNullOrWhiteSpace(order))
                order = "Id";
            return TableNoTracking.Where(query).OrderBy(order).ToPagerList(query.GetPager());
        }

        public async Task<PagerList<TEntity>> PagerQueryAsync(IQueryBase<TEntity> query)
        {
            return PagerQuery(query);
        }

        public virtual TEntity FirstOrDefault(TKey id)
        {
            return FindAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(TKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {   
            return FindAll().FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }


        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return FirstOrDefault(predicate);
        }
     

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Single(predicate);
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return FindAll().ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().Where(predicate).ToList();
        }
        /// <summary>
        ///
        /// </summary>
        public abstract IQueryable<TEntity> TableNoTracking { get; }

    }
}
