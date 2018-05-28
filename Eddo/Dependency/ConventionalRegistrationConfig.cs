using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Dependency
{
    public class ConventionalRegistrationConfig
    {
        /// <summary>
        /// 是否注册安装
        /// 默认为是
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// 创建一个注册配置
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}
