using Eddo.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Caching
{
    public abstract class CacheProviderBase:ICacheProvider
    {
        protected readonly IIocManager IocManager;
        protected CacheProviderBase(IIocManager iocManager)
        {
            IocManager = iocManager;
        }
        public abstract ICache GetCach(string name);
    }
}
