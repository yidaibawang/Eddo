using DoSoft.Core.UserManagerment;
using Eddo.Applications.Services;
using Eddo.Runtime.Session;
using System;
using System.Threading.Tasks;

namespace DoSoft.Application
{
    public abstract class DosoftAppServiceBase: ApplicationService
    {
        public UserManager UserManager { get; set; }
        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(EddoSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }
    }
}
