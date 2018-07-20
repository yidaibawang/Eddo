using Eddo.Permissions.Authorization;
using Web.Core.Authorization;
using Eddo.Dependency;
using Eddo.Domain.Repository;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Users.Entities;

namespace Web.Core
{
    public class LogInManager : EddoLoginManager<Role, User>
    {     

        public LogInManager(UserManager userManage, RoleManager roleManage, IUnitOfWorkManage unitOfWorkManager, IRepository<UserLoginAttempt, long> userLoginAttemptRepository, IIocResolve iocResolver) : 
            base(userManage, roleManage, unitOfWorkManager, userLoginAttemptRepository, iocResolver)
        {
        }
    }
}
