using DoSoft.Admin.Models.Users;
using DoSoft.Application.Authorization;
using DoSoft.Core.UserManagerment;
using System.Web.Mvc;
using Eddo.Applications.Services.Dtos;
using System.Threading.Tasks;

namespace DoSoft.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserAppServicecs _userAppService;
        private readonly UserManager _userManager;
        public UsersController(IUserAppServicecs userAppService, UserManager userManager)
        {
            _userAppService = userAppService;
            _userManager = userManager;
        }
        // GET: Users
        public ActionResult Index()
        {
            var model = new UserListModel();
            return View(model);
        }
        public async Task<ActionResult> CreateOrEditModal(long?id)
        {
            var output = await _userAppService.GetUserForEdit(new NullableIdDto<long> { Id = id });
            var viewModel = new CreateOrEditUserModalViewModel(output);

            return PartialView("_CreateOrEditModal", viewModel);
        }
    }
}