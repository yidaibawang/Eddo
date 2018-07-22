using Eddo.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Configuration
{
    public static class EddoWebApiConfigurationExtensions
    {
        public static IEddoWebApiModuleConfiguration EddoWebApi(this IModuleConfigurations configurations)
        {
            return configurations.EddoConfiguration.GetOrCreate("Modules.Eddo.Web.Api", () => configurations.EddoConfiguration.IocManager.Resolve<IEddoWebApiModuleConfiguration>());
        }
    }
}
