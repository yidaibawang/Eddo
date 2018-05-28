using Eddo.Permissions.Authorization.Users;
using Eddo.Domain.Repository;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Users.Entities;

namespace Web.Core.Authorization
{
    public class UserStore : EddoUserStore<Role, User>
    {
        public UserStore(IRepository<User, long> userRepository, IRepository<UserLogin, long> userLoginRepository, IRepository<UserRole, long> userRoleRepository, IRepository<UserClaim, long> userClaimRepository, IRepository<Role> roleRepository, IRepository<UserPermissionSetting, long> userPermissionSettingRepository, IUnitOfWorkManage unitOfWorkManager) : base(userRepository, userLoginRepository, userRoleRepository, userClaimRepository, roleRepository, userPermissionSettingRepository, unitOfWorkManager)
        {
        }
    }
}
