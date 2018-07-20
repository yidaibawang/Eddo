using DoSoft.Core.UserManagerment;
using Eddo.Domain.Repository;
using Eddo.Permissions.Authorization.Roles;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Core.Authorization.Roles
{
    public class RoleStore : EddoRoleStore<Role, User>
    {
        public RoleStore(IRepository<Role> roleRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolePermissionSetting, long> rolePermissionSettingRepository) : base(roleRepository, userRoleRepository, rolePermissionSettingRepository)
        {
        }
    }
}

