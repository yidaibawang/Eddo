using Eddo.Dependency;
using Eddo.Extensions;
using Eddo.Resources.Embedded;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Eddo.Web.Mvc.Resources
{
    public class WebResourceHelper
    {
        public static void ExposeEmbeddedResources(string rootPath, Assembly assembly, string resourceNamespace)
        {
            SingletonDependency<IEmbeddedResourcesConfiguration>.Instance.Sources.Add(
                new EmbeddedResourceSet(rootPath, assembly, resourceNamespace)
            );

            /* @hikalkan: Default values are a workaround. If it's not set, @Url.Action in views can not run
             * properly and use this route accidently.
             * We should find a better way of serving embedded resources in the future, but this works as I tested.
             */
            RouteTable.Routes.MapRoute(
                name: "EmbeddedResource:" + rootPath,
                url: rootPath.EnsureEndsWith('/') + "{*pathInfo}",
                defaults: new
                {
                    controller = "EddoNoController",
                    action = "EddoNoAction"
                }
                ).RouteHandler = new EmbeddedResourceRouteHandler(rootPath);
        }

        /// <summary>
        /// Gets an embedded resource file.
        /// </summary>
        /// <param name="fullResourcePath">Full path of the resource</param>
        /// <returns>Embedded resource file</returns>
        public static EmbeddedResourceItem GetEmbeddedResource(string fullResourcePath)
        {
            return SingletonDependency<IEmbeddedResourceManager>.Instance.GetResource(fullResourcePath);
        }
    }
}
