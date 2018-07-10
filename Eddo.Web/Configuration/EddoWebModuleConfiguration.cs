using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Dependency;
namespace Eddo.Web.Configuration
{
     public  class EddoWebModuleConfiguration : IEddoWebModuleConfiguration,ITransientDependency
    {
        public bool SendAllExceptionsToClients { get; set; }
    }
}
