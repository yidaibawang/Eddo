using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization.Roles.Entities
{
     public class RolePermissionSetting:PermissionSetting
    {
         public virtual int RoleId { get; set; }
    }
}
