using Eddo.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Collections.Extensions
{
     public static class QueryableExtensions
    {
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="criteria">查询条件对象</param>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, ICriteria<TEntity> criteria) where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (criteria == null)
                throw new ArgumentNullException(nameof(criteria));
            var predicate = criteria.getpredicate();
            if (predicate == null)
                return source;
            return source.Where(predicate);
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="condition">该值为true时添加查询条件，否则忽略</param>
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (condition == false)
                return source;
            return source.Where(predicate);
        }
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        public static IQueryable<TEntity> PageBy<TEntity>(this IQueryable<TEntity> source, IPager pager)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pager == null)
                throw new ArgumentNullException(nameof(pager));
            if (pager.TotalCount <= 0)
                pager.TotalCount = source.Count();
            return source.Skip(pager.GetSkipCount()).Take(pager.PageSize);
        }

        /// <summary>
        /// 转换为分页列表
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pager">分页对象</param>
        public static PagerList<TEntity> ToPagerList<TEntity>(this IQueryable<TEntity> source, IPager pager)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (pager == null)
                throw new ArgumentNullException(nameof(pager));
            var result = new PagerList<TEntity>(pager);
            result.AddRange(source.ToList());
            return result;
        }
    }
}
