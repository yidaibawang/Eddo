using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Configuration
{
     public  class EddoWebModuleConfiguration : IEddoWebModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }
    }
}
