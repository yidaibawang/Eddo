using Castle.Facilities.Logging;
using Eddo.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DoSoft.Web
{
    public class MvcApplication : EddoWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            EddoBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net()
                   .WithConfig("log4net.config")
                );

            base.Application_Start(sender, e);
        }
    }
}
