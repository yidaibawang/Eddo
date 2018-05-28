using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Repository
{
    public interface IQueryBase<TEntity>:ICriteria<TEntity> where TEntity:class
    {
        /// <summary>
        /// 获取排序条件
        /// </summary>
        string GetOrder();
        /// <summary>
        /// 获取分页参数
        /// </summary>
        IPager GetPager();
    }
}
