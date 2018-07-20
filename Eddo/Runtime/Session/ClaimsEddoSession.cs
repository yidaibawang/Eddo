using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Dependency;
using System.Security.Claims;

namespace Eddo.Runtime.Session
{
    public class ClaimsEddoSession:EddoSessionBase,ISingletonDependency
    {
        public override long? UserId
        {
            get
            {
                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public override int? TenantId
        {
            get
            {
       
                var tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == EddoClaimTypes.TenantId);
                if (!string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return Convert.ToInt32(tenantIdClaim.Value);
                }

            

                return null;
            }
        }

        public override long? ImpersonatorUserId
        {
            get
            {
                var impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == EddoClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        public override int? ImpersonatorTenantId
        {
            get
            {
            
                var impersonatorTenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == EddoClaimTypes.ImpersonatorTenantId);
                if (string.IsNullOrEmpty(impersonatorTenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        protected IPrincipalAccessor PrincipalAccessor { get; }


        public ClaimsEddoSession(
            IPrincipalAccessor principalAccessor)
        {
       
            PrincipalAccessor = principalAccessor;
        }
    }
}
