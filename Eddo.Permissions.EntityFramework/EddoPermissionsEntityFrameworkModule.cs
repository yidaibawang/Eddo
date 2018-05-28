using Eddo.Modules;
using Eddo.EntityFramework;
using System.Reflection;

namespace Eddo.Permissions.EntityFramework
{
    [DependsOn(typeof(EddoPermissionsModule),typeof(EddoEntityFrameworkModule))]
    public class EddoPermissionsEntityFrameworkModule:EddoModule
    {
        public override void PreInitialize()
        {

        }
        /// <summary>
        /// 安装处理函数
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

    }
}
