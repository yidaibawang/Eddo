using System.Collections.Generic;
using Eddo.Configuration.Startup;
namespace Eddo.Web.Api.Controllers.Runtime.Caching
{
    public class ClearCacheSettingProvider :SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(ClearCacheSettingNames.Password, "123qweasdZXC")
            };
        }
    }
}
