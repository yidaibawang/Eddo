using Eddo.Permissions.Authorization.Users;
using Microsoft.AspNet.Identity;

namespace Eddo.Permissions.Authorization.Roles.Entities
{
    public class EddoRole<TUser> : EddoRoleBase, IRole<int> where TUser : EddoUser<TUser>
    {
        protected EddoRole()
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="displayName"></param>
        protected EddoRole(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        protected EddoRole(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }
    }
}
