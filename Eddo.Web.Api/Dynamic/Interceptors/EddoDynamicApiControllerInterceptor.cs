using System;
using Castle.DynamicProxy;
using System.Reflection;
using Eddo.Extensions;

namespace Eddo.WebApi.Dynamic.Interceptors
{
    public class EddoDynamicApiControllerInterceptor<T> : IInterceptor
    {
        private readonly T _proxiedObject;

        public EddoDynamicApiControllerInterceptor(T proxiedObject)
        {
            _proxiedObject = proxiedObject;
        }
        public void Intercept(IInvocation invocation)
        {
            if (DynamicApiControllerActionHelper.IsMethodOfType(invocation.Method, typeof(T)))
            {
                //Call real object's method
                try
                {
                    invocation.ReturnValue = invocation.Method.Invoke(_proxiedObject, invocation.Arguments);
                }
                catch (TargetInvocationException targetInvocation)
                {
                    if (targetInvocation.InnerException != null)
                    {
                        targetInvocation.InnerException.ReThrow();
                    }

                    throw;
                }
            }
            else
            {
                //Call api controller's methods as usual.
                invocation.Proceed();
            }
        }
    }
}
