using Eddo.Permissions.Authorization.Roles;
using Eddo.Caching;
using Eddo.Domain.UnitOfWorks;

namespace Web.Core.Authorization
{
    public class RoleManager : EddoRoleManager<Role, User>
    {
        public RoleManager(RoleStore store, IUnitOfWorkManage unitOfWorkManager, ICacheManager cacheManager) : base(store, unitOfWorkManager, cacheManager)
        {

        }
    }
}
