using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Controllers.Dynamic.Clients
{
    public static class DynamicApiClientBuilder
    {
        public static IApiClientBuilder<TService> For<TService>(string url)
        {
            return new ApiClientBuilder<TService>(url);
        }
    }
}
