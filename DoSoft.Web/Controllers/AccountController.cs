using DoSoft.Core.Authorization;
using DoSoft.Core.Authorization.Roles;
using DoSoft.Core.UserManagerment;
using DoSoft.Web.Models.Account;
using Eddo.Domain.UnitOfWorks;
using Eddo.Extensions;
using Eddo.Permissions.Authorization;
using Eddo.Permissions.Authorization.Users;
using Eddo.Permissions.IdentityFramework;
using Eddo.UI;
using Eddo.Web.Models;
using Eddo.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eddo.Linq;
using System.Data.Entity;

namespace DoSoft.Web.Controllers
{
    public class AccountController : EddoController
    {
        // GET: Account
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManage _unitOfWorkManager;
        private readonly LogInManager _logInManager;


        public IAuthenticationManager _authenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public AccountController(UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManage unitOfWorkManager,
            LogInManager logInManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _logInManager = logInManager;
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginFormViewModel
            {
           
                IsSelfRegistrationEnabled =false,
                SuccessMessage = successMessage,
                UserNameOrEmailAddress = userNameOrEmailAddress
            });
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel loginModel, string returnUrl)
        {
 
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }


            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                null
                );

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            return Json(new AjaxResponse { TargetUrl = returnUrl });
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
        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {   
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            if (rememberMe)
            {
                _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
            }
            else
            {
                _authenticationManager.SignIn(
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(int.Parse(System.Configuration.ConfigurationManager.AppSettings["AuthSession.ExpireTimeInMinutes.WhenNotPersistent"] ?? "30"))
                    },
                    identity);
            }
        }

        //
        // 获取用户注册界面
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }
        [HttpPost]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("FormIsNotValidMessage");
                }

                //创建用户
                var user = new User
                {
                    Name = model.Name,
                    EmailAddress = model.EmailAddress,
                    IsActive = true
                };

                //Get external login info if possible
                ExternalLoginInfo externalLoginInfo = null;
                if (model.IsExternalLogin)
                {
                    externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        throw new ApplicationException("Can not external login!");
                    }

                    user.Logins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            LoginProvider = externalLoginInfo.Login.LoginProvider,
                            ProviderKey = externalLoginInfo.Login.ProviderKey
                        }
                    };

                    if (model.UserName.IsNullOrEmpty())
                    {
                        model.UserName = model.EmailAddress;
                    }

                    model.Password = DoSoft.Core.UserManagerment.User.CreateRandomPassword();

                    if (string.Equals(externalLoginInfo.Email, model.EmailAddress, StringComparison.InvariantCultureIgnoreCase))
                    {
                        user.IsEmailConfirmed = true;
                    }
                }
                else
                {
                    //Username and Password are required if not external login
                    if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                    {
                        throw new Exception("表单验证信息");
                    }
                }

                user.UserName = model.UserName;
                user.Password = new PasswordHasher().HashPassword(model.Password);

                //Switch to the tenant


                //Add default roles
                user.Roles = new List<UserRole>();
                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    user.Roles.Add(new UserRole { RoleId = defaultRole.Id });
                }

                //Save user
                await _userManager.CreateAsync(user);

                //If can not login, show a register result page
                return View("RegisterResult", new RegisterResultViewModel
                {
         
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    IsActive = user.IsActive
                });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Register", model);
            }
        }
        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors();
        }
    }
}