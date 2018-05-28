using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Caching
{
     public interface ICacheProvider
    {   
        /// <summary>
        /// 获取缓存提供处理对象
        /// </summary>
        /// <param name="name">缓存区名称</param>
        /// <returns></returns>
                 
        ICache GetCach(string name);
    }
}
