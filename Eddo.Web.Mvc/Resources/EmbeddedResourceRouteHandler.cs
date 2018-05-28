using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Eddo.Web.Mvc.Resources
{
    [Obsolete]
    internal class EmbeddedResourceRouteHandler : IRouteHandler
    {
        private readonly string _rootPath;

        public EmbeddedResourceRouteHandler(string rootPath)
        {
            _rootPath = rootPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new EmbeddedResourceHttpHandler(_rootPath, requestContext.RouteData);
        }
    }
}
