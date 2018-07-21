using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Controllers.Dynamic.Buiders
{
    internal static class DynamicApiVerbHelper
    {
        public static HttpVerb GetConventionalVerbForMethodName(string methodName)
        {
            if (methodName.StartsWith("Get", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Get;
            }

            if (methodName.StartsWith("Update", StringComparison.InvariantCultureIgnoreCase) || methodName.StartsWith("Put", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Put;
            }

            if (methodName.StartsWith("Delete", StringComparison.InvariantCultureIgnoreCase) || methodName.StartsWith("Remove", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Delete;
            }

            if (methodName.StartsWith("Create", StringComparison.InvariantCultureIgnoreCase) || methodName.StartsWith("Post", StringComparison.InvariantCultureIgnoreCase))
            {
                return HttpVerb.Post;
            }

            return GetDefaultHttpVerb();
        }

        public static HttpVerb GetDefaultHttpVerb()
        {
            return HttpVerb.Post;
        }
    }
}
