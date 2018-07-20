using DoSoft.Core.Authorization.Roles;
using DoSoft.Core.UserManagerment;
using Eddo.Dependency;
using Eddo.Domain.Repository;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization;
using Eddo.Permissions.Authorization.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Core.Authorization
{
    public class LogInManager:EddoLoginManager<Role, User>
    {
        public LogInManager(UserManager userManage, RoleManager roleManage, IUnitOfWorkManage unitOfWorkManager, IRepository<UserLoginAttempt, long> userLoginAttemptRepository, IIocResolve iocResolver) :
         base(userManage, roleManage, unitOfWorkManager, userLoginAttemptRepository, iocResolver)
        {
        }
    }
}
