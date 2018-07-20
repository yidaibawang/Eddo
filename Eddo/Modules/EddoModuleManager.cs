using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Eddo.Dependency;
using Castle.Core.Logging;
using Eddo.Configuration;

namespace Eddo.Modules
{
    /// <summary>
    ///  模块管理
    /// </summary>
    public class EddoModuleManager:IEddoModuleManager
    {
        public ILogger Logger { get; set; }

        private readonly EddoModuleCollection _modules;

        private readonly IIocManager _iocManager;
        private readonly IModuleFinder _moduleFinder;

        public EddoModuleManager(IIocManager iocManager, IModuleFinder moduleFinder)
        {
            _modules = new EddoModuleCollection();
            _iocManager = iocManager;
            _moduleFinder = moduleFinder;
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// 安装模块
        /// </summary>
        public virtual void InitializeModules()
        {
            LoadAll();

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());
        }

        private void LoadAll()
        {
            Logger.Debug("加载 EddoModule...");

            var moduleTypes = AddMissingDependedModules(_moduleFinder.FindAll());
            Logger.Debug("发现 " + moduleTypes.Count + " EddoModule");

            //注册ioc容器
            foreach (var moduleType in moduleTypes)
            {
                if (!EddoModule.IsEddoModule(moduleType))
                {
                    throw new Exception("此类型不是EddoModule类型: " + moduleType.AssemblyQualifiedName);
                }

                if (!_iocManager.IsRegistered(moduleType))
                {
                    _iocManager.Register(moduleType);
                }
            }

            //添加模块
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = (EddoModule)_iocManager.Resolve(moduleType);

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IEddoStartupConfiguration>();

                _modules.Add(new EddoModuleInfo(moduleObject));

                Logger.DebugFormat("加载模块: " + moduleType.AssemblyQualifiedName);
            }

            EnsureKernelModuleToBeFirst();

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private void EnsureKernelModuleToBeFirst()
        {
            var kernelModuleIndex = _modules.FindIndex(m => m.Type == typeof(EddoKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = _modules[kernelModuleIndex];
                _modules.RemoveAt(kernelModuleIndex);
                _modules.Insert(0, kernelModule);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                //Set dependencies according to assembly dependency
                foreach (var referencedAssemblyName in moduleInfo.Assembly.GetReferencedAssemblies())
                {
                    var referencedAssembly = Assembly.Load(referencedAssemblyName);
                    var dependedModuleList = _modules.Where(m => m.Assembly == referencedAssembly).ToList();
                    if (dependedModuleList.Count > 0)
                    {
                        moduleInfo.Dependencies.AddRange(dependedModuleList);
                    }
                }

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in EddoModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new Exception("没有发现  depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }

        private static ICollection<Type> AddMissingDependedModules(ICollection<Type> allModules)
        {
            var initialModules = allModules.ToList();
            foreach (var module in initialModules)
            {
                FillDependedModules(module, allModules);
            }

            return allModules;
        }
        /// <summary>
        /// 查找依赖项模块
        /// </summary>
        /// <param name="module"></param>
        /// <param name="allModules"></param>
        private static void FillDependedModules(Type module, ICollection<Type> allModules)
        {
            foreach (var dependedModule in EddoModule.FindDependedModuleTypes(module))
            {
                if (!allModules.Contains(dependedModule))
                {
                    allModules.Add(dependedModule);
                    FillDependedModules(dependedModule, allModules);
                }
            }
        }
    }
}
