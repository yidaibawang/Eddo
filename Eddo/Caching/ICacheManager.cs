using System;

namespace Eddo.Caching
{
    public interface ICacheManager
    {
        ICache GetCacher(string name);
        ICache GetCacher(Type type);
        ICache GetCacher<T>();
    }
}
