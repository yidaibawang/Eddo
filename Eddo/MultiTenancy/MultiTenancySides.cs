using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.MultiTenancy
{
    [Flags]
    public enum MultiTenancySides
    {
        /// <summary>
        /// Tenant side.
        /// </summary>
        Tenant = 1,

        /// <summary>
        /// Host (tenancy owner) side.
        /// </summary>
        Host = 2
    }
}
