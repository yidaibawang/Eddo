using Eddo.Dependency;
using Eddo.Reflection;
using Eddo.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Authorization
{
    public class AuthorizationHelper : IAuthorizationHelper, ITransientDependency
    {
        public IEddoSession EddoSession { get; set; }


        public AuthorizationHelper()
        {

            EddoSession = NullEddoSession.Instance;
    
        }

        public async Task AuthorizeAsync(IEnumerable<IEddoAuthorizeAttribute> authorizeAttributes)
        {
            await Task.FromResult(0);
        }

        public async Task AuthorizeAsync(MethodInfo methodInfo)
        {
            await CheckFeatures(methodInfo);
            await CheckPermissions(methodInfo);
        }

        private async Task CheckFeatures(MethodInfo methodInfo)
        {
             await Task.FromResult(0);
        }

        private async Task CheckPermissions(MethodInfo methodInfo)
        {
       

            if (AllowAnonymous(methodInfo))
            {
                return;
            }

            var authorizeAttributes =
                ReflectionHelper.GetAttributesOfMemberAndDeclaringType(
                    methodInfo
                ).OfType<IEddoAuthorizeAttribute>().ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            await AuthorizeAsync(authorizeAttributes);
        }

        private static bool AllowAnonymous(MethodInfo methodInfo)
        {
            return true;
        }
    }
    
}
