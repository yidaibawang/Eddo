using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Castle;
using Castle.Core.Logging;

namespace Eddo.Reflection
{
    public class TypeFinder:ITypeFinder
    {
        public ILogger Logger { get; set; }
        private string assemblySkipLoadingPattern="^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper|^Castle|^ComponentArt|^CppCodeProvider|^DotNetOpenAuth|^EntityFramework|^EPPlus|^FluentValidation|^ImageResizer|^itextsharp|^log4net|^MaxMind|^MbUnit|^MiniProfiler|^Mono.Math|^MvcContrib|^Newtonsoft|^NHibernate|^nunit|^PerlRegex|^QuickGraph|^Recaptcha|^Remotion|^RestSharp|^Rhino|^Telerik|^Iesi|^UserAgentStringLibrary|^VJSharpCodeProvider|^WebActivator|^WebDev|^WebGrease";
        private IAssemblyFinder AssemblyFinder;
        private readonly object _syncObj = new object();
        private Type[] _types;

        public TypeFinder(IAssemblyFinder assemblyFinder)
        {
            AssemblyFinder = assemblyFinder;
            Logger = NullLogger.Instance;

        }
        public virtual bool Match(AssemblyName assemblyName)
        {
            return !Regex.IsMatch(assemblyName.FullName, assemblySkipLoadingPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    
        public Type[] Find(Func<Type, bool> predicate)
        {
            return  GetAllTypes().Where(predicate).ToArray();
        }

        public Type[] FindAll()
        {
            if (_types == null)
            {
                lock (_syncObj)
                {
                    if (_types == null)
                    {
                        _types = GetAllTypes().ToArray();
                    }
                }
            }
            return _types;
        }
        private List<Type> GetAllTypes()
        {
            var allTypes = new List<Type>();

            foreach (var assembly in AssemblyFinder.GetAllAssemblies().Distinct())
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly==null||typesInThisAssembly.Count()<0)
                    {
                        continue;
                    }
                    if (Match(assembly.GetName()))
                    {
                        allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                    }
                }
                catch (Exception ex)
                {
                     Logger.Warn(ex.ToString(), ex);
                    //throw new Exception(ex.Message);
                }
            }

            return allTypes;
        }
    }
}
