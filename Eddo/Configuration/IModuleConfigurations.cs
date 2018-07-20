using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Configuration
{
    public  interface IModuleConfigurations
    {
        IEddoStartupConfiguration EddoConfiguration { get; }
    }
}
