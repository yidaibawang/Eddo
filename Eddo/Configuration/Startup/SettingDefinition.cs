using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Configuration.Startup
{
    public class SettingDefinition
    {
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the setting.
        /// This can be used to show setting to the user.
        /// </summary>


        /// <summary>
        /// Is this setting inherited from parent scopes.
        /// Default: True.
        /// </summary>
        public bool IsInherited { get; set; }

        /// <summary>
        /// Gets/sets group for this setting.
        /// </summary>


        /// <summary>
        /// Default value of the setting.
        /// </summary>
        public string DefaultValue { get; private set; }

        /// <summary>
        /// Can clients see this setting and it's value.
        /// It maybe dangerous for some settings to be visible to clients (such as email server password).
        /// Default: false.
        /// </summary>
        public bool IsVisibleToClients { get; private set; }

        /// <summary>
        /// Can be used to store a custom object related to this setting.
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Creates a new <see cref="SettingDefinition"/> object.
        /// </summary>
        /// <param name="name">Unique name of the setting</param>
        /// <param name="defaultValue">Default value of the setting</param>
        /// <param name="displayName">Display name of the permission</param>
        /// <param name="group">Group of this setting</param>
        /// <param name="description">A brief description for this setting</param>
        /// <param name="scopes">Scopes of this setting. Default value: <see cref="SettingScopes.Application"/>.</param>
        /// <param name="isVisibleToClients">Can clients see this setting and it's value. Default: false</param>
        /// <param name="isInherited">Is this setting inherited from parent scopes. Default: True.</param>
        /// <param name="customData">Can be used to store a custom object related to this setting</param>
        public SettingDefinition(
            string name,
            string defaultValue,

            bool isVisibleToClients = false,
            bool isInherited = true,
            object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            DefaultValue = defaultValue;
           
            IsVisibleToClients = isVisibleToClients;
            IsInherited = isInherited;
            CustomData = customData;
        }
    }
}
