using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Modules;
using System.Reflection;
using Eddo;
using Eddo.Permissions;

namespace Web.Core
{   
    [DependsOn(typeof(EddoPermissionsModule))]
    public class WebCoreModule:EddoModule
    {
        /// <summary>
        /// 安装处理函数
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
