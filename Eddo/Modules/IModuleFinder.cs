using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo
{
     public interface IModuleFinder
    {
        /// <summary>
        /// Finds all modules.
        /// </summary>
        /// <returns>List of modules</returns>
        ICollection<Type> FindAll();
    }
}
