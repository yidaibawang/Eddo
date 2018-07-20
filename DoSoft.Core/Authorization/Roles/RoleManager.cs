using DoSoft.Core.UserManagerment;
using Eddo.Caching;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Core.Authorization.Roles
{
    public class RoleManager: EddoRoleManager<Role, User>
    {
        public RoleManager(RoleStore store, IUnitOfWorkManage unitOfWorkManager, ICacheManager cacheManager) : base(store, unitOfWorkManager, cacheManager)
        {

        }
    }
}
