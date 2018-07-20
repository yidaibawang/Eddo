using Eddo.Auditing;
using Eddo.Dependency;
using Eddo.Domain.Repository;
using Eddo.Domain.UnitOfWorks;
using Eddo.Extensions;
using Eddo.Permissions.Authorization.Roles;
using Eddo.Permissions.Authorization.Roles.Entities;
using Eddo.Permissions.Authorization.Users;
using Eddo.Permissions.Authorization.Users.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace Eddo.Permissions.Authorization
{
    public abstract class EddoLoginManager<TRole, TUser> : ITransientDependency
        where TRole : EddoRole<TUser>, new()
        where TUser : EddoUser<TUser>
    {
        protected EddoUserManage<TRole, TUser> UserManager { get; }
        protected EddoRoleManager<TRole, TUser> RoleManager { get; }
        protected IUnitOfWorkManage UnitOfWorkManager { get; }
        public IClientInfoProvider ClientInfoProvider { get; set; }
        protected IRepository<UserLoginAttempt, long> UserLoginAttemptRepository { get; }
        protected IIocResolve IocResolver { get; }
        public EddoLoginManager(EddoUserManage<TRole, TUser> userManage, EddoRoleManager<TRole, TUser> roleManage,
            IUnitOfWorkManage unitOfWorkManager, IRepository<UserLoginAttempt, long> userLoginAttemptRepository, IIocResolve iocResolver)
        {
            UserManager = userManage;
            RoleManager = roleManage;
            UnitOfWorkManager = unitOfWorkManager;
            UserLoginAttemptRepository = userLoginAttemptRepository;
            IocResolver = iocResolver;
            ClientInfoProvider = NullClientInfoProvider.Instance;
        }
        public virtual async Task<EddoLoginResult<TUser>> LoginAsync(UserLoginInfo login, string tenancyName = null)
        {

            var result = await LoginAsyncInternal(login, tenancyName);
            await SaveLoginAttempt(result, tenancyName, login.ProviderKey + "@" + login.LoginProvider);
            return result;
        }
        protected virtual async Task<EddoLoginResult<TUser>> LoginAsyncInternal(UserLoginInfo login, string tenancyName)
        {
            if (login == null || login.LoginProvider.IsNullOrEmpty() || login.ProviderKey.IsNullOrEmpty())
            {
                throw new ArgumentException("login");
            }
            var user = await UserManager.UserStore.FindAsync(login);
            if (user == null)
            {
                return new EddoLoginResult<TUser>(EddoLoginResultType.UnknownExternalLogin);
            }

            return await CreateLoginResultAsync(user);
        }
        public virtual async Task<EddoLoginResult<TUser>> LoginAsync(string userNameOrEmailAddress, string plainPassword, string tenancyName = null, bool shouldLockout = true)
        {
            var result = await LoginAsyncInternal(userNameOrEmailAddress, plainPassword, tenancyName, shouldLockout);
            await SaveLoginAttempt(result, tenancyName, userNameOrEmailAddress);
            return result;
        }

        protected virtual async Task<EddoLoginResult<TUser>> LoginAsyncInternal(string userNameOrEmailAddress, string plainPassword, string tenancyName, bool shouldLockout)
        {
            if (userNameOrEmailAddress.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userNameOrEmailAddress));
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            //TryLoginFromExternalAuthenticationSources method may create the user, that's why we are calling it before AbpStore.FindByNameOrEmailAsync
            var loggedInFromExternalSource = await TryLoginFromExternalAuthenticationSources(userNameOrEmailAddress, plainPassword);

            var user = await UserManager.UserStore.FindByNameOrEmailAsync(userNameOrEmailAddress);
            if (user == null)
            {
                return new EddoLoginResult<TUser>(EddoLoginResultType.InvalidUserNameOrEmailAddress);
            }

            if (!loggedInFromExternalSource)
            {

                if (await UserManager.IsLockedOutAsync(user.Id))
                {
                    return new EddoLoginResult<TUser>(EddoLoginResultType.LockedOut, user);
                }

                var verificationResult = UserManager.PasswordHasher.VerifyHashedPassword(user.Password, plainPassword);
                if (verificationResult != PasswordVerificationResult.Success)
                {
                    if (shouldLockout)
                    {
                        if (await TryLockOutAsync(user.Id))
                        {
                            return new EddoLoginResult<TUser>(EddoLoginResultType.LockedOut, user);
                        }
                    }

                    return new EddoLoginResult<TUser>(EddoLoginResultType.InvalidPassword, user);
                }

                await UserManager.ResetAccessFailedCountAsync(user.Id);

            }
           return await CreateLoginResultAsync(user);

        }

        protected virtual async Task<bool> TryLoginFromExternalAuthenticationSources(string userNameOrEmailAddress, string plainPassword)
        {

            return false;
        }
        protected virtual async Task<bool> TryLockOutAsync(long userId)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {

                var isLockOut = await UserManager.IsLockedOutAsync(userId);
                await uow.CommitAsync();

                return isLockOut;
            }

        }

        protected virtual async Task<EddoLoginResult<TUser>> CreateLoginResultAsync(TUser user)
        {
            if (!user.IsActive)
            {
                return new EddoLoginResult<TUser>(EddoLoginResultType.UserIsNotActive);
            }

            if (!user.IsEmailConfirmed)
            {
                return new EddoLoginResult<TUser>(EddoLoginResultType.UserEmailIsNotConfirmed);
            }

            user.LastLoginTime = DateTime.Now;

            await UserManager.UserStore.UpdateAsync(user);

            return new EddoLoginResult<TUser>(
                user,
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
            );
        }
        protected virtual async Task SaveLoginAttempt(EddoLoginResult<TUser> loginResult, string tenancyName, string userNameOrEmailAddress)
        {
            using (var uow = UnitOfWorkManager.Begin())
            {
                var loginAttempt = new UserLoginAttempt
                {
                    TenantId = null,
                    TenancyName = tenancyName,

                    UserId = loginResult.User != null ? loginResult.User.Id : (long?)null,
                    UserNameOrEmailAddress = userNameOrEmailAddress,

                    Result = loginResult.Result,

                    BrowserInfo = ClientInfoProvider.BrowserInfo,
                    ClientIpAddress = ClientInfoProvider.ClientIpAddress,
                    ClientName = ClientInfoProvider.ComputerName,
                };
                await UserLoginAttemptRepository.AddAsync(loginAttempt);
                await uow.CommitAsync();
            }
        }

    }


}

