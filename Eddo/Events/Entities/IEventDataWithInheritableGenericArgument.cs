using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Events.Entities
{
    public  interface IEventDataWithInheritableGenericArgument
    {
        object[] GetConstructorArgs();
    }
}
