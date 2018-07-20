using Eddo.Modules;
using Eddo.Permissions;
using System.Reflection;

namespace DoSoft.Core
{
    [DependsOn(typeof(EddoPermissionsModule))]
    public class DoSoftCoreModule:EddoModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
