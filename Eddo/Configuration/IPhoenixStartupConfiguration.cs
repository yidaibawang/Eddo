using Eddo.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Configuration.Startup;
namespace Eddo.Configuration
{
    public interface IEddoStartupConfiguration: IDictionaryBasedConfig
    {
         IIocManager IocManager { get; }
         IModuleConfigurations Modules { get;  }
        string DefaultNameOrConnectionString { get; set; }
        ISettingsConfiguration Settings { get; }
        void ReplaceService(Type type, Action replaceAction);
        T Get<T>();
    }
}
