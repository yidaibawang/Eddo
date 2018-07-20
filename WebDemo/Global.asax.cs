using System;
using Eddo.Web;
using Castle.Core;
using Castle.Facilities.Logging;

namespace WebDemo
{
    public class MvcApplication :EddoWebApplication
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
