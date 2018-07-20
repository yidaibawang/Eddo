using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Authorization
{
    public interface IEddoAuthorizeAttribute
    {
        string[] Permissions { get; }

 
        bool RequireAllPermissions { get; set; }
    }
}
