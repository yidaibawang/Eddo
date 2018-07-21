using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Controllers.Dynamic.Clients
{
    public interface IApiClientBuilder<TService>
    {
        /// <summary>
        /// 
        /// </summary>
        void Build();
    }
}
