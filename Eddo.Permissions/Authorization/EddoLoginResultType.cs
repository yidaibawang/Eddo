using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization
{
    public enum EddoLoginResultType: byte
    {
        Success = 1,

        InvalidUserNameOrEmailAddress,
        
        InvalidPassword,
        
        UserIsNotActive,

        InvalidTenancyName,
        
        TenantIsNotActive,

        UserEmailIsNotConfirmed,
        
        UnknownExternalLogin,

        LockedOut,

        UserPhoneNumberIsNotConfirmed,
    }
}
