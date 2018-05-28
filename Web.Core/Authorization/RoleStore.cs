using Eddo.Permissions.Authorization.Roles;
using Eddo.Domain.Repository;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;

namespace Web.Core.Authorization
{
    public class RoleStore : EddoRoleStore<Role, User>
    {
        public RoleStore(IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolePermissionSetting, long> rolePermissionSettingRepository) : base(roleRepository, userRoleRepository, rolePermissionSettingRepository)
        {
        }
    }
}
