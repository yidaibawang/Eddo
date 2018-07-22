using Eddo.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.WebApi.Config
{
    public static class EddoWebApiConfigurationExtensions
    {
        public static IEddoWebApiModuleConfiguration AbpWebApi(this IModuleConfigurations configurations)
        {
            return configurations.EddoConfiguration.Get<IEddoWebApiModuleConfiguration>();
        }
    }
}
