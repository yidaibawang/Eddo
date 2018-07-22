using Eddo.Dependency;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Eddo.WebApi
{

    public class EddoApiControllerActivator : IHttpControllerActivator
    {
        private readonly IIocResolve _iocResolver;

        public EddoApiControllerActivator(IIocResolve iocResolver)
        {
            _iocResolver = iocResolver;
        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = _iocResolver.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}
