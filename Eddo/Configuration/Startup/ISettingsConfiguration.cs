using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Collections;
namespace Eddo.Configuration.Startup
{
    public interface ISettingsConfiguration
    {
        ITypeList<SettingProvider> Providers
        {
            get;
        }
    }
}
