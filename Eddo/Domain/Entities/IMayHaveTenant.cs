using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.Entities
{
    public  interface IMayHaveTenant
    {
        int? TenantId { get; set; }
    }
}
