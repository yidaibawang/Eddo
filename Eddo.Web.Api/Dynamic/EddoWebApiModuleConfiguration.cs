using System.Web.Http;

namespace Eddo.WebApi
{
    public class EddoWebApiModuleConfiguration : IEddoWebApiModuleConfiguration
    {    
        
        public HttpConfiguration HttpConfiguration
        {
            get;set;
        }
        public EddoWebApiModuleConfiguration()
        {
            HttpConfiguration = GlobalConfiguration.Configuration;
        }
    }
}
