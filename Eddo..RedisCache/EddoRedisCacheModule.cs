using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Modules;
using Eddo.Reflection;
using Eddo.Dependency;
using Castle.MicroKernel.Registration;

namespace Eddo.RedisCache
{    
    [DependsOn(typeof(EddoKernelModule))]
    public class EddoRedisCacheModule:EddoModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<EddoRedisCacheOptions>();
            IocManager.RegisterIfNot<RedisMemoryCache>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EddoRedisCacheModule).GetAssembly());
        }

    }
}
