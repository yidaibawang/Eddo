using DoSoft.Core;
using Eddo.AutoMapper;
using Eddo.Modules;
using System.Reflection;

namespace DoSoft.Application
{
    [DependsOn(typeof(DoSoftCoreModule), typeof(EddoAutoMapperModule))]
    public class DoSoftApplicationModule:EddoModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        public override void PreInitialize()
        {
            base.PreInitialize();
        }
    }
}
