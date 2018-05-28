using Eddo.Dependency;
using System;

namespace Eddo.Configuration.Startup
{
    public static class EddoStartupConfigurationExtensions
    {
        /// <summary>
        /// 替换服务类型
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="type">Type.</param>
        /// <param name="impl">Implementation.</param>
        /// <param name="lifeStyle">Life style.</param>
        public static void ReplaceService(this IEddoStartupConfiguration configuration, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            configuration.ReplaceService(type, () =>
            {
                configuration.IocManager.Register(type, impl, lifeStyle);
            });
        }

        /// <summary>
        ///替换服务类型
        /// </summary>
        /// <typeparam name="TType">Type of the service.</typeparam>
        /// <typeparam name="TImpl">Type of the implementation.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="lifeStyle">Life style.</param>
        public static void ReplaceService<TType, TImpl>(this IEddoStartupConfiguration configuration, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            configuration.ReplaceService(typeof(TType), () =>
            {
                configuration.IocManager.Register<TType, TImpl>(lifeStyle);
            });
        }
        /// <summary>
        /// 替换服务类型
        /// </summary>
        /// <typeparam name="TType">Type of the service.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="replaceAction">Replace action.</param>
        public static void ReplaceService<TType>(this IEddoStartupConfiguration configuration, Action replaceAction)
            where TType : class
        {
            configuration.ReplaceService(typeof(TType), replaceAction);
        }
    }
}
