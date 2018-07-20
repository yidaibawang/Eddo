using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.MultiTenancy
{
    public class MultiTenancySideAttribute : Attribute
    {
        /// <summary>
        /// Multitenancy side.
        /// </summary>
        public MultiTenancySides Side { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTenancySideAttribute"/> class.
        /// </summary>
        /// <param name="side">Multitenancy side.</param>
        public MultiTenancySideAttribute(MultiTenancySides side)
        {
            Side = side;
        }
    }
}
