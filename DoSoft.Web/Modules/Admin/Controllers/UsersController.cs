using DoSoft.Admin.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoSoft.Admin.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var model = new UserListModel();
            return View(model);
        }
        public ActionResult CreateOrEditModal()
        {
            return View();
        }
    }
}