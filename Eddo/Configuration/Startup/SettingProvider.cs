using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Dependency;
namespace Eddo.Configuration.Startup
{
    public abstract class SettingProvider : ITransientDependency
    {
        /// <summary>
        /// Gets all setting definitions provided by this provider.
        /// </summary>
        /// <returns>List of settings</returns>
        public abstract IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context);
    }
}
