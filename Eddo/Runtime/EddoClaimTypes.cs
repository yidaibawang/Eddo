using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Runtime
{
     public static class EddoClaimTypes
    {
        public const string TenantId = "http://www.dosoft.com/identity/claims/tenantId";

        /// <summary>
        /// ImpersonatorUserId.
        /// </summary>
        public const string ImpersonatorUserId = "http://www.dosoft.com/identity/claims/impersonatorUserId";

        /// <summary>
        /// ImpersonatorTenantId
        /// </summary>
        public const string ImpersonatorTenantId = "http://www.dosoft.com/identity/claims/impersonatorTenantId";
    }
}
