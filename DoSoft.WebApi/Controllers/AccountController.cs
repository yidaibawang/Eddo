using DoSoft.Core.Authorization;
using DoSoft.Core.Authorization.Roles;
using DoSoft.Core.UserManagerment;
using DoSoft.WebApi.Models;
using Eddo.Permissions.Authorization;
using Eddo.Permissions.Authorization.Users;
using Eddo.UI;
using Eddo.Web.Api.Controllers;
using Eddo.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
namespace DoSoft.WebApi.Controllers
{
    public class AccountController : EddoApiController
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        private readonly LogInManager _logInManager;

        static AccountController()
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
        }

        public AccountController(LogInManager logInManager)
        {
            _logInManager = logInManager;
        }

        [HttpPost]
        public async Task<AjaxResponse> Authenticate(LoginModel loginModel)
        {
            CheckModelState();

            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                null
                );

            var ticket = new AuthenticationTicket(loginResult.Identity, new AuthenticationProperties());

            var currentUtc =System.DateTime.Now;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            return new AjaxResponse(OAuthBearerOptions.AccessTokenFormat.Protect(ticket));
        }

        private async Task<EddoLoginResult<User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password);

            switch (loginResult.Result)
            {
                case EddoLoginResultType.Success:
                    return loginResult;

                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }

        }

        private Exception CreateExceptionForFailedLoginAttempt(EddoLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case EddoLoginResultType.Success:
                    return new ApplicationException("不要使用成功结果调用此方法!");
                case EddoLoginResultType.InvalidUserNameOrEmailAddress:
                case EddoLoginResultType.InvalidPassword:
                    return new UserFriendlyException("登录异常", " 用户名或密码错误");
                case EddoLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException("登录异常", "租户不存在");
                case EddoLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException("登录异常", "租户没激活");
                case EddoLoginResultType.UserIsNotActive:
                    return new UserFriendlyException("登录异常", "账号" + usernameOrEmailAddress + "没有激活");
                case EddoLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException("登录异常", "邮箱没有确认不能登录");
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException("登录失败");
            }
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("Invalid request!");
            }
        }
    }
}
