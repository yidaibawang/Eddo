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
            //添加自定义映射
            Configuration.Modules.EddoAutoMapper().Configurators.Add(mapper =>
            {
                CustomDtoMapper.CreateMappings(mapper);
            });
        }
        public override void PreInitialize()
        {
            base.PreInitialize();
        }
    }
}
