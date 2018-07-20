using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Domain.Entities;
using System.Linq.Expressions;

namespace Eddo.Domain.Repository
{
    public interface IReadableRepository<TEntity>: IReadableRepository<TEntity,int> where TEntity : class, IEntity<int>
    {

    }
    public interface IReadableRepository<TEntity, TKey> where TEntity:class,IEntity<TKey>
    {
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="criteria">查询条件对象</param>
        IQueryable<TEntity> Find(ICriteria<TEntity> criteria);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">查询条件</param>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        TEntity Find(object id);
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        Task<TEntity> FindAsync(object id);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        List<TEntity> FindByIds(params TKey[] ids);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        List<TEntity> FindByIds(IEnumerable<TKey> ids);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">逗号分隔的Id列表，范例："1,2"</param>
        List<TEntity> FindByIds(string ids);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        Task<List<TEntity>> FindByIdsAsync(params TKey[] ids);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">逗号分隔的Id列表，范例："1,2"</param>
        Task<List<TEntity>> FindByIdsAsync(string ids);
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(TKey id);

        /// <summary>
        /// Gets an entity with given primary key or null if not found.
        /// </summary>
        /// <param name="id">Primary key of the entity to get</param>
        /// <returns>Entity or null</returns>
        Task<TEntity> FirstOrDefaultAsync(TKey id);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Gets an entity with given given predicate or null if not found.
        /// </summary>
        /// <param name="predicate">Predicate to filter entities</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">查询条件</param>
        IQueryable<TEntity> FindAll();
        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<IQueryable<TEntity>> FindAllAsync();
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        bool Exists(TKey id);
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        Task<bool> ExistsAsync(TKey id);
        /// <summary>
        /// 获取实体个数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to get all entities based on given <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">A condition to filter entities</param>
        /// <returns>List of all entities</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取实体个数
        /// </summary>
        /// <param name="predicate">查询条件</param>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
      
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        Task<PagerList<TEntity>> PagerQueryAsync(IQueryBase<TEntity> query);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        PagerList<TEntity> PagerQueryAsNoTracking(IQueryBase<TEntity> query);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        Task<PagerList<TEntity>> PagerQueryAsNoTrackingAsync(IQueryBase<TEntity> query);
    }
}
