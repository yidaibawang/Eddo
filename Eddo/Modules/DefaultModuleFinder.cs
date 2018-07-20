using Eddo.Modules;
using Eddo.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eddo
{
    public class DefaultModuleFinder:IModuleFinder
    {
        private readonly ITypeFinder _typeFinder;

        public DefaultModuleFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public ICollection<Type> FindAll()
        {
            return _typeFinder.Find(EddoModule.IsEddoModule).ToList();
        }
    }
}
