using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Modules;
using System.Reflection;

namespace Eddo.Permissions
{   
    [DependsOn(typeof(EddoKernelModule))]
    public  class EddoPermissionsModule:EddoModule
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
