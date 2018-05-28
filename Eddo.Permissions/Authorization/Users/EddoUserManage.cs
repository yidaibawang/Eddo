using Eddo.Dependency;
using Eddo.Domain.Services;
using Eddo.Domain.UnitOfWorks;
using Eddo.Permissions.Authorization.Roles;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Runtime.Session;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization.Users
{
    public abstract class EddoUserManage<TRole, TUser> : UserManager<TUser, long>, IDomainService, ITransientDependency
        where TUser:EddoUser<TUser>
        where TRole:EddoRole<TUser>, new()
    {

        public IEddoSession EddoSession { get; set; }

        protected EddoRoleManager<TRole, TUser> RoleManager { get; }
        public EddoUserStore<TRole, TUser> UserStore { get; }
        private readonly IUnitOfWorkManage _unitOfWorkManager;
        public EddoUserManage(EddoUserStore<TRole, TUser> store, IUnitOfWorkManage unitOfWorkManager) : base(store)
        {
            _unitOfWorkManager = unitOfWorkManager;
            UserStore = store;
            EddoSession = NullEddoSession.Instance;
        }
        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            user.TenantId = null;

            return await base.CreateAsync(user);
        }
        public virtual async Task<IdentityResult> CheckDuplicateUsernameOrEmailAddressAsync(long? expectedUserId, string userName, string emailAddress)
        {
            var user = (await FindByNameAsync(userName));
            if (user != null && user.Id != expectedUserId)
            {
                return EddoIdentityResult.Failed("Identity.DuplicateUserName", userName);
            }

            user = (await FindByEmailAsync(emailAddress));
            if (user != null && user.Id != expectedUserId)
            {
                return EddoIdentityResult.Failed("Identity.DuplicateEmail", emailAddress);
            }

            return IdentityResult.Success;
        }

        public virtual async Task<TUser> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            return await UserStore.FindByNameOrEmailAsync(userNameOrEmailAddress);
        }

        public virtual Task<List<TUser>> FindAllAsync(UserLoginInfo login)
        {
            return UserStore.FindAllAsync(login);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception cref="AbpException">Throws exception if no user found with given id</exception>
        public virtual async Task<TUser> GetUserByIdAsync(long userId)
        {
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("There is no user with id: " + userId);
            }

            return user;
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async override Task<IdentityResult> UpdateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.EmailAddress);
            if (!result.Succeeded)
            {
                return result;
            }

            //Admin user's username can not be changed!
            if (user.UserName != EddoUser<TUser>.AdminUserName)
            {
                if ((await GetOldUserNameAsync(user.Id)) == EddoUser<TUser>.AdminUserName)
                {
                    return EddoIdentityResult.Failed("CanNotRenameAdminUser", EddoUser<TUser>.AdminUserName);
                }
            }

            return await base.UpdateAsync(user);
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected virtual Task<string> GetOldUserNameAsync(long userId)
        {
            return UserStore.GetUserNameFromDatabaseAsync(userId);
        }
        public async override Task<IdentityResult> DeleteAsync(TUser user)
        {
            if (user.UserName == EddoUser<TUser>.AdminUserName)
            {
                return EddoIdentityResult.Failed("CanNotDeleteAdminUser", EddoUser<TUser>.AdminUserName);
            }

            return await base.DeleteAsync(user);
        }

        public virtual async Task<IdentityResult> ChangePasswordAsync(TUser user, string newPassword)
        {
            var result = await PasswordValidator.ValidateAsync(newPassword);
            if (!result.Succeeded)
            {
                return result;
            }

            await UserStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword));
            return IdentityResult.Success;
        }

        /// <summary>
        /// 创建身份认证
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationType"></param>
        /// <returns></returns>
        public async override Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);
            if (user.TenantId.HasValue)
            {
                identity.AddClaim(new Claim(user.Id.ToString(),user.Id.ToString(CultureInfo.InvariantCulture)));
            }

            return identity;
        }


    }
}
