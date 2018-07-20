using Eddo.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Applications.Services
{
    public abstract class ApplicationService: EddoServiceBase,IApplicationService
    {
        public IEddoSession EddoSession { get; set; }
        public ApplicationService()
        {
            EddoSession = NullEddoSession.Instance;
        }
    }
}
