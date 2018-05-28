using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo
{
    public interface IEddoModuleManager
    {
      
        void InitializeModules();

        void ShutdownModules();
    }
}
