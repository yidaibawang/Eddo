using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Dependency
{
    public class ConventionalRegistrationContext:IConventionalRegistrationContext
    {
        /// <summary>
        /// 获取
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        ///Ioc管理器
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// 注册配置
        /// </summary>
        public ConventionalRegistrationConfig Config { get; private set; }

        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager, ConventionalRegistrationConfig config)
        {
            Assembly = assembly;
            IocManager = iocManager;
            Config = config;
        }
    }
}
