using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization.Users
{
    public class EddoLoginResult<TUser> where TUser:EddoUser<TUser>
    {
    
        public EddoLoginResultType Result { get; private set; }


        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public EddoLoginResult(EddoLoginResultType result, TUser user = null)
        {
            Result = result;
            User = user;
        }

        public EddoLoginResult(TUser user, ClaimsIdentity identity)
            : this(EddoLoginResultType.Success)
        {
            User = user;
            Identity = identity;
        }
    }
}
