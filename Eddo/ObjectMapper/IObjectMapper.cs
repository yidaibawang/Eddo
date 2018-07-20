using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.ObjectMapper
{
    public interface IObjectMapper
    {
        /// <summary>
        /// 转换数据源
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination object</typeparam>
        /// <param name="source">Source object</param>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// 执行数据对象转换
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns>Returns the same <see cref="destination"/> object after mapping operation</returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
