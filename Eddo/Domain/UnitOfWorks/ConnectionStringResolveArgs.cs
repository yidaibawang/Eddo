using Eddo.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Domain.UnitOfWorks
{
    public class ConnectionStringResolveArgs : Dictionary<string, object>
    {
        public MultiTenancySides? MultiTenancySide { get; set; }

        public ConnectionStringResolveArgs(MultiTenancySides? multiTenancySide = null)
        {
            MultiTenancySide = multiTenancySide;
        }
    }
}
