using DoSoft.Core.Security.Dtos;
using DoSoft.Core.Security.Entities;
using Eddo.Caching;
using Eddo.Domain.Repository;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions;
namespace DoSoft.Core.Security
{
    public class ModuleMange:EddoModuleMangeBase<Module,int, ModuleInputDto>
    {
        public ModuleMange(ICacheManager cacheManager, IUnitOfWorkManage unitOfWorkManager,
            IRepository<Module, int> moduleRepository):base(cacheManager, unitOfWorkManager, moduleRepository)
        {

        }
    }
}
