using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Configuration
{
    public   class ModuleConfigurations:IModuleConfigurations
    {
        public IEddoStartupConfiguration EddoConfiguration { get; private set; }

        public ModuleConfigurations(IEddoStartupConfiguration eddoConfiguration)
        {
            EddoConfiguration = eddoConfiguration;
        }
    }
}
