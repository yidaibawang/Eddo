using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Configuration
{
    public interface IEddoWebModuleConfiguration
    {
        bool SendAllExceptionsToClients { get; set; }
    }
}
