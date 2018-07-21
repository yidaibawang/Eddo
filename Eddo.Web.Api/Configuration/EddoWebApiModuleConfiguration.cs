
using System.Web.Http;
namespace Eddo.Web.Api.Configuration
{
    public class EddoWebApiModuleConfiguration:IEddoWebApiModuleConfiguration
    {
        public HttpConfiguration HttpConfiguration { get; set; }

        public EddoWebApiModuleConfiguration()
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
        }
    }
}
