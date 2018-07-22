using Eddo.WebApi.Dynamic.Formatters;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Eddo.WebApi.Dynamic.Scripting
{
    public class EddoServiceProxiesController: EddoWebApiController
    {
        private readonly ScriptProxyManager _scriptProxyManager;
        public EddoServiceProxiesController(ScriptProxyManager scriptProxyManager)
        {
            _scriptProxyManager = scriptProxyManager;
        }

        /// <summary>
        /// 通过名称获取代理服务
        /// </summary>
        /// <param name="name">Name of the service</param>
        /// <param name="type">Script type</param>
        public HttpResponseMessage Get(string name, ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetScript(name, type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }

        /// <summary>
        /// 获取所属服务信息
        /// </summary>
        /// <param name="type">Script type</param>
        public HttpResponseMessage GetAll(ProxyScriptType type = ProxyScriptType.JQuery)
        {
            var script = _scriptProxyManager.GetAllScript(type);
            var response = Request.CreateResponse(System.Net.HttpStatusCode.OK, script, new PlainTextFormatter());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-javascript");
            return response;
        }
    }
}
