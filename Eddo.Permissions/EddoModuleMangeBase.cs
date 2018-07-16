using Eddo.Caching;
using Eddo.Dependency;
using Eddo.Domain.Repository;
using Eddo.Domain.Services;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eddo.Permissions
{
    public abstract class EddoModuleMangeBase<TModule, TModuleKey, TModuleInputDto> : IEddoModuleMange<TModule, TModuleKey, TModuleInputDto>,IDomainService, ITransientDependency
        where TModule: EddoModuleBase<TModuleKey>
        where TModuleKey : struct, IEquatable<TModuleKey>
        where TModuleInputDto: EddoModuleInputDtoBase<TModuleKey>
    {

        private readonly ICacheManager _cacheManager;
        private readonly IUnitOfWorkManage _unitOfWorkManager;
        private readonly IRepository<TModule, TModuleKey> _moduleRepository;
        public EddoModuleMangeBase(ICacheManager cacheManager,IUnitOfWorkManage unitOfWorkManager, 
            IRepository<TModule, TModuleKey> moduleRepository)
        {
            _cacheManager = cacheManager;
            _unitOfWorkManager = unitOfWorkManager;
            _moduleRepository = moduleRepository;
        }
        public IQueryable<TModule> Modules
        {
            get { return _moduleRepository.FindAll(); }
        }
        /// <summary>
        /// 获取树节点及其子节点的所有模块编号
        /// </summary>
        /// <param name="rootIds">树节点</param>
        /// <returns>模块编号集合</returns>
        public virtual TModuleKey[] GetModuleTreeIds(params TModuleKey[] rootIds)
        {
            return rootIds.SelectMany(m => Modules.Where(n => n.TreePathString.Contains($"${m}$")).Select(n => n.Id)).Distinct()
                .ToArray();
        }

        private static string GetModuleTreePath(TModuleKey currentId, string parentTreePath, string treePathItemFormat)
        {
            return $"{parentTreePath},{currentId}";
        }
    }
}
