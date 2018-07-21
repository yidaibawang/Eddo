using Eddo.Configuration.Startup;
using Eddo.Dependency;
using System;
using System.Collections.Generic;

namespace Eddo.Configuration
{
    public class EddoStartupConfiguration: DictionaryBasedConfig,IEddoStartupConfiguration
    {
        public IIocManager IocManager { get; private set; }
        public IModuleConfigurations Modules { get; private set; }

        public string DefaultNameOrConnectionString { get; set; }
        public Dictionary<Type, Action> ServiceReplaceActions { get; private set; }

        public ISettingsConfiguration Settings { get; private set; }

        public EddoStartupConfiguration(IIocManager iocManager)
        {
            IocManager = iocManager;
         
        }
        public void Initialize()
        {
            Modules = IocManager.Resolve<IModuleConfigurations>();
            ServiceReplaceActions = new Dictionary<Type, Action>();
            Settings = IocManager.Resolve<ISettingsConfiguration>();
        }
        public void ReplaceService(Type type, Action replaceAction)
        {
            ServiceReplaceActions[type] = replaceAction;
        }

        public T Get<T>()
        {
            return GetOrCreate(typeof(T).FullName, () => IocManager.Resolve<T>());
        }
    }
}
