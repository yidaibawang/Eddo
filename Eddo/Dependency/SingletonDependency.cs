using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Dependency
{
    public static class SingletonDependency<T>
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static T Instance => LazyInstance.Value;
        private static readonly Lazy<T> LazyInstance;

        static SingletonDependency()
        {
            LazyInstance = new Lazy<T>(() => IocManager.Instance.Resolve<T>(), true);
        }
    }
}
