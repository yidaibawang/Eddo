using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Modules;
using Eddo;
using Eddo.EntityFramework;
using Web.Core;
using System.Reflection;
using Eddo.Permissions.EntityFramework;
using System.Data.Entity;

namespace web.ef
{   
    [DependsOn(typeof(EddoPermissionsEntityFrameworkModule),typeof(WebCoreModule))]
     public class WebDataModule:EddoModule
    {
        /// <summary>
        ///安装前处理函数
        /// </summary>
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<WebDBContext>());

            //web.config (or app.config for non-web projects) file should contain a connection string named "Default".
            Configuration.DefaultNameOrConnectionString = "Default";
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
