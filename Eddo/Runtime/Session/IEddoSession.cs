using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Runtime.Session
{
     public  interface IEddoSession
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// 租户编号
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// UserId of the impersonator.
        /// This is filled if a user is performing actions behalf of the <see cref="UserId"/>.
        /// </summary>
        long? ImpersonatorUserId { get; }

        /// <summary>
        /// TenantId of the impersonator.
        /// This is filled if a user with <see cref="ImpersonatorUserId"/> performing actions behalf of the <see cref="UserId"/>.
        /// </summary>
        int? ImpersonatorTenantId { get; }

        /// <summary>
        /// Used to change <see cref="TenantId"/> and <see cref="UserId"/> for a limited scope.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IDisposable Use(int? tenantId, long? userId);
    }
}

