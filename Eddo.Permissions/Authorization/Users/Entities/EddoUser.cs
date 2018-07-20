using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddo.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace Eddo.Permissions.Authorization.Users
{
    public abstract class EddoUser<Tuser> : EddoUserBase, IUser<long>,ICreatedTime where Tuser : EddoUser<Tuser>
    {
        public DateTime CreatedTime
        {
            get;set;
        }
        public EddoUser()
        {
            CreatedTime = System.DateTime.Now;
        }
           
    }
}
