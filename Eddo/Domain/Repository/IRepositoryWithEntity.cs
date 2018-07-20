using Eddo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eddo.Domain.Repository
{
    public  interface IRepository<TEntity>: IRepository<TEntity, int>, IReadableRepository<TEntity> where TEntity : class, IEntity<int>
    {

    }
    public interface IRepository<TEntity, TKey> : IRepository, IReadableRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        TEntity Add(TEntity entity);
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Add(IEnumerable<TEntity> entities);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void AddAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 插入返回id
        /// </summary>
        /// <param name="entities">实体</param>
        TKey AddGetId(TEntity entity);
        /// <summary>
        /// 同步插入返回id
        /// </summary>
        /// <param name="TEntity">实体</param>
        Task<TKey> AddAsyncGetId(TEntity entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        TEntity Update(TEntity entity);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        void Remove(TKey id);
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        Task RemoveAsync(TKey id);
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove(TEntity entity);
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task RemoveAsync(TEntity entity);
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        void Remove(IEnumerable<TKey> ids);
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="ids">实体编号集合</param>
        Task RemoveAsync(IEnumerable<TKey> ids);
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Remove(IEnumerable<TEntity> entities);
        /// <summary>
        /// 移除实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task RemoveAsync(IEnumerable<TEntity> entities);
        void Remove(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        ///
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }
    }
}
