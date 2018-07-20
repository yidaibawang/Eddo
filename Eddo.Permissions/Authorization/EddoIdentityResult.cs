using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization
{
    public class EddoIdentityResult: IdentityResult
    {
        public EddoIdentityResult()
        {

        }

        public EddoIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {

        }

        public EddoIdentityResult(params string[] errors)
            :base(errors)
        {

        }

        public static EddoIdentityResult Failed(params string[] errors)
        {
            return new EddoIdentityResult(errors);
        }
    }
}
