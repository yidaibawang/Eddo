using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DoSoft.Web;
using Eddo.Modules;
namespace DoSoft.Admin
{
    [DependsOn(typeof(DoSoftWebModule))]
    public class DoSoftAdminModule : EddoModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}