using Microsoft.AspNet.Identity;
using Eddo.Dependency;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Domain.Repository;
using System;
using System.Threading.Tasks;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Users.Entities;
using System.Collections.Generic;
using System.Linq;
using Eddo.Linq;
using System.Security.Claims;

namespace Eddo.Permissions.Authorization.Users
{
    public abstract class EddoUserStore<TRole,TUser>:
        IUserStore<TUser, long>,
        IUserPasswordStore<TUser, long>,
        IUserEmailStore<TUser, long>,
        IUserLoginStore<TUser, long>,
        IUserRoleStore<TUser, long>,
        IQueryableUserStore<TUser, long>,
        IUserLockoutStore<TUser, long>,
        IUserPermissionStore<TUser>,
        IUserPhoneNumberStore<TUser, long>,
        IUserClaimStore<TUser, long>,
        IUserSecurityStampStore<TUser, long>,
        IUserTwoFactorStore<TUser, long>,
        ITransientDependency
        where TUser:EddoUser<TUser>
        where TRole:EddoRole<TUser>
    {

        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private readonly IRepository<TUser, long> _userRepository;
        private readonly IRepository<UserLogin, long> _userLoginRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<UserClaim, long> _userClaimRepository;
        private readonly IRepository<TRole> _roleRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionSettingRepository;
        private readonly IUnitOfWorkManage _unitOfWorkManager;
        public EddoUserStore(IRepository<TUser, long> userRepository, IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository, IRepository<UserClaim, long> userClaimRepository,
            IRepository<TRole> roleRepository, IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManage unitOfWorkManager)
        {
            _userRepository =userRepository;
            _userLoginRepository =userLoginRepository;
            _userRoleRepository = userRoleRepository;
            _userClaimRepository = userClaimRepository;
            _roleRepository = roleRepository;
            _userPermissionSettingRepository = userPermissionSettingRepository;
            _unitOfWorkManager = unitOfWorkManager;
            AsyncQueryableExecuter = NullAsyncQueryableExecuter.Instance;
        }
        #region IUserStore
        public async Task CreateAsync(TUser user)
        {
            await _userRepository.AddAsync(user);
        }
        public async Task UpdateAsync(TUser user)
        {
            await _userRepository.UpdateAsync(user);
        }
        public async Task DeleteAsync(TUser user)
        {
            await _userRepository.RemoveAsync(user);
        }

        public async Task<TUser> FindByIdAsync(long userId)
        {
            return await _userRepository.SingleAsync(u=>u.Id==userId);
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            return await _userRepository.SingleAsync(u => u.UserName == userName);
        }

        public void Dispose()
        {
  
        }
        #endregion
        #region IUserPasswordStore
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }
        #endregion
        #region IUserEmailStore
        public Task SetEmailAsync(TUser user, string email)
        {
            user.EmailAddress = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.EmailAddress);
        }

        public async Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return await Task.FromResult(user.IsEmailConfirmed);
        }

        public  Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.IsEmailConfirmed = confirmed;
            return  Task.FromResult(0);
        }

        public  async Task<TUser> FindByEmailAsync(string email)
        {
            return await _userRepository.FirstOrDefaultAsync(
                 user => user.EmailAddress == email
             );
        }
        #endregion

   
        #region IUserLoginStore
        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            await _userLoginRepository.AddAsync(
                  new UserLogin
                  {
                      TenantId = user.TenantId,
                      LoginProvider = login.LoginProvider,
                      ProviderKey = login.ProviderKey,
                      UserId = user.Id
                  });
        }

        public virtual async Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
         var userlogin=new UserLogin
            {
                TenantId = user.TenantId,
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            };
            await _userLoginRepository.RemoveAsync(userlogin);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            return (await _userLoginRepository.GetAllListAsync(ul => ul.UserId == user.Id))
                           .Select(ul => new UserLoginInfo(ul.LoginProvider, ul.ProviderKey))
                           .ToList();
           }

        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            var userLogin = await _userLoginRepository.FirstOrDefaultAsync(
            ul => ul.LoginProvider == login.LoginProvider && ul.ProviderKey == login.ProviderKey
            );

            if (userLogin == null)
            {
                return null;
            }
            return await _userRepository.FirstOrDefaultAsync(u => u.Id == userLogin.UserId);
        }
        #endregion
        #region IUserRoleStore
        public async Task AddToRoleAsync(TUser user, string roleName)
        {
            var role = await GetRoleByNameAsync(roleName);
            await _userRoleRepository.AddAsync(new UserRole(user.TenantId, user.Id, role.Id));
        }

        public async Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            var role = await GetRoleByNameAsync(roleName);
            await _userRoleRepository.RemoveAsync(new UserRole(user.TenantId, user.Id, role.Id));
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            var query = from userRole in _userRoleRepository.FindAll()
                        join role in _roleRepository.FindAll() on userRole.RoleId equals role.Id
                        where userRole.UserId == user.Id
                        select role.Name;

            return await AsyncQueryableExecuter.ToListAsync(query);
        }

        public async Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            var role = await GetRoleByNameAsync(roleName);
            return await _userRoleRepository.FirstOrDefaultAsync(ur => ur.UserId == user.Id && ur.RoleId == role.Id) != null;
        }
        private async Task<TRole> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleRepository.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                throw new Exception("Could not find a role with name: " + roleName);
            }

            return role;
        }



        #endregion
        #region IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult(
                    user.LockoutEndDateUtc.HasValue
                        ? new DateTimeOffset(DateTime.SpecifyKind(user.LockoutEndDateUtc.Value, DateTimeKind.Utc))
                        : new DateTimeOffset()
                );
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(++user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.IsLockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.IsLockoutEnabled = enabled;
            return Task.FromResult(0);
        }


        #endregion
        #region  IPhoneNumberStore
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.IsPhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.IsPhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }


        #endregion
        #region  IUserClaimStore
        public async Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            var userClaims = await _userClaimRepository.GetAllListAsync(uc => uc.UserId == user.Id);
            return userClaims.Select(uc => new Claim(uc.ClaimType, uc.ClaimValue)).ToList();
        }

        public  Task AddClaimAsync(TUser user, Claim claim)
        {
            return _userClaimRepository.AddAsync(new UserClaim(user, claim));
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            var uc = new UserClaim
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
            _userClaimRepository.RemoveAsync(uc);
            return Task.FromResult(0);
        }
        #endregion
        #region  IUserSecurityStampStore
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }


        #endregion
        #region  IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.IsLockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(user.IsLockoutEnabled);
        }
        #endregion
        #region IUserPermissionStore
        public virtual async Task AddPermissionAsync(TUser user, PermissionGrantInfo permissionGrant)
        {
            if (await HasPermissionAsync(user.Id, permissionGrant))
            {
                return;
            }

            await _userPermissionSettingRepository.AddAsync(
                new UserPermissionSetting
                {
                    TenantId = user.TenantId,
                    UserId = user.Id,
                    Name = permissionGrant.Name,
                    IsGranted = permissionGrant.IsGranted
                });
        }

        public virtual async Task RemovePermissionAsync(TUser user, PermissionGrantInfo permissionGrant)
        {
         
            await _userPermissionSettingRepository.RemoveAsync(
                  permissionSetting => permissionSetting.UserId == user.Id &&
                                     permissionSetting.Name == permissionGrant.Name &&
                                     permissionSetting.IsGranted == permissionGrant.IsGranted
            );
        }

        public virtual async Task<IList<PermissionGrantInfo>> GetPermissionsAsync(long userId)
        {
            return (await _userPermissionSettingRepository.GetAllListAsync(p => p.UserId == userId))
                .Select(p => new PermissionGrantInfo(p.Name, p.IsGranted))
                .ToList();
        }

        public virtual async Task<bool> HasPermissionAsync(long userId, PermissionGrantInfo permissionGrant)
        {
            return await _userPermissionSettingRepository.FirstOrDefaultAsync(
                       p => p.UserId == userId &&
                            p.Name == permissionGrant.Name &&
                            p.IsGranted == permissionGrant.IsGranted
                   ) != null;
        }

        public virtual async Task RemoveAllPermissionSettingsAsync(TUser user)
        {
            await _userPermissionSettingRepository.RemoveAsync(user.Id);
        }

        #endregion

        public virtual Task<List<TUser>> FindAllAsync(UserLoginInfo login)
        {
            var query = from userLogin in _userLoginRepository.FindAll()
                        join user in _userRepository.FindAll() on userLogin.UserId equals user.Id
                        where userLogin.LoginProvider == login.LoginProvider && userLogin.ProviderKey == login.ProviderKey
                        select user;

            return Task.FromResult(query.ToList());
        }

        public virtual Task<TUser> FindAsync(int? tenantId, UserLoginInfo login)
        {
          
                var query = from userLogin in _userLoginRepository.FindAll()
                            join user in _userRepository.FindAll() on userLogin.UserId equals user.Id
                            where userLogin.LoginProvider == login.LoginProvider && userLogin.ProviderKey == login.ProviderKey
                            select user;

                return Task.FromResult(query.FirstOrDefault());
            
        }
        public virtual async Task<TUser> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            return await _userRepository.FirstOrDefaultAsync(
                user => (user.UserName == userNameOrEmailAddress || user.EmailAddress == userNameOrEmailAddress)
                );
        }
        /// <summary>
        /// 数据库中获取信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<string> GetUserNameFromDatabaseAsync(long userId)
        {
                var user = await _userRepository.FirstOrDefaultAsync(u=> u.Id==userId);
    
                return user.UserName;
            
        }
        public IQueryable<TUser> Users
        {
            get
            {
               return  _userRepository.FindAll();
            }
        }
    }
}
