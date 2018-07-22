using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eddo.WebApi
{
    public   interface IEddoWebApiModuleConfiguration
    {
        HttpConfiguration HttpConfiguration { get; set; }
    }
}
