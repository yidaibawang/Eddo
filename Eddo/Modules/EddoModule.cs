using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Eddo.Collections.Extensions;
using Eddo.Dependency;
using Castle.Core.Logging;
using Eddo.Configuration;

namespace Eddo.Modules
{
    public abstract class EddoModule
    {
        public IIocManager IocManager { get; internal set; }
        public ILogger Logger { get; set; }
        public IEddoStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        ///安装前处理函数
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// 安装处理函数
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 安装后函数
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Shutdown()
        {

        }
        /// <summary>
        /// 判断是否为EddoModule模块类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEddoModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(EddoModule).IsAssignableFrom(type);
        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }
        /// <summary>
        ///查找依赖项模块
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsEddoModule(moduleType))
            {
                throw new Exception("This type is not an Eddo module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            list.AddIfNotContains(typeof(EddoKernelModule));
            return list;
        }
        //添加路由映射信息
        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsEddoModule(module))
            {
                throw new Exception("此类型不是EddoModule类型： " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }


    }
}
