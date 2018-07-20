using System.Reflection;

namespace Eddo.Dependency
{
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        ///获取程序集
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// 获取ioc注册管理
        /// </summary>
        IIocManager IocManager { get; }

        /// <summary>
        /// 获取注册配置
        /// </summary>
        ConventionalRegistrationConfig Config { get; }
    }
}
