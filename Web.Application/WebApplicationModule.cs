using Eddo.Modules;
using System.Reflection;
using Web.Core;
using Eddo.AutoMapper;

namespace Web.Application
{
    [DependsOn(typeof(WebCoreModule), typeof(EddoAutoMapperModule))]
    public class WebApplicationModule:EddoModule
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
