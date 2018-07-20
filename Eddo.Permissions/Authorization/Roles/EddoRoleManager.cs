using Eddo.Caching;
using Eddo.Dependency;
using Eddo.Domain.Services;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization.Roles
{
    public abstract class EddoRoleManager<TRole, TUser> : RoleManager<TRole, int>,
       IDomainService, ITransientDependency
       where TRole : EddoRole<TUser> where TUser : EddoUser<TUser>
    {
        protected EddoRoleStore<TRole, TUser> EddoRoleStore { get; private set; }

        private readonly ICacheManager _cacheManager;
        private readonly IUnitOfWorkManage _unitOfWorkManager;
        public EddoRoleManager(EddoRoleStore<TRole, TUser> store, IUnitOfWorkManage unitOfWorkManager,
            ICacheManager cacheManager) : base(store)
        {
            EddoRoleStore = store;
            _unitOfWorkManager = unitOfWorkManager;
            _cacheManager = cacheManager;
        }
        public override IQueryable<TRole> Roles { get {
                return EddoRoleStore.FindAll();
            } }
        public override Task<TRole> FindByNameAsync(string displayName)
        {
            return EddoRoleStore.FindByNameAsync(displayName);
        }
        public async override Task<IdentityResult> CreateAsync(TRole role)
        {
            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name, role.DisplayName);
            if (!result.Succeeded)
            {
                return result;
            }
            role.TenantId = null;
            return await base.CreateAsync(role);
        }
        public async override Task<IdentityResult> DeleteAsync(TRole role)
        {
            if (role.IsStatic)
            {
                return EddoIdentityResult.Failed(string.Format("CanNotDeleteStaticRole"), role.Name);
            }

            return await base.DeleteAsync(role);
        }
        public override async Task<IdentityResult> UpdateAsync(TRole role)
        {
            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name, role.DisplayName);
            if (!result.Succeeded)
            {
                return result;
            }

            return await base.UpdateAsync(role);
        }
        public virtual async Task<IdentityResult> CheckDuplicateRoleNameAsync(int? expectedRoleId, string name, string displayName)
        {
            var role = await FindByDisplayNameAsync(name);
            if (role != null && role.Id != expectedRoleId)
            {
                return EddoIdentityResult.Failed(string.Format("RoleNameIsAlreadyTaken", name));
            }

            role = await FindByDisplayNameAsync(displayName);
            if (role != null && role.Id != expectedRoleId)
            {
                return EddoIdentityResult.Failed(string.Format("RoleDisplayNameIsAlreadyTaken", displayName));
            }

            return IdentityResult.Success;
        }
        public virtual Task<TRole> FindByDisplayNameAsync(string displayName)
        {
            return EddoRoleStore.FindByDisplayNameAsync(displayName);
        }
       
    }
}
