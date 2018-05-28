using Eddo.Permissions.Authorization.Users;
using Eddo.Domain.UnitOfWorks;

namespace Web.Core.Authorization
{
    public class UserManager : EddoUserManage<Role, User>
    {
        public UserManager(UserStore store, IUnitOfWorkManage unitOfWorkManager) : base(store, unitOfWorkManager)
        {

        }
    }
}
