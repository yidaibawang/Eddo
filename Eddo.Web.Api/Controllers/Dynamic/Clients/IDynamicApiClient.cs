using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Controllers.Dynamic.Clients
{
    public interface IDynamicApiClient<out TService>
    {
        /// <summary>
        /// Url of the service.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// The service object.
        /// </summary>
        TService Service { get; }
    }
}
