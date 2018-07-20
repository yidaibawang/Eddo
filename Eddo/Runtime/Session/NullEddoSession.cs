using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Runtime.Session
{
     public  class NullEddoSession:EddoSessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullEddoSession Instance { get; } = new NullEddoSession();

        /// <inheritdoc/>
        public override long? UserId => null;

        /// <inheritdoc/>
        public override int? TenantId => null;


        public override long? ImpersonatorUserId => null;

        public override int? ImpersonatorTenantId => null;
        
    }
}
