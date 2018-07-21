using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Eddo.Collections.Extensions;
using Eddo.Extensions;
using Eddo.Caching;
using Eddo.UI;
using Eddo.Web.Models;
using Eddo.Web.Api.Controllers;

namespace Eddo.Web.Api.Controllers.Runtime.Caching
{
    public class EddoCacheController : EddoApiController
    {
        private readonly ICacheManager _cacheManager;

        public EddoCacheController(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        [HttpPost]
        public async Task<AjaxResponse> Clear(ClearCacheModel model)
        {
            if (model.Password.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Password can not be null or empty!");
            }

            if (model.Caches.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Caches can not be null or empty!");
            }


            var caches = _cacheManager.GetAllCaches().Where(c => model.Caches.Contains(c.Name));
            foreach (var cache in caches)
            {
                cache.Clear();
            }

            return new AjaxResponse();
        }

        [HttpPost]
        [Route("api/AbpCache/ClearAll")]
        public async Task<AjaxResponse> ClearAll(ClearAllCacheModel model)
        {
            if (model.Password.IsNullOrEmpty())
            {
                throw new UserFriendlyException("Password can not be null or empty!");
            }


            var caches = _cacheManager.GetAllCaches();
            foreach (var cache in caches)
            {
                 cache.Clear();
            }

            return new AjaxResponse();
        }
    }
}
