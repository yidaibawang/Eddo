using System;
using System.Collections.Generic;
namespace Eddo.Caching
{
    public interface ICacheManager
    {
        ICache GetCacher(string name);
        ICache GetCacher(Type type);
        ICache GetCacher<T>();
       IReadOnlyList<ICache> GetAllCaches();
    }
}
