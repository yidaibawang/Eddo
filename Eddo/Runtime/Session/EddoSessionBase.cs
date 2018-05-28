using System;

namespace Eddo.Runtime.Session
{
    public abstract class EddoSessionBase : IEddoSession
    {

        public abstract long? UserId { get; }

        public abstract int? TenantId { get; }

        public abstract long? ImpersonatorUserId { get; }

        public abstract int? ImpersonatorTenantId { get; }

        public IDisposable Use(int? tenantId, long? userId)
        {
            throw new NotImplementedException();
        }
    }
}
