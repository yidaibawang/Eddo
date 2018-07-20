using Eddo.Domain.Repository;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Eddo.Dependency;
using System.Linq;

namespace Eddo.Permissions.Authorization.Roles
{

    public abstract class EddoRoleStore<TRole, TUser> : IRoleStore<TRole, int>,ITransientDependency
        where TUser : EddoUser<TUser>
        where TRole : EddoRole<TUser>
    {
        private readonly IRepository<TRole> _roleRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionSettingRepository;
        public EddoRoleStore(IRepository<TRole> roleRepository, IRepository<UserRole, long> userRoleRepository, IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _rolePermissionSettingRepository = rolePermissionSettingRepository;
        }
        public virtual async Task CreateAsync(TRole role)
        {
            await _roleRepository.AddAsync(role);
        }
        public virtual async Task UpdateAsync(TRole role)
        {
            await _roleRepository.UpdateAsync(role);
        }
        public virtual async Task DeleteAsync(TRole role)
        {
            await _roleRepository.RemoveAsync(role);
        }
        public virtual Task<TRole> FindByIdAsync(int roleId)
        {
            return _roleRepository.SingleAsync(r => r.Id == roleId);
        }
        public virtual IQueryable<TRole> FindAll()
        {
            return _roleRepository.FindAll();
        }
        public virtual Task<TRole> FindByNameAsync(string roleName)
        {
            return _roleRepository.SingleAsync(r => r.Name == roleName);
        }
        public virtual async Task<TRole> FindByDisplayNameAsync(string displayName)
        {
            return await _roleRepository.FirstOrDefaultAsync(
                role => role.DisplayName == displayName
                );
        }

        public virtual void Dispose()
        {

        }
    }
}
