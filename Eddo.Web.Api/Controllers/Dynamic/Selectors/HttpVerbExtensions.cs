using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Web.Api.Controllers.Dynamic.Selectors
{
    public static class HttpVerbExtensions
    {
        /// <summary>
        /// Compares a <see cref="HttpVerb"/> with a <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="verb">The Http verb</param>
        /// <param name="method">The Http method</param>
        /// <returns>True, if they are equal</returns>
        public static bool IsEqualTo(this HttpVerb verb, HttpMethod method)
        {
            if (verb == HttpVerb.Get && method == HttpMethod.Get)
            {
                return true;
            }

            if (verb == HttpVerb.Post && method == HttpMethod.Post)
            {
                return true;
            }

            if (verb == HttpVerb.Put && method == HttpMethod.Put)
            {
                return true;
            }

            if (verb == HttpVerb.Delete && method == HttpMethod.Delete)
            {
                return true;
            }

            return false;
        }
    }
}
