using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Dependency
{
    public class EngineContext
    {

        public EngineContext()
        {

        }
        public static IIocManager Initialize(bool forceRecreate)
        {
            if (Singleton<IIocManager>.Instance == null || forceRecreate)
            {
                Singleton<IIocManager>.Instance = new IocManager();
            }
            return Singleton<IIocManager>.Instance;
        }
        public static IIocManager Current
        {
            get
            {
                if (Singleton<IIocManager>.Instance == null)
                {
                    Initialize(false);
                }
                return IocManager.Instance;
            }
        }
    }
}
