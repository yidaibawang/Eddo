using Microsoft.AspNet.Identity;
using System.Collections.Generic;
namespace Eddo.Permissions.IdentityFramework
{
    public  class EddoIdentityResult:IdentityResult
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
