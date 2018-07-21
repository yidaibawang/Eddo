using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Collections;
namespace Eddo.Configuration.Startup
{
    internal class SettingsConfiguration : ISettingsConfiguration
    {
        public ITypeList<SettingProvider> Providers { get; private set; }

        public SettingsConfiguration()
        {
            Providers = new TypeList<SettingProvider>();
        }
    }
}
