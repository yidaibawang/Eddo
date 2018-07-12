using DoSoft.Core.Authorization.Roles;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSoft.Core.UserManagerment
{
    public class UserManager : EddoUserManage<Role, User>
    {
        public UserManager(UserStore store, IUnitOfWorkManage unitOfWorkManager) : base(store, unitOfWorkManager)
        {

        }
    }
}
